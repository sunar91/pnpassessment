﻿using Microsoft.AspNetCore.DataProtection;
using Microsoft.Identity.Client;
using PnP.Core.Services;
using PnP.Scanning.Core.Services;
using PnP.Scanning.Core.Storage;
using System.Security.Cryptography.X509Certificates;

namespace PnP.Scanning.Core.Authentication
{
    internal sealed class AuthenticationManager
    {
        private IClientApplicationBase? clientApplication;
        private string? scanAuthenticationMode;
        private Func<DeviceCodeResult, Task>? deviceCodeCallback;

        public AuthenticationManager(IDataProtectionProvider provider)
        {
            DataProtectionProvider = provider;
        }

        internal IDataProtectionProvider DataProtectionProvider { get; private set; }

        internal static AuthenticationManager Create(StartRequest request, IDataProtectionProvider provider)
        {
            var authenticationManager = new AuthenticationManager(provider);

            // Configure the authentication manager
            authenticationManager.InitializeAuthentication(request);

            return authenticationManager;
        }        

        private IClientApplicationBase? InitializeAuthentication(StartRequest request)
        {
            return InitializedAuthentication(request.Tenant, request.AuthMode, Enum.Parse<Microsoft365Environment>(request.Environment), Guid.Parse(request.ApplicationId), request.TenantId,
                                             request.CertPath, request.CertFile, request.CertPassword);
        }


        private IClientApplicationBase? InitializedAuthentication(string tenantName, string authMode, Microsoft365Environment environment, Guid applicationId, string tenantId,
                                                                  string? certPath, string? certFile, string? certPassword)
        {            

            if (tenantName == null)
            {
                throw new ArgumentNullException(nameof(tenantName));
            }

            if (authMode == null)
            {
                throw new ArgumentNullException(nameof(authMode));
            }

            if (applicationId == Guid.Empty)
            {
                throw new ArgumentException("No application id specified", nameof(applicationId));
            }

            if (authMode.Equals("Interactive", StringComparison.OrdinalIgnoreCase))
            {
                var builder = PublicClientApplicationBuilder.Create(applicationId.ToString());
                builder = GetBuilderWithAuthority(builder, environment);
                builder = builder.WithRedirectUri("http://localhost");

                if (!string.IsNullOrEmpty(tenantId))
                {
                    builder = builder.WithTenantId(tenantId);
                }

                clientApplication = builder.Build();

                // Setup a local encrypted cache
                TokenCacheManager.EnableSerialization(clientApplication.UserTokenCache, DataProtectionProvider, StorageManager.GetScannerFolder());
            }
            else if (authMode.Equals("Device", StringComparison.OrdinalIgnoreCase))
            {
                var builder = PublicClientApplicationBuilder.Create(applicationId.ToString());
                builder = GetBuilderWithAuthority(builder, environment);

                if (!string.IsNullOrEmpty(tenantId))
                {
                    builder = builder.WithTenantId(tenantId);
                }

                clientApplication = builder.Build();

                // Setup a local encrypted cache
                TokenCacheManager.EnableSerialization(clientApplication.UserTokenCache, DataProtectionProvider, StorageManager.GetScannerFolder());
            }
            else if (authMode.Equals("Application", StringComparison.OrdinalIgnoreCase))
            {
                var certificate = LoadCertificate(certPath, certFile, certPassword);

                var builder = ConfidentialClientApplicationBuilder.Create(applicationId.ToString()).WithCertificate(certificate);
                builder = GetBuilderWithAuthority(builder, environment, tenantId);
                clientApplication = builder.Build();

                // Setup a local encrypted cache - not needed for application permissions as we have all we need for unattended token acquisition
                //TokenCacheManager.EnableSerialization((clientApplication as IConfidentialClientApplication).AppTokenCache, DataProtectionProvider, StorageManager.GetScannerFolder());
            }
            else
            {
                throw new Exception($"Authentication type {authMode} is unknown");
            }

            scanAuthenticationMode = authMode;
            return clientApplication;
        }

