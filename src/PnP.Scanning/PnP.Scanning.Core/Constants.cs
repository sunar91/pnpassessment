﻿namespace PnP.Scanning.Core
{
    internal static class Constants
    {
        #region commandline
        internal const string StartMode = "mode";
        internal const string StartTenant = "tenant";
        internal const string StartEnvironment = "environment";
        internal const string StartSitesList = "siteslist";
        internal const string StartSitesFile = "sitesfile";
        internal const string StartAuthMode = "authmode";
        internal const string StartApplicationId = "applicationid";
        internal const string StartCertPath = "certpath";
        internal const string StartCertFile = "certfile";
        internal const string StartCertPassword = "certpassword";

        internal const string StartTestNumberOfSites = "testnumberofsites";

        internal const string ListRunning = "running";
        internal const string ListPaused = "paused";
        internal const string ListFinished = "finished";
        internal const string ListTerminated = "terminated";

        internal const string PauseScanId = "scanid";
        internal const string PauseAll = "all";
        #endregion

        #region Status
        internal const string MessageInformation = "Information";
        internal const string MessageWarning = "Warning";
        internal const string MessageError = "Error";
        #endregion
    }
}