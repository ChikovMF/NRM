using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NRM.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParselSatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParselSatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParselType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParselType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Place",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Place", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbstractParsel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    TrackNumber = table.Column<string>(type: "text", nullable: false),
                    DepartureDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DepartureTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    PlaceOfDepartureId = table.Column<int>(type: "integer", nullable: true),
                    PlaceOfDeliveryId = table.Column<int>(type: "integer", nullable: true),
                    Sender = table.Column<string>(type: "text", nullable: true),
                    Recipient = table.Column<string>(type: "text", nullable: true),
                    TypeId = table.Column<int>(type: "integer", nullable: true),
                    GroupParselId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbstractParsel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbstractParsel_AbstractParsel_GroupParselId",
                        column: x => x.GroupParselId,
                        principalTable: "AbstractParsel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AbstractParsel_ParselSatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ParselSatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbstractParsel_ParselType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ParselType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbstractParsel_Place_PlaceOfDeliveryId",
                        column: x => x.PlaceOfDeliveryId,
                        principalTable: "Place",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbstractParsel_Place_PlaceOfDepartureId",
                        column: x => x.PlaceOfDepartureId,
                        principalTable: "Place",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbstractParsel_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbstractLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Message = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    GroupParselId = table.Column<int>(type: "integer", nullable: true),
                    ParselId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbstractLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbstractLog_AbstractParsel_GroupParselId",
                        column: x => x.GroupParselId,
                        principalTable: "AbstractParsel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbstractLog_AbstractParsel_ParselId",
                        column: x => x.ParselId,
                        principalTable: "AbstractParsel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbstractLog_LogType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "LogType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbstractLog_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbstractLog_GroupParselId",
                table: "AbstractLog",
                column: "GroupParselId");

            migrationBuilder.CreateIndex(
                name: "IX_AbstractLog_ParselId",
                table: "AbstractLog",
                column: "ParselId");

            migrationBuilder.CreateIndex(
                name: "IX_AbstractLog_TypeId",
                table: "AbstractLog",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AbstractLog_UserId",
                table: "AbstractLog",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AbstractParsel_GroupParselId",
                table: "AbstractParsel",
                column: "GroupParselId");

            migrationBuilder.CreateIndex(
                name: "IX_AbstractParsel_PlaceOfDeliveryId",
                table: "AbstractParsel",
                column: "PlaceOfDeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_AbstractParsel_PlaceOfDepartureId",
                table: "AbstractParsel",
                column: "PlaceOfDepartureId");

            migrationBuilder.CreateIndex(
                name: "IX_AbstractParsel_StatusId",
                table: "AbstractParsel",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AbstractParsel_TypeId",
                table: "AbstractParsel",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AbstractParsel_UserId",
                table: "AbstractParsel",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbstractLog");

            migrationBuilder.DropTable(
                name: "AbstractParsel");

            migrationBuilder.DropTable(
                name: "LogType");

            migrationBuilder.DropTable(
                name: "ParselSatus");

            migrationBuilder.DropTable(
                name: "ParselType");

            migrationBuilder.DropTable(
                name: "Place");
        }
    }
}