        // This method is the only one called from the scanner clients (CLI)
        internal async Task VerifyAuthenticationAsync(string tenantName, string authMode, Microsoft365Environment environment, Guid applicationId, string tenantId,
                                                      string? certPath, FileInfo? certFile, string? certPassword,
                                                      Func<DeviceCodeResult, Task> deviceCodeCallbackInstance)
        {

            clientApplication = InitializedAuthentication(tenantName, authMode, environment, applicationId, tenantId, certPath, certFile?.FullName, certPassword);
            deviceCodeCallback = deviceCodeCallbackInstance;

            if (clientApplication != null)
            {

                // Getting SharePoint access token
                var uri = new Uri(GetSiteFromTenant(tenantName));
                var scopes = new[] { $"{uri.Scheme}://{uri.Authority}/.default" };
                string accessToken = await GetAccessTokenAsync(scopes);
                if (string.IsNullOrEmpty(accessToken))
                {
                    throw new Exception("Failed to retrieve SharePoint access token");
                }

                // Getting Microsoft Graph access token
                uri = new Uri($"https://{CloudManager.GetMicrosoftGraphAuthority(environment)}");
                scopes = new[] { $"{uri.Scheme}://{uri.Authority}/.default" };
                accessToken = await GetAccessTokenAsync(scopes);
                if (string.IsNullOrEmpty(accessToken))
                {
                    throw new Exception("Failed to retrieve Graph access token");
                }
            }
            else
            {
                throw new Exception("No authentication provider was setup");
            }
        }

        internal static string GetSiteFromTenant(string tenantName)
        {
            if (Uri.TryCreate(tenantName, UriKind.Absolute, out var uri))
            {
                return $"https://{uri.DnsSafeHost}";
            }
            else
            {
                return $"https://{tenantName}";
            }
        }

        internal async Task<string> GetAccessTokenAsync(string[] scopes)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var accounts = (await clientApplication.GetAccountsAsync()).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            try
            {
                AuthenticationResult result = await clientApplication.AcquireTokenSilent(scopes, accounts.FirstOrDefault())
                    .ExecuteAsync()
                    .ConfigureAwait(false);
                return result.AccessToken;
            }
            catch (MsalUiRequiredException)
            {
                if (clientApplication is IPublicClientApplication publicClientApplication)
                {
                    if (scanAuthenticationMode == "Interactive")
                    {
                        var builder = publicClientApplication.AcquireTokenInteractive(scopes);
                        AuthenticationResult result = await builder.ExecuteAsync();
                        return result.AccessToken;
                    }
                    else if (scanAuthenticationMode == "Device")
                    {
                        var builder = publicClientApplication.AcquireTokenWithDeviceCode(scopes, deviceCodeCallback);
                        AuthenticationResult result = await builder.ExecuteAsync();
                        return result.AccessToken;
                    }
                }
                else if (clientApplication is IConfidentialClientApplication confidentialClientApplication)
                {
                    if (scanAuthenticationMode == "Application")
                    {
                        var builder = confidentialClientApplication.AcquireTokenForClient(scopes);
                        AuthenticationResult result = await builder.ExecuteAsync();
                        return result.AccessToken;
                    }
                }
            }

            return null;
        }        

        private PublicClientApplicationBuilder GetBuilderWithAuthority(PublicClientApplicationBuilder builder, Microsoft365Environment azureEnvironment)
        {
            if (azureEnvironment == Microsoft365Environment.Production)
            {
                var azureADEndPoint = $"https://{CloudManager.GetAzureADLoginAuthority(azureEnvironment)}";
                builder = builder.WithAuthority($"{azureADEndPoint}/organizations");
            }
            else
            {
                switch (azureEnvironment)
                {
                    case Microsoft365Environment.USGovernment:
                    case Microsoft365Environment.USGovernmentDoD:
                    case Microsoft365Environment.USGovernmentHigh:
                        {
                            builder = builder.WithAuthority(AzureCloudInstance.AzureUsGovernment, AadAuthorityAudience.AzureAdMyOrg);
                            break;
                        }
                    case Microsoft365Environment.Germany:
                        {
                            builder = builder.WithAuthority(AzureCloudInstance.AzureGermany, AadAuthorityAudience.AzureAdMyOrg);
                            break;
                        }
                    case Microsoft365Environment.China:
                        {
                            builder = builder.WithAuthority(AzureCloudInstance.AzureChina, AadAuthorityAudience.AzureAdMyOrg);
                            break;
                        }
                }
            }
            return builder;
        }

