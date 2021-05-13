using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CERA.DataOperation.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_AppServicePlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResourceGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_AppServicePlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryPhone = table.Column<int>(type: "int", nullable: false),
                    ClientDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_CloudPlugIns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CloudProviderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssemblyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DllPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullyQualifiedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateEnabled = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DevContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupportContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_CloudPlugIns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Compliances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssessmentType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Compliances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Disks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResourceGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Disks", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_RateCard",
                columns: table => new
                {
                    MeterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IncludedQuantity = table.Column<float>(type: "real", nullable: false),
                    MeterCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeterRegion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeterStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeterSubCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_RateCard", x => x.MeterId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ResourceGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    provisioningstate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ResourceGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ResourceHealth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvailabilityState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ResourceHealth", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Resources",
                columns: table => new
                {
                    ResourceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResourceProviderNameSpace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResourceGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResourceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tags = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Resources", x => x.ResourceID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_SqlServer",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResourceGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_SqlServer", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_StorageAccounts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResourceGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StorageAccounts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Subscription",
                columns: table => new
                {
                    SubscriptionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Subscription", x => x.SubscriptionId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Tenants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_UsageDetails",
                columns: table => new
                {
                    UsageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    billingPeriodId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    usageStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usageEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    instanceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    instanceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    instanceLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    meterId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    usageQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    pretaxCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isEstimated = table.Column<bool>(type: "bit", nullable: false),
                    subscriptionGuid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    subscriptionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    product = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    consumedService = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    partNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    resourceGuid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    offerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    chargesBilledSeparately = table.Column<bool>(type: "bit", nullable: false),
                    meterDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    additionalProperties = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_UsageDetails", x => x.UsageId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_VirtualMachines",
                columns: table => new
                {
                    VMId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VMName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResourceGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_VirtualMachines", x => x.VMId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_WebApps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResourceGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_WebApps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ClientCloudPlugins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PlugInId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TenantId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientSecret = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientCloudPlugins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_ClientCloudPlugins_tbl_Clients_ClientId1",
                        column: x => x.ClientId1,
                        principalTable: "tbl_Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_ClientCloudPlugins_tbl_CloudPlugIns_PlugInId",
                        column: x => x.PlugInId,
                        principalTable: "tbl_CloudPlugIns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientCloudPlugins_ClientId1",
                table: "tbl_ClientCloudPlugins",
                column: "ClientId1");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientCloudPlugins_PlugInId",
                table: "tbl_ClientCloudPlugins",
                column: "PlugInId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_AppServicePlans");

            migrationBuilder.DropTable(
                name: "tbl_ClientCloudPlugins");

            migrationBuilder.DropTable(
                name: "tbl_Compliances");

            migrationBuilder.DropTable(
                name: "tbl_Disks");

            migrationBuilder.DropTable(
                name: "tbl_RateCard");

            migrationBuilder.DropTable(
                name: "tbl_ResourceGroups");

            migrationBuilder.DropTable(
                name: "tbl_ResourceHealth");

            migrationBuilder.DropTable(
                name: "tbl_Resources");

            migrationBuilder.DropTable(
                name: "tbl_SqlServer");

            migrationBuilder.DropTable(
                name: "tbl_StorageAccounts");

            migrationBuilder.DropTable(
                name: "tbl_Subscription");

            migrationBuilder.DropTable(
                name: "tbl_Tenants");

            migrationBuilder.DropTable(
                name: "tbl_UsageDetails");

            migrationBuilder.DropTable(
                name: "tbl_VirtualMachines");

            migrationBuilder.DropTable(
                name: "tbl_WebApps");

            migrationBuilder.DropTable(
                name: "tbl_Clients");

            migrationBuilder.DropTable(
                name: "tbl_CloudPlugIns");
        }
    }
}
