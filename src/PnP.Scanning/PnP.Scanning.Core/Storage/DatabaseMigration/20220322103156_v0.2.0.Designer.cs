﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PnP.Scanning.Core.Storage;

#nullable disable

namespace PnP.Scanning.Core.Storage.DatabaseMigration
{
    [DbContext(typeof(ScanContext))]
    [Migration("20220322103156_v0.2.0")]
    partial class v020
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.2");

            modelBuilder.Entity("PnP.Scanning.Core.Storage.Cache", b =>
                {
                    b.Property<Guid>("ScanId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Key")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("ScanId", "Key");

                    b.HasIndex("ScanId", "Key")
                        .IsUnique();

                    b.ToTable("Cache");
                });

            modelBuilder.Entity("PnP.Scanning.Core.Storage.History", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Event")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Message")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ScanId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ScanId", "Event", "EventDate")
                        .IsUnique();

                    b.ToTable("History");
                });

            modelBuilder.Entity("PnP.Scanning.Core.Storage.Property", b =>
                {
                    b.Property<Guid>("ScanId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("ScanId", "Name");

                    b.HasIndex("ScanId", "Name")
                        .IsUnique();

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("PnP.Scanning.Core.Storage.Scan", b =>
                {
                    b.Property<Guid>("ScanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CLIApplicationId")
                        .HasColumnType("TEXT");

                    b.Property<string>("CLIAuthMode")
                        .HasColumnType("TEXT");

                    b.Property<string>("CLICertFile")
                        .HasColumnType("TEXT");

                    b.Property<string>("CLICertFilePassword")
                        .HasColumnType("TEXT");

                    b.Property<string>("CLICertPath")
                        .HasColumnType("TEXT");

                    b.Property<string>("CLIEnvironment")
                        .HasColumnType("TEXT");

                    b.Property<string>("CLIMode")
                        .HasColumnType("TEXT");

                    b.Property<string>("CLISiteFile")
                        .HasColumnType("TEXT");

                    b.Property<string>("CLISiteList")
                        .HasColumnType("TEXT");

                    b.Property<string>("CLITenant")
                        .HasColumnType("TEXT");

                    b.Property<string>("CLITenantId")
                        .HasColumnType("TEXT");

                    b.Property<int>("CLIThreads")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("PostScanStatus")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PreScanStatus")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Version")
                        .HasColumnType("TEXT");

                    b.HasKey("ScanId");

                    b.ToTable("Scans");
                });

            modelBuilder.Entity("PnP.Scanning.Core.Storage.SiteCollection", b =>
                {
                    b.Property<Guid>("ScanId")
                        .HasColumnType("TEXT");

                    b.Property<string>("SiteUrl")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Error")
                        .HasColumnType("TEXT");

                    b.Property<string>("StackTrace")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("ScanId", "SiteUrl");

                    b.HasIndex("ScanId", "SiteUrl")
                        .IsUnique();

                    b.ToTable("SiteCollections");
                });

            modelBuilder.Entity("PnP.Scanning.Core.Storage.SyntexContentType", b =>
                {
                    b.Property<Guid>("ScanId")
                        .HasColumnType("TEXT");

                    b.Property<string>("SiteUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("WebUrl")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ListId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ContentTypeId")
                        .HasColumnType("TEXT");

                    b.Property<int>("FieldCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FileCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Group")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Hidden")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ListContentTypeId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("ScanId", "SiteUrl", "WebUrl", "ListId", "ContentTypeId");

                    b.HasIndex("ScanId", "ContentTypeId");

                    b.HasIndex("ScanId", "SiteUrl", "WebUrl", "ListId", "ContentTypeId")
                        .IsUnique();

                    b.ToTable("SyntexContentTypes");
                });

            modelBuilder.Entity("PnP.Scanning.Core.Storage.SyntexContentTypeField", b =>
                {
                    b.Property<Guid>("ScanId")
                        .HasColumnType("TEXT");

                    b.Property<string>("SiteUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("WebUrl")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ListId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ContentTypeId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("FieldId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Hidden")
                        .HasColumnType("INTEGER");

                    b.Property<string>("InternalName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Required")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("TermSetId")
                        .HasColumnType("TEXT");

                    b.Property<string>("TypeAsString")
                        .HasColumnType("TEXT");

                    b.HasKey("ScanId", "SiteUrl", "WebUrl", "ListId", "ContentTypeId", "FieldId");

                    b.HasIndex("ScanId", "SiteUrl", "WebUrl", "ListId", "ContentTypeId", "FieldId")
                        .IsUnique();

                    b.ToTable("SyntexContentTypeFields");
                });

            modelBuilder.Entity("PnP.Scanning.Core.Storage.SyntexContentTypeSummary", b =>
                {
                    b.Property<Guid>("ScanId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ContentTypeId")
                        .HasColumnType("TEXT");

                    b.Property<int>("FieldCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FileCount")
                        .HasColumnType("INTEGER");

                    b.Property<double>("FileCountLowerQuartile")
                        .HasColumnType("REAL");

                    b.Property<double>("FileCountMax")
                        .HasColumnType("REAL");

                    b.Property<double>("FileCountMean")
                        .HasColumnType("REAL");

                    b.Property<double>("FileCountMedian")
                        .HasColumnType("REAL");

                    b.Property<double>("FileCountMin")
                        .HasColumnType("REAL");

                    b.Property<double>("FileCountStandardDeviation")
                        .HasColumnType("REAL");

                    b.Property<double>("FileCountUpperQuartile")
                        .HasColumnType("REAL");

                    b.Property<string>("Group")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Hidden")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsSyntexContentType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ListCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("SyntexModelDriveId")
                        .HasColumnType("TEXT");

                    b.Property<string>("SyntexModelObjectId")
                        .HasColumnType("TEXT");

                    b.HasKey("ScanId", "ContentTypeId");

                    b.HasIndex("ScanId", "ContentTypeId")
                        .IsUnique();

                    b.ToTable("SyntexContentTypeOverview");
                });

            modelBuilder.Entity("PnP.Scanning.Core.Storage.SyntexField", b =>
                {
                    b.Property<Guid>("ScanId")
                        .HasColumnType("TEXT");

                    b.Property<string>("SiteUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("WebUrl")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ListId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("FieldId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Hidden")
                        .HasColumnType("INTEGER");

                    b.Property<string>("InternalName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Required")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("TermSetId")
                        .HasColumnType("TEXT");

                    b.Property<string>("TypeAsString")
                        .HasColumnType("TEXT");

                    b.HasKey("ScanId", "SiteUrl", "WebUrl", "ListId", "FieldId");

                    b.HasIndex("ScanId", "SiteUrl", "WebUrl", "ListId", "FieldId")
                        .IsUnique();

                    b.ToTable("SyntexFields");
                });

            modelBuilder.Entity("PnP.Scanning.Core.Storage.SyntexList", b =>
                {
                    b.Property<Guid>("ScanId")
                        .HasColumnType("TEXT");

                    b.Property<string>("SiteUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("WebUrl")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ListId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("AllowContentTypes")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ContentTypeCount")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<int>("FieldCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FlowInstanceCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemCount")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastChanged")
                        .HasColumnType("TEXT");

                    b.Property<int>("LastChangedMonth")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastChangedMonthString")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastChangedQuarter")
                        .HasColumnType("TEXT");

                    b.Property<int>("LastChangedYear")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ListExperienceOptions")
                        .HasColumnType("TEXT");

                    b.Property<string>("ListServerRelativeUrl")
                        .HasColumnType("TEXT");

                    b.Property<int>("ListTemplate")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ListTemplateString")
                        .HasColumnType("TEXT");

                    b.Property<int>("RetentionLabelCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<int>("WorkflowInstanceCount")
                        .HasColumnType("INTEGER");

                    b.HasKey("ScanId", "SiteUrl", "WebUrl", "ListId");

                    b.HasIndex("ScanId", "SiteUrl", "WebUrl", "ListId")
                        .IsUnique();

                    b.ToTable("SyntexLists");
                });

            modelBuilder.Entity("PnP.Scanning.Core.Storage.SyntexModelUsage", b =>
                {
                    b.Property<Guid>("ScanId")
                        .HasColumnType("TEXT");

                    b.Property<string>("SiteUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("WebUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("Classifier")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TargetSiteId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TargetWebId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TargetListId")
                        .HasColumnType("TEXT");

                    b.Property<double>("AverageConfidenceScore")
                        .HasColumnType("REAL");

                    b.Property<int>("ClassifiedItemCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NotProcessedItemCount")
                        .HasColumnType("INTEGER");

                    b.HasKey("ScanId", "SiteUrl", "WebUrl", "Classifier", "TargetSiteId", "TargetWebId", "TargetListId");

                    b.HasIndex("ScanId", "SiteUrl", "WebUrl", "Classifier", "TargetSiteId", "TargetWebId", "TargetListId");

                    b.ToTable("SyntexModelUsage");
                });

            modelBuilder.Entity("PnP.Scanning.Core.Storage.TestDelay", b =>
                {
                    b.Property<Guid>("ScanId")
                        .HasColumnType("TEXT");

                    b.Property<string>("SiteUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("WebUrl")
                        .HasColumnType("TEXT");

                    b.Property<int>("Delay1")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Delay2")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Delay3")
                        .HasColumnType("INTEGER");

                    b.Property<string>("WebIdString")
                        .HasColumnType("TEXT");

                    b.HasKey("ScanId", "SiteUrl", "WebUrl");

                    b.HasIndex("ScanId", "SiteUrl", "WebUrl")
                        .IsUnique();

                    b.ToTable("TestDelays");
                });

            modelBuilder.Entity("PnP.Scanning.Core.Storage.Web", b =>
                {
                    b.Property<Guid>("ScanId")
                        .HasColumnType("TEXT");

                    b.Property<string>("SiteUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("WebUrl")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Error")
                        .HasColumnType("TEXT");

                    b.Property<string>("StackTrace")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Template")
                        .HasColumnType("TEXT");

                    b.Property<string>("WebUrlAbsolute")
                        .HasColumnType("TEXT");

                    b.HasKey("ScanId", "SiteUrl", "WebUrl");

                    b.HasIndex("ScanId", "SiteUrl", "WebUrl")
                        .IsUnique();

                    b.ToTable("Webs");
                });

            modelBuilder.Entity("PnP.Scanning.Core.Storage.Workflow", b =>
                {
                    b.Property<Guid>("ScanId")
                        .HasColumnType("TEXT");

                    b.Property<string>("SiteUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("WebUrl")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DefinitionId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SubscriptionId")
                        .HasColumnType("TEXT");

                    b.Property<int>("ActionCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CancelledInstances")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CancellingInstances")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CompletedInstances")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ContentTypeId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ContentTypeName")
                        .HasColumnType("TEXT");

                    b.Property<string>("DefinitionDescription")
                        .HasColumnType("TEXT");

                    b.Property<string>("DefinitionName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Enabled")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasSubscriptions")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsOOBWorkflow")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastDefinitionEdit")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastSubscriptionEdit")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ListId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ListTitle")
                        .HasColumnType("TEXT");

                    b.Property<string>("ListUrl")
                        .HasColumnType("TEXT");

                    b.Property<int>("NotStartedInstances")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RestrictToType")
                        .HasColumnType("TEXT");

                    b.Property<string>("Scope")
                        .HasColumnType("TEXT");

                    b.Property<int>("StartedInstances")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SubscriptionName")
                        .HasColumnType("TEXT");

                    b.Property<int>("SuspendedInstances")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TerminatedInstances")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TotalInstances")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UnsupportedActionCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UnsupportedActionsInFlow")
                        .HasColumnType("TEXT");

                    b.Property<string>("UsedActions")
                        .HasColumnType("TEXT");

                    b.Property<string>("UsedTriggers")
                        .HasColumnType("TEXT");

                    b.HasKey("ScanId", "SiteUrl", "WebUrl", "DefinitionId", "SubscriptionId");

                    b.HasIndex("ScanId", "SiteUrl", "WebUrl", "DefinitionId", "SubscriptionId")
                        .IsUnique();

                    b.ToTable("Workflows");
                });
#pragma warning restore 612, 618
        }
    }
}
