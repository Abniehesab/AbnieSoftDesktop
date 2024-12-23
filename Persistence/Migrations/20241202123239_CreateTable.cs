using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kols",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    KolCode = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    AccKolCode = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    KolName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    FirstTotalDebtorValue = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    FirstTotalCreditorValue = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    TotalDebtorValue = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    TotalCreditorValue = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    NatureFinalBalance = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    Finalbalance = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    BusinessId = table.Column<long>(type: "INTEGER", nullable: false),
                    AccGroupId = table.Column<long>(type: "INTEGER", nullable: false),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kols", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MaterialGroupCode = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    MaterialGroupTitle = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    BusinessId = table.Column<long>(type: "INTEGER", nullable: true),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialUnits",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MaterialUnitCode = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    MaterialUnitTitle = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    BusinessId = table.Column<long>(type: "INTEGER", nullable: true),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tafzili2s",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tafzili2Code = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    Tafzili2Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    FirstTotalDebtorValue = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    FirstTotalCreditorValue = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    TotalDebtorValue = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    TotalCreditorValue = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    NatureFinalBalance = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    Finalbalance = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    BusinessId = table.Column<long>(type: "INTEGER", nullable: false),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tafzili2s", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tafzili3s",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tafzili3Code = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    Tafzili3Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    FirstTotalDebtorValue = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    FirstTotalCreditorValue = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    TotalDebtorValue = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    TotalCreditorValue = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    NatureFinalBalance = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    Finalbalance = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    BusinessId = table.Column<long>(type: "INTEGER", nullable: false),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tafzili3s", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tafzilis",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TafziliCode = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    TafziliName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    AccTafziliCode = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    TafziliType = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    TafziliRef = table.Column<long>(type: "INTEGER", maxLength: 100, nullable: false),
                    SharedKey = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    FirstTotalDebtorValue = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    FirstTotalCreditorValue = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    TotalDebtorValue = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    TotalCreditorValue = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    NatureFinalBalance = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    Finalbalance = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    NatureFinalBalanceTafzili2 = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    FinalbalanceTafzili2 = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    NatureFinalBalanceTafzili3 = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    FinalbalanceTafzili3 = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    TafziliGroupId = table.Column<long>(type: "INTEGER", nullable: false),
                    BusinessId = table.Column<long>(type: "INTEGER", nullable: false),
                    Tafzili2 = table.Column<bool>(type: "INTEGER", nullable: false),
                    FirstTotalDebtorValueTafzili2 = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    FirstTotalCreditorValueTafzili2 = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    TotalDebtorValueTafzili2 = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    TotalCreditorValueTafzili2 = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    Tafzili3 = table.Column<bool>(type: "INTEGER", nullable: false),
                    FirstTotalDebtorValueTafzili3 = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    FirstTotalCreditorValueTafzili3 = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    TotalDebtorValueTafzili3 = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    TotalCreditorValueTafzili3 = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tafzilis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TafziliTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TafziliTypeCode = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    TafziliTypeName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TafziliTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Moeins",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MoeinCode = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    AccMoeinCode = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    MoeinName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    FirstTotalDebtorValue = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    FirstTotalCreditorValue = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    TotalDebtorValue = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    TotalCreditorValue = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    NatureFinalBalance = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    Finalbalance = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    AccNatureId = table.Column<long>(type: "INTEGER", nullable: false),
                    BusinessId = table.Column<long>(type: "INTEGER", nullable: false),
                    KolId = table.Column<long>(type: "INTEGER", nullable: false),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moeins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Moeins_Kols_KolId",
                        column: x => x.KolId,
                        principalTable: "Kols",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MaterialType = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    MaterialCode = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    MaterialTitle = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    MaterialDescription = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    MaterialUnitId = table.Column<long>(type: "INTEGER", nullable: false),
                    MaterialGroupId = table.Column<long>(type: "INTEGER", nullable: false),
                    BusinessId = table.Column<long>(type: "INTEGER", nullable: true),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materials_MaterialGroups_MaterialGroupId",
                        column: x => x.MaterialGroupId,
                        principalTable: "MaterialGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Materials_MaterialUnits_MaterialUnitId",
                        column: x => x.MaterialUnitId,
                        principalTable: "MaterialUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tenders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TenderCode = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    TenderTitle = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    TenderNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    TenderDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    TenderType = table.Column<int>(type: "INTEGER", nullable: true),
                    TenderStartDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    TenderEndDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    TenderPeriod = table.Column<int>(type: "INTEGER", nullable: true),
                    AmountWork = table.Column<string>(type: "TEXT", nullable: true),
                    PlaceOfWork = table.Column<string>(type: "TEXT", nullable: true),
                    WarrancyAmount = table.Column<decimal>(type: "decimal(19,4)", nullable: true),
                    PrePayment = table.Column<decimal>(type: "decimal(19,4)", nullable: true),
                    PlaceOfWorksaleOfTenderDocuments = table.Column<string>(type: "TEXT", nullable: true),
                    ReadingTenderOffersDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ReadingTenderOffersPlace = table.Column<string>(type: "TEXT", nullable: true),
                    ProposedPrice = table.Column<decimal>(type: "decimal(19,4)", nullable: true),
                    TenderOwnerId = table.Column<long>(type: "INTEGER", nullable: true),
                    BusinessId = table.Column<long>(type: "INTEGER", nullable: true),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tenders_Tafzilis_TenderOwnerId",
                        column: x => x.TenderOwnerId,
                        principalTable: "Tafzilis",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TafziliGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TafziliGroupCode = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    TafziliGroupName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    FirstTotalDebtorValue = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    FirstTotalCreditorValue = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    TotalDebtorValue = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    TotalCreditorValue = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    NatureFinalBalance = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    Finalbalance = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    BusinessId = table.Column<long>(type: "INTEGER", nullable: false),
                    TafziliTypeId = table.Column<long>(type: "INTEGER", nullable: false),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TafziliGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TafziliGroups_TafziliTypes_TafziliTypeId",
                        column: x => x.TafziliTypeId,
                        principalTable: "TafziliTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccDocmentDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccDocumentRowNumber = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    AccDocumentRowDate = table.Column<DateTime>(type: "TEXT", maxLength: 100, nullable: false),
                    AccDocumentNumber = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    Inflection = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    AccDocumentType = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    AccDocumentRowDescription = table.Column<string>(type: "TEXT", maxLength: 5000, nullable: false),
                    DebtorValue = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    CreditorValue = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    IsUpdate = table.Column<bool>(type: "INTEGER", nullable: false),
                    FKAccDocumentRowLastModifierId = table.Column<long>(name: "FK_AccDocumentRowLastModifierId", type: "INTEGER", nullable: true),
                    BusinessId = table.Column<long>(type: "INTEGER", nullable: false),
                    AccDocumentId = table.Column<long>(type: "INTEGER", nullable: true),
                    KolId = table.Column<long>(type: "INTEGER", nullable: true),
                    MoeinId = table.Column<long>(type: "INTEGER", nullable: true),
                    TafziliId = table.Column<long>(type: "INTEGER", nullable: true),
                    Tafzili2Id = table.Column<long>(type: "INTEGER", nullable: true),
                    Tafzili3Id = table.Column<long>(type: "INTEGER", nullable: true),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccDocmentDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccDocmentDetails_Kols_KolId",
                        column: x => x.KolId,
                        principalTable: "Kols",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccDocmentDetails_Moeins_MoeinId",
                        column: x => x.MoeinId,
                        principalTable: "Moeins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccDocmentDetails_Tafzili2s_Tafzili2Id",
                        column: x => x.Tafzili2Id,
                        principalTable: "Tafzili2s",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccDocmentDetails_Tafzili3s_Tafzili3Id",
                        column: x => x.Tafzili3Id,
                        principalTable: "Tafzili3s",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccDocmentDetails_Tafzilis_TafziliId",
                        column: x => x.TafziliId,
                        principalTable: "Tafzilis",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ContractDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ContractCode = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    ContractNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    ContractType = table.Column<int>(type: "INTEGER", nullable: true),
                    ContractTitle = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ContractLocation = table.Column<string>(type: "TEXT", nullable: true),
                    ContractStartDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ContractEndDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ContractPeriod = table.Column<int>(type: "INTEGER", nullable: true),
                    ContractPrice = table.Column<decimal>(type: "decimal(19,4)", nullable: true),
                    TerminationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ContractAgreements = table.Column<string>(type: "TEXT", nullable: true),
                    CostEstimation = table.Column<decimal>(type: "decimal(19,4)", nullable: true),
                    PercentageOfChanges = table.Column<float>(type: "REAL", nullable: true),
                    TenderId = table.Column<long>(type: "INTEGER", nullable: true),
                    ContractorId = table.Column<long>(type: "INTEGER", nullable: true),
                    MoeinId = table.Column<long>(type: "INTEGER", nullable: true),
                    BusinessId = table.Column<long>(type: "INTEGER", nullable: true),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_Moeins_MoeinId",
                        column: x => x.MoeinId,
                        principalTable: "Moeins",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectStatusFactors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectStatusFactorCode = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    ProjectStatusFactorTitle = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ProjectStatusFactorType = table.Column<int>(type: "INTEGER", nullable: true),
                    ProjectStatusFactorUser = table.Column<int>(type: "INTEGER", nullable: true),
                    ProjectStatusFactorPercent = table.Column<float>(type: "REAL", nullable: true),
                    BusinessId = table.Column<long>(type: "INTEGER", nullable: true),
                    MoeinId = table.Column<long>(type: "INTEGER", nullable: false),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectStatusFactors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectStatusFactors_Moeins_MoeinId",
                        column: x => x.MoeinId,
                        principalTable: "Moeins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StoreCode = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    StoreTitle = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    StoreAdmin = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    StoreAddress = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    BusinessId = table.Column<long>(type: "INTEGER", nullable: true),
                    MoeinId = table.Column<long>(type: "INTEGER", nullable: false),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stores_Moeins_MoeinId",
                        column: x => x.MoeinId,
                        principalTable: "Moeins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MoeinTafziliGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MoeinId = table.Column<long>(type: "INTEGER", nullable: false),
                    TafziliGroupId = table.Column<long>(type: "INTEGER", nullable: false),
                    BusinessId = table.Column<long>(type: "INTEGER", nullable: false),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoeinTafziliGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoeinTafziliGroups_Moeins_MoeinId",
                        column: x => x.MoeinId,
                        principalTable: "Moeins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoeinTafziliGroups_TafziliGroups_TafziliGroupId",
                        column: x => x.TafziliGroupId,
                        principalTable: "TafziliGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentCheques",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PaymentNumber = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    PaymentChequeRowNumber = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    PaymentChequeNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PaymentChequeSayyadiNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PaymentChequeDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PaymentChequeDescription = table.Column<string>(type: "TEXT", nullable: true),
                    PaymentChequeValue = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    IsFirstPeriod = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsGuarantee = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsUpdate = table.Column<bool>(type: "INTEGER", nullable: false),
                    PaymentChequeLastState = table.Column<int>(type: "INTEGER", nullable: true),
                    BusinessId = table.Column<long>(type: "INTEGER", nullable: false),
                    PaymentId = table.Column<long>(type: "INTEGER", nullable: true),
                    BankId = table.Column<long>(type: "INTEGER", nullable: true),
                    MoeinForBankId = table.Column<long>(type: "INTEGER", nullable: true),
                    ReciverId = table.Column<long>(type: "INTEGER", nullable: true),
                    MoeinForReciverId = table.Column<long>(type: "INTEGER", nullable: true),
                    ContractId = table.Column<long>(type: "INTEGER", nullable: true),
                    MoeinForPaymentChequeId = table.Column<long>(type: "INTEGER", nullable: true),
                    AccDocumentId = table.Column<long>(type: "INTEGER", nullable: true),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentCheques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentCheques_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentCheques_Moeins_MoeinForBankId",
                        column: x => x.MoeinForBankId,
                        principalTable: "Moeins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentCheques_Moeins_MoeinForPaymentChequeId",
                        column: x => x.MoeinForPaymentChequeId,
                        principalTable: "Moeins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentCheques_Moeins_MoeinForReciverId",
                        column: x => x.MoeinForReciverId,
                        principalTable: "Moeins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentCheques_Tafzilis_BankId",
                        column: x => x.BankId,
                        principalTable: "Tafzilis",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentCheques_Tafzilis_ReciverId",
                        column: x => x.ReciverId,
                        principalTable: "Tafzilis",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReceiveCheques",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReceiveNumber = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    ReceiveChequeRowNumber = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    ReceiveChequeNumber = table.Column<string>(type: "TEXT", nullable: true),
                    ReceiveChequeSayyadiNumber = table.Column<string>(type: "TEXT", nullable: true),
                    ReceiveChequeDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ReceiveDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PaymentToDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ReceiveChequeDescription = table.Column<string>(type: "TEXT", nullable: true),
                    ReceiveChequeValue = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    IsFirstPeriod = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsGuarantee = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsUpdate = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReceiveChequeLastState = table.Column<int>(type: "INTEGER", nullable: true),
                    ReceiveChequeBeforeLastState = table.Column<int>(type: "INTEGER", nullable: true),
                    BusinessId = table.Column<long>(type: "INTEGER", nullable: false),
                    ReceiveId = table.Column<long>(type: "INTEGER", nullable: true),
                    PaymentId = table.Column<long>(type: "INTEGER", nullable: true),
                    BankId = table.Column<long>(type: "INTEGER", nullable: true),
                    MoeinForBankId = table.Column<long>(type: "INTEGER", nullable: true),
                    PayerId = table.Column<long>(type: "INTEGER", nullable: true),
                    MoeinForPayerId = table.Column<long>(type: "INTEGER", nullable: true),
                    PaymentToId = table.Column<long>(type: "INTEGER", nullable: true),
                    MoeinForPaymentToId = table.Column<long>(type: "INTEGER", nullable: true),
                    ContractId = table.Column<long>(type: "INTEGER", nullable: true),
                    MoeinForReceiveChequeId = table.Column<long>(type: "INTEGER", nullable: true),
                    AccDocumentId = table.Column<long>(type: "INTEGER", nullable: true),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiveCheques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiveCheques_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceiveCheques_Moeins_MoeinForBankId",
                        column: x => x.MoeinForBankId,
                        principalTable: "Moeins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceiveCheques_Moeins_MoeinForPayerId",
                        column: x => x.MoeinForPayerId,
                        principalTable: "Moeins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceiveCheques_Moeins_MoeinForPaymentToId",
                        column: x => x.MoeinForPaymentToId,
                        principalTable: "Moeins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceiveCheques_Moeins_MoeinForReceiveChequeId",
                        column: x => x.MoeinForReceiveChequeId,
                        principalTable: "Moeins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceiveCheques_Tafzilis_BankId",
                        column: x => x.BankId,
                        principalTable: "Tafzilis",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceiveCheques_Tafzilis_PayerId",
                        column: x => x.PayerId,
                        principalTable: "Tafzilis",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceiveCheques_Tafzilis_PaymentToId",
                        column: x => x.PaymentToId,
                        principalTable: "Tafzilis",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CostLists",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CostListDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CostListNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    CostListType = table.Column<int>(type: "INTEGER", nullable: true),
                    PaymentType = table.Column<int>(type: "INTEGER", nullable: true),
                    TotallyCostList = table.Column<decimal>(type: "decimal(19,4)", nullable: true),
                    CostListDescription = table.Column<string>(type: "TEXT", nullable: true),
                    IsUpdate = table.Column<bool>(type: "INTEGER", nullable: false),
                    FKAccDocumentRowLastModifierId = table.Column<long>(name: "FK_AccDocumentRowLastModifierId", type: "INTEGER", nullable: true),
                    BusinessId = table.Column<long>(type: "INTEGER", nullable: false),
                    AccDocumentId = table.Column<long>(type: "INTEGER", nullable: true),
                    ContractId = table.Column<long>(type: "INTEGER", nullable: true),
                    InstantPaymentId = table.Column<long>(type: "INTEGER", nullable: true),
                    MoeinForInstantPaymentId = table.Column<long>(type: "INTEGER", nullable: true),
                    StoreId = table.Column<long>(type: "INTEGER", nullable: true),
                    WorkshopId = table.Column<long>(type: "INTEGER", nullable: true),
                    employerId = table.Column<long>(type: "INTEGER", nullable: true),
                    MoeinForemployerId = table.Column<long>(type: "INTEGER", nullable: true),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostLists_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CostLists_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CostLists_Tafzilis_InstantPaymentId",
                        column: x => x.InstantPaymentId,
                        principalTable: "Tafzilis",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CostLists_Tafzilis_employerId",
                        column: x => x.employerId,
                        principalTable: "Tafzilis",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CostListDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CostListRowNumber = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    CostListRowDate = table.Column<DateTime>(type: "TEXT", maxLength: 100, nullable: false),
                    CostListNumber = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    ReceiptNumber = table.Column<string>(type: "TEXT", nullable: true),
                    AmountMaterial = table.Column<float>(type: "REAL", nullable: true),
                    PriceMaterial = table.Column<decimal>(type: "TEXT", nullable: true),
                    CostListRowDescription = table.Column<string>(type: "TEXT", nullable: false),
                    CostListRowInitialValue = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    CostListRowfinalValue = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    IsUpdate = table.Column<bool>(type: "INTEGER", nullable: false),
                    FKAccDocumentRowLastModifierId = table.Column<long>(name: "FK_AccDocumentRowLastModifierId", type: "INTEGER", nullable: true),
                    BusinessId = table.Column<long>(type: "INTEGER", nullable: false),
                    CostListId = table.Column<long>(type: "INTEGER", nullable: true),
                    AccDocumentId = table.Column<long>(type: "INTEGER", nullable: true),
                    KolId = table.Column<long>(type: "INTEGER", nullable: true),
                    MoeinId = table.Column<long>(type: "INTEGER", nullable: true),
                    TafziliId = table.Column<long>(type: "INTEGER", nullable: true),
                    Tafzili2Id = table.Column<long>(type: "INTEGER", nullable: true),
                    Tafzili3Id = table.Column<long>(type: "INTEGER", nullable: true),
                    SupplierId = table.Column<long>(type: "INTEGER", nullable: true),
                    MoeinForSupplierId = table.Column<long>(type: "INTEGER", nullable: true),
                    BankId = table.Column<long>(type: "INTEGER", nullable: true),
                    MoeinForBankId = table.Column<long>(type: "INTEGER", nullable: true),
                    SideFactorId = table.Column<long>(type: "INTEGER", nullable: true),
                    MoeinForSideFactorId = table.Column<long>(type: "INTEGER", nullable: true),
                    MaterialGroupId = table.Column<long>(type: "INTEGER", nullable: true),
                    MaterialId = table.Column<long>(type: "INTEGER", nullable: true),
                    MaterialUnitId = table.Column<long>(type: "INTEGER", nullable: true),
                    CostListFactorId = table.Column<long>(type: "INTEGER", nullable: true),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostListDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostListDetails_CostLists_CostListId",
                        column: x => x.CostListId,
                        principalTable: "CostLists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CostListDetails_MaterialGroups_MaterialGroupId",
                        column: x => x.MaterialGroupId,
                        principalTable: "MaterialGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CostListDetails_MaterialUnits_MaterialUnitId",
                        column: x => x.MaterialUnitId,
                        principalTable: "MaterialUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CostListDetails_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CostListDetails_Moeins_MoeinId",
                        column: x => x.MoeinId,
                        principalTable: "Moeins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CostListDetails_Tafzili2s_Tafzili2Id",
                        column: x => x.Tafzili2Id,
                        principalTable: "Tafzili2s",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CostListDetails_Tafzili3s_Tafzili3Id",
                        column: x => x.Tafzili3Id,
                        principalTable: "Tafzili3s",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CostListDetails_Tafzilis_BankId",
                        column: x => x.BankId,
                        principalTable: "Tafzilis",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CostListDetails_Tafzilis_SideFactorId",
                        column: x => x.SideFactorId,
                        principalTable: "Tafzilis",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CostListDetails_Tafzilis_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Tafzilis",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CostListDetails_Tafzilis_TafziliId",
                        column: x => x.TafziliId,
                        principalTable: "Tafzilis",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MaterialCirculations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MaterialCirculationType = table.Column<int>(type: "INTEGER", nullable: true),
                    MaterialCirculationRowNumber = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    MaterialCirculationRowDate = table.Column<DateTime>(type: "TEXT", maxLength: 100, nullable: false),
                    MaterialCirculationOperationNumber = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    AmountMaterialEntered = table.Column<float>(type: "REAL", nullable: true),
                    PriceMaterialEntered = table.Column<decimal>(type: "TEXT", nullable: true),
                    AmountMaterialOutput = table.Column<float>(type: "REAL", nullable: true),
                    PriceMaterialOutput = table.Column<decimal>(type: "TEXT", nullable: true),
                    MaterialCirculationRowDescription = table.Column<string>(type: "TEXT", nullable: false),
                    IsUpdate = table.Column<bool>(type: "INTEGER", nullable: false),
                    FKMaterialCirculationRowLastModifierId = table.Column<long>(name: "FK_MaterialCirculationRowLastModifierId", type: "INTEGER", nullable: true),
                    BusinessId = table.Column<long>(type: "INTEGER", nullable: false),
                    ContractId = table.Column<long>(type: "INTEGER", nullable: true),
                    BuyFactorId = table.Column<long>(type: "INTEGER", nullable: true),
                    CostListId = table.Column<long>(type: "INTEGER", nullable: true),
                    MaterialGroupId = table.Column<long>(type: "INTEGER", nullable: true),
                    MaterialId = table.Column<long>(type: "INTEGER", nullable: true),
                    MaterialUnitId = table.Column<long>(type: "INTEGER", nullable: true),
                    SupplierId = table.Column<long>(type: "INTEGER", nullable: true),
                    StoreId = table.Column<long>(type: "INTEGER", nullable: true),
                    AccDocumentId = table.Column<long>(type: "INTEGER", nullable: true),
                    IsDelete = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialCirculations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialCirculations_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaterialCirculations_CostLists_CostListId",
                        column: x => x.CostListId,
                        principalTable: "CostLists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaterialCirculations_MaterialGroups_MaterialGroupId",
                        column: x => x.MaterialGroupId,
                        principalTable: "MaterialGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaterialCirculations_MaterialUnits_MaterialUnitId",
                        column: x => x.MaterialUnitId,
                        principalTable: "MaterialUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaterialCirculations_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaterialCirculations_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaterialCirculations_Tafzilis_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Tafzilis",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccDocmentDetails_KolId",
                table: "AccDocmentDetails",
                column: "KolId");

            migrationBuilder.CreateIndex(
                name: "IX_AccDocmentDetails_MoeinId",
                table: "AccDocmentDetails",
                column: "MoeinId");

            migrationBuilder.CreateIndex(
                name: "IX_AccDocmentDetails_Tafzili2Id",
                table: "AccDocmentDetails",
                column: "Tafzili2Id");

            migrationBuilder.CreateIndex(
                name: "IX_AccDocmentDetails_Tafzili3Id",
                table: "AccDocmentDetails",
                column: "Tafzili3Id");

            migrationBuilder.CreateIndex(
                name: "IX_AccDocmentDetails_TafziliId",
                table: "AccDocmentDetails",
                column: "TafziliId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_MoeinId",
                table: "Contracts",
                column: "MoeinId");

            migrationBuilder.CreateIndex(
                name: "IX_CostListDetails_BankId",
                table: "CostListDetails",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_CostListDetails_CostListId",
                table: "CostListDetails",
                column: "CostListId");

            migrationBuilder.CreateIndex(
                name: "IX_CostListDetails_MaterialGroupId",
                table: "CostListDetails",
                column: "MaterialGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CostListDetails_MaterialId",
                table: "CostListDetails",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_CostListDetails_MaterialUnitId",
                table: "CostListDetails",
                column: "MaterialUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_CostListDetails_MoeinId",
                table: "CostListDetails",
                column: "MoeinId");

            migrationBuilder.CreateIndex(
                name: "IX_CostListDetails_SideFactorId",
                table: "CostListDetails",
                column: "SideFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_CostListDetails_SupplierId",
                table: "CostListDetails",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_CostListDetails_Tafzili2Id",
                table: "CostListDetails",
                column: "Tafzili2Id");

            migrationBuilder.CreateIndex(
                name: "IX_CostListDetails_Tafzili3Id",
                table: "CostListDetails",
                column: "Tafzili3Id");

            migrationBuilder.CreateIndex(
                name: "IX_CostListDetails_TafziliId",
                table: "CostListDetails",
                column: "TafziliId");

            migrationBuilder.CreateIndex(
                name: "IX_CostLists_ContractId",
                table: "CostLists",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_CostLists_employerId",
                table: "CostLists",
                column: "employerId");

            migrationBuilder.CreateIndex(
                name: "IX_CostLists_InstantPaymentId",
                table: "CostLists",
                column: "InstantPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_CostLists_StoreId",
                table: "CostLists",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialCirculations_ContractId",
                table: "MaterialCirculations",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialCirculations_CostListId",
                table: "MaterialCirculations",
                column: "CostListId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialCirculations_MaterialGroupId",
                table: "MaterialCirculations",
                column: "MaterialGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialCirculations_MaterialId",
                table: "MaterialCirculations",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialCirculations_MaterialUnitId",
                table: "MaterialCirculations",
                column: "MaterialUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialCirculations_StoreId",
                table: "MaterialCirculations",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialCirculations_SupplierId",
                table: "MaterialCirculations",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_MaterialGroupId",
                table: "Materials",
                column: "MaterialGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_MaterialUnitId",
                table: "Materials",
                column: "MaterialUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Moeins_KolId",
                table: "Moeins",
                column: "KolId");

            migrationBuilder.CreateIndex(
                name: "IX_MoeinTafziliGroups_MoeinId",
                table: "MoeinTafziliGroups",
                column: "MoeinId");

            migrationBuilder.CreateIndex(
                name: "IX_MoeinTafziliGroups_TafziliGroupId",
                table: "MoeinTafziliGroups",
                column: "TafziliGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCheques_BankId",
                table: "PaymentCheques",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCheques_ContractId",
                table: "PaymentCheques",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCheques_MoeinForBankId",
                table: "PaymentCheques",
                column: "MoeinForBankId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCheques_MoeinForPaymentChequeId",
                table: "PaymentCheques",
                column: "MoeinForPaymentChequeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCheques_MoeinForReciverId",
                table: "PaymentCheques",
                column: "MoeinForReciverId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCheques_ReciverId",
                table: "PaymentCheques",
                column: "ReciverId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectStatusFactors_MoeinId",
                table: "ProjectStatusFactors",
                column: "MoeinId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveCheques_BankId",
                table: "ReceiveCheques",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveCheques_ContractId",
                table: "ReceiveCheques",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveCheques_MoeinForBankId",
                table: "ReceiveCheques",
                column: "MoeinForBankId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveCheques_MoeinForPayerId",
                table: "ReceiveCheques",
                column: "MoeinForPayerId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveCheques_MoeinForPaymentToId",
                table: "ReceiveCheques",
                column: "MoeinForPaymentToId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveCheques_MoeinForReceiveChequeId",
                table: "ReceiveCheques",
                column: "MoeinForReceiveChequeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveCheques_PayerId",
                table: "ReceiveCheques",
                column: "PayerId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveCheques_PaymentToId",
                table: "ReceiveCheques",
                column: "PaymentToId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_MoeinId",
                table: "Stores",
                column: "MoeinId");

            migrationBuilder.CreateIndex(
                name: "IX_TafziliGroups_TafziliTypeId",
                table: "TafziliGroups",
                column: "TafziliTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenders_TenderOwnerId",
                table: "Tenders",
                column: "TenderOwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccDocmentDetails");

            migrationBuilder.DropTable(
                name: "CostListDetails");

            migrationBuilder.DropTable(
                name: "MaterialCirculations");

            migrationBuilder.DropTable(
                name: "MoeinTafziliGroups");

            migrationBuilder.DropTable(
                name: "PaymentCheques");

            migrationBuilder.DropTable(
                name: "ProjectStatusFactors");

            migrationBuilder.DropTable(
                name: "ReceiveCheques");

            migrationBuilder.DropTable(
                name: "Tenders");

            migrationBuilder.DropTable(
                name: "Tafzili2s");

            migrationBuilder.DropTable(
                name: "Tafzili3s");

            migrationBuilder.DropTable(
                name: "CostLists");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "TafziliGroups");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "Tafzilis");

            migrationBuilder.DropTable(
                name: "MaterialGroups");

            migrationBuilder.DropTable(
                name: "MaterialUnits");

            migrationBuilder.DropTable(
                name: "TafziliTypes");

            migrationBuilder.DropTable(
                name: "Moeins");

            migrationBuilder.DropTable(
                name: "Kols");
        }
    }
}