        private ConfidentialClientApplicationBuilder GetBuilderWithAuthority(ConfidentialClientApplicationBuilder builder, Microsoft365Environment azureEnvironment, string tenantId = "")
        {
            if (azureEnvironment == Microsoft365Environment.Production)
            {
                var azureADEndPoint = $"https://{CloudManager.GetAzureADLoginAuthority(azureEnvironment)}";
                if (!string.IsNullOrEmpty(tenantId))
                {
                    builder = builder.WithAuthority($"{azureADEndPoint}/organizations", tenantId);
                }
                else
                {
                    builder = builder.WithAuthority($"{azureADEndPoint}/organizations");
                }
            }
            else
            {
                switch (azureEnvironment)
                {
                    case Microsoft365Environment.USGovernment:
                    case Microsoft365Environment.USGovernmentDoD:
                    case Microsoft365Environment.USGovernmentHigh:
                        {
                            builder = builder.WithAuthority(AzureCloudInstance.AzureUsGovernment, AadAuthorityAudience.AzureAdMyOrg);
                            break;
                        }
                    case Microsoft365Environment.Germany:
                        {
                            builder = builder.WithAuthority(AzureCloudInstance.AzureGermany, AadAuthorityAudience.AzureAdMyOrg);
                            break;
                        }
                    case Microsoft365Environment.China:
                        {
                            builder = builder.WithAuthority(AzureCloudInstance.AzureChina, AadAuthorityAudience.AzureAdMyOrg);
                            break;
                        }
                }
            }
            return builder;
        }

        private X509Certificate2 LoadCertificate(string? certPathLocation, string? certFile, string? certPassword)
        {
            if (!string.IsNullOrEmpty(certPathLocation))
            {
                // Did we get a three part certificate path (= local stored cert)
                var certPath = certPathLocation.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                if (certPath.Length == 3 && (certPath[1].Equals("CurrentUser", StringComparison.InvariantCultureIgnoreCase) || certPath[1].Equals("LocalMachine", StringComparison.InvariantCultureIgnoreCase)))
                {
                    // Load the Cert based upon this
                    string certThumbPrint = certPath[2].ToUpper();

                    _ = Enum.TryParse(certPath[0], out StoreName storeName);
                    _ = Enum.TryParse(certPath[1], out StoreLocation storeLocation);

                    var store = new X509Store(storeName, storeLocation);
                    store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                    var certificateCollection = store.Certificates.Find(X509FindType.FindByThumbprint, certThumbPrint, false);

                    store.Close();

                    foreach (var certificate in certificateCollection)
                    {
                        if (certificate.Thumbprint == certThumbPrint)
                        {
                            return certificate;
                        }
                    }
                }

                throw new Exception($"Certificate ciould not be loaded with using this path information {certPathLocation}");
            }
            else
            {
                if (!File.Exists(certFile))
                {
                    throw new FileNotFoundException($"Certificate file {certFile} does not exist");
                }

                using (var certfile = File.OpenRead(certFile))
                {
                    var certificateBytes = new byte[certfile.Length];
                    certfile.Read(certificateBytes, 0, (int)certfile.Length);
                    // Don't dispose the cert as that will lead to "m_safeCertContext is an invalid handle" errors when the confidential client actually uses the cert
                    return new X509Certificate2(certificateBytes,
                                                certPassword,
                                                X509KeyStorageFlags.Exportable |
                                                X509KeyStorageFlags.MachineKeySet |
                                                X509KeyStorageFlags.PersistKeySet);
                }
            }
        }
    }
}
