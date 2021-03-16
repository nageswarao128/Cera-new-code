﻿// <auto-generated />
using System;
using CERA.DataOperation.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CERA.DataOperation.Migrations
{
    [DbContext(typeof(CeraDbContext))]
    partial class CeraDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CERA.Entities.Models.CeraSubscription", b =>
                {
                    b.Property<string>("SubscriptionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenantID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubscriptionId");

                    b.ToTable("tbl_Subscription");
                });

            modelBuilder.Entity("CERA.Entities.Models.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ClientDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("PrimaryAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrimaryContactName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrimaryEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PrimaryPhone")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("tbl_Clients");
                });

            modelBuilder.Entity("CERA.Entities.Models.ClientCloudPlugin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ClientId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ClientId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ClientSecret")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<Guid?>("PlugInId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TenantId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId1");

                    b.HasIndex("PlugInId");

                    b.ToTable("tbl_ClientCloudPlugins");
                });

            modelBuilder.Entity("CERA.Entities.Models.CloudPlugIn", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AssemblyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClassName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CloudProviderName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateEnabled")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DevContact")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DllPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullyQualifiedName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ServiceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupportContact")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("tbl_CloudPlugIns");
                });

            modelBuilder.Entity("CERA.Entities.Models.ClientCloudPlugin", b =>
                {
                    b.HasOne("CERA.Entities.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId1");

                    b.HasOne("CERA.Entities.Models.CloudPlugIn", "PlugIn")
                        .WithMany()
                        .HasForeignKey("PlugInId");

                    b.Navigation("Client");

                    b.Navigation("PlugIn");
                });
#pragma warning restore 612, 618
        }
    }
}
