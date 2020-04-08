using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WhiskyArchive.Services.WhiskyRecording.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "whiskyrecording");

            migrationBuilder.CreateSequence(
                name: "distilleryseq",
                schema: "whiskyrecording",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "whiskyimageseq",
                schema: "whiskyrecording",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "whiskypriceseq",
                schema: "whiskyrecording",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "whiskyseq",
                schema: "whiskyrecording",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "ClientRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "currency",
                schema: "whiskyrecording",
                columns: table => new
                {
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Id = table.Column<int>(nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "distilleries",
                schema: "whiskyrecording",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    DistilleryId = table.Column<string>(maxLength: 200, nullable: false),
                    DistilleryName_ChineseTraditional = table.Column<string>(nullable: true),
                    DistilleryName_ChineseSimplified = table.Column<string>(nullable: true),
                    DistilleryName_English = table.Column<string>(nullable: true),
                    Established = table.Column<string>(nullable: false),
                    Introdution = table.Column<string>(nullable: false),
                    SmwsCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_distilleries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pricereference",
                schema: "whiskyrecording",
                columns: table => new
                {
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Id = table.Column<int>(nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pricereference", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "whiskys",
                schema: "whiskyrecording",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    WhiskyId = table.Column<string>(maxLength: 200, nullable: false),
                    WhiskyName_Chinese = table.Column<string>(nullable: true),
                    WhiskyName_English = table.Column<string>(nullable: true),
                    DistilleryId = table.Column<string>(nullable: false),
                    Bottler = table.Column<string>(nullable: false),
                    WhiskyDetail_Vintage = table.Column<string>(nullable: true),
                    WhiskyDetail_Bottled = table.Column<string>(nullable: true),
                    WhiskyDetail_StatedAge = table.Column<int>(nullable: true),
                    WhiskyDetail_CaskType = table.Column<string>(nullable: true),
                    WhiskyDetail_CaskNumber = table.Column<string>(nullable: true),
                    WhiskyDetail_NumOfBottles = table.Column<int>(nullable: true),
                    WhiskyDetail_Strength = table.Column<float>(nullable: false),
                    WhiskyDetail_Size = table.Column<int>(nullable: false),
                    WhiskyDetail_Market = table.Column<string>(nullable: true),
                    WhiskyBaseRating = table.Column<float>(nullable: false),
                    Notes = table.Column<string>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_whiskys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "whiskyimages",
                schema: "whiskyrecording",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    WhiskyImageNumber = table.Column<int>(nullable: false),
                    WhiskyId = table.Column<string>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    WhiskyId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_whiskyimages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_whiskyimages_whiskys_WhiskyId1",
                        column: x => x.WhiskyId1,
                        principalSchema: "whiskyrecording",
                        principalTable: "whiskys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "whiskyprices",
                schema: "whiskyrecording",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    WhiskyPriceNumber = table.Column<int>(nullable: false),
                    WhiskyId = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(20,2)", nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    PriceReferenceId = table.Column<int>(nullable: false),
                    Seller = table.Column<string>(nullable: false),
                    PriceDate = table.Column<DateTime>(nullable: false),
                    WhiskyId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_whiskyprices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_whiskyprices_currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "whiskyrecording",
                        principalTable: "currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_whiskyprices_pricereference_PriceReferenceId",
                        column: x => x.PriceReferenceId,
                        principalSchema: "whiskyrecording",
                        principalTable: "pricereference",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_whiskyprices_whiskys_WhiskyId1",
                        column: x => x.WhiskyId1,
                        principalSchema: "whiskyrecording",
                        principalTable: "whiskys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_distilleries_DistilleryId",
                schema: "whiskyrecording",
                table: "distilleries",
                column: "DistilleryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_whiskyimages_WhiskyId1",
                schema: "whiskyrecording",
                table: "whiskyimages",
                column: "WhiskyId1");

            migrationBuilder.CreateIndex(
                name: "IX_whiskyprices_CurrencyId",
                schema: "whiskyrecording",
                table: "whiskyprices",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_whiskyprices_PriceReferenceId",
                schema: "whiskyrecording",
                table: "whiskyprices",
                column: "PriceReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_whiskyprices_WhiskyId1",
                schema: "whiskyrecording",
                table: "whiskyprices",
                column: "WhiskyId1");

            migrationBuilder.CreateIndex(
                name: "IX_whiskys_WhiskyId",
                schema: "whiskyrecording",
                table: "whiskys",
                column: "WhiskyId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientRequests");

            migrationBuilder.DropTable(
                name: "distilleries",
                schema: "whiskyrecording");

            migrationBuilder.DropTable(
                name: "whiskyimages",
                schema: "whiskyrecording");

            migrationBuilder.DropTable(
                name: "whiskyprices",
                schema: "whiskyrecording");

            migrationBuilder.DropTable(
                name: "currency",
                schema: "whiskyrecording");

            migrationBuilder.DropTable(
                name: "pricereference",
                schema: "whiskyrecording");

            migrationBuilder.DropTable(
                name: "whiskys",
                schema: "whiskyrecording");

            migrationBuilder.DropSequence(
                name: "distilleryseq",
                schema: "whiskyrecording");

            migrationBuilder.DropSequence(
                name: "whiskyimageseq",
                schema: "whiskyrecording");

            migrationBuilder.DropSequence(
                name: "whiskypriceseq",
                schema: "whiskyrecording");

            migrationBuilder.DropSequence(
                name: "whiskyseq",
                schema: "whiskyrecording");
        }
    }
}
