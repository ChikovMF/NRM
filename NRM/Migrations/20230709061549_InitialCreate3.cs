using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NRM.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbstractLog_AbstractParsel_GroupParselId",
                table: "AbstractLog");

            migrationBuilder.DropForeignKey(
                name: "FK_AbstractLog_AbstractParsel_ParselId",
                table: "AbstractLog");

            migrationBuilder.DropTable(
                name: "AbstractParsel");

            migrationBuilder.DropTable(
                name: "ParselSatus");

            migrationBuilder.DropTable(
                name: "ParselType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Place",
                table: "Place");

            migrationBuilder.RenameTable(
                name: "Place",
                newName: "Places");

            migrationBuilder.RenameColumn(
                name: "ParselId",
                table: "AbstractLog",
                newName: "ParcelId");

            migrationBuilder.RenameColumn(
                name: "GroupParselId",
                table: "AbstractLog",
                newName: "GroupParcelId");

            migrationBuilder.RenameIndex(
                name: "IX_AbstractLog_ParselId",
                table: "AbstractLog",
                newName: "IX_AbstractLog_ParcelId");

            migrationBuilder.RenameIndex(
                name: "IX_AbstractLog_GroupParselId",
                table: "AbstractLog",
                newName: "IX_AbstractLog_GroupParcelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Places",
                table: "Places",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ParcelStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParcelStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParcelType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParcelType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbstractParcel",
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
                    GroupParcelId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbstractParcel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbstractParcel_AbstractParcel_GroupParcelId",
                        column: x => x.GroupParcelId,
                        principalTable: "AbstractParcel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AbstractParcel_ParcelStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ParcelStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbstractParcel_ParcelType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ParcelType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbstractParcel_Places_PlaceOfDeliveryId",
                        column: x => x.PlaceOfDeliveryId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbstractParcel_Places_PlaceOfDepartureId",
                        column: x => x.PlaceOfDepartureId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbstractParcel_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, false, "Полный администратор" },
                    { 2, false, "Администратор пользователей" },
                    { 3, false, "Администратор посылок" },
                    { 4, false, "Пользователь" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "IsDeleted", "LastName", "Login", "PasswordHash", "Patronymic", "PhoneNumber", "RoleId" },
                values: new object[] { 1, "ia-matvey@mail.ru", "Матвей", false, "Чиков", "Admin", "$2a$11$5sMN/8tgOvEHpRIFglQL8ux3xuTjyFuw/r2OcIw4AxMD7AC54bDwy", "Федорович", "89147291215", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_AbstractParcel_GroupParcelId",
                table: "AbstractParcel",
                column: "GroupParcelId");

            migrationBuilder.CreateIndex(
                name: "IX_AbstractParcel_PlaceOfDeliveryId",
                table: "AbstractParcel",
                column: "PlaceOfDeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_AbstractParcel_PlaceOfDepartureId",
                table: "AbstractParcel",
                column: "PlaceOfDepartureId");

            migrationBuilder.CreateIndex(
                name: "IX_AbstractParcel_StatusId",
                table: "AbstractParcel",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AbstractParcel_TypeId",
                table: "AbstractParcel",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AbstractParcel_UserId",
                table: "AbstractParcel",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbstractLog_AbstractParcel_GroupParcelId",
                table: "AbstractLog",
                column: "GroupParcelId",
                principalTable: "AbstractParcel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AbstractLog_AbstractParcel_ParcelId",
                table: "AbstractLog",
                column: "ParcelId",
                principalTable: "AbstractParcel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbstractLog_AbstractParcel_GroupParcelId",
                table: "AbstractLog");

            migrationBuilder.DropForeignKey(
                name: "FK_AbstractLog_AbstractParcel_ParcelId",
                table: "AbstractLog");

            migrationBuilder.DropTable(
                name: "AbstractParcel");

            migrationBuilder.DropTable(
                name: "ParcelStatus");

            migrationBuilder.DropTable(
                name: "ParcelType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Places",
                table: "Places");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameTable(
                name: "Places",
                newName: "Place");

            migrationBuilder.RenameColumn(
                name: "ParcelId",
                table: "AbstractLog",
                newName: "ParselId");

            migrationBuilder.RenameColumn(
                name: "GroupParcelId",
                table: "AbstractLog",
                newName: "GroupParselId");

            migrationBuilder.RenameIndex(
                name: "IX_AbstractLog_ParcelId",
                table: "AbstractLog",
                newName: "IX_AbstractLog_ParselId");

            migrationBuilder.RenameIndex(
                name: "IX_AbstractLog_GroupParcelId",
                table: "AbstractLog",
                newName: "IX_AbstractLog_GroupParselId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Place",
                table: "Place",
                column: "Id");

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
                name: "AbstractParsel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    DepartureDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DepartureTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    TrackNumber = table.Column<string>(type: "text", nullable: false),
                    PlaceOfDeliveryId = table.Column<int>(type: "integer", nullable: true),
                    PlaceOfDepartureId = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    GroupParselId = table.Column<int>(type: "integer", nullable: true),
                    TypeId = table.Column<int>(type: "integer", nullable: true),
                    Recipient = table.Column<string>(type: "text", nullable: true),
                    Sender = table.Column<string>(type: "text", nullable: true)
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

            migrationBuilder.AddForeignKey(
                name: "FK_AbstractLog_AbstractParsel_GroupParselId",
                table: "AbstractLog",
                column: "GroupParselId",
                principalTable: "AbstractParsel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AbstractLog_AbstractParsel_ParselId",
                table: "AbstractLog",
                column: "ParselId",
                principalTable: "AbstractParsel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
