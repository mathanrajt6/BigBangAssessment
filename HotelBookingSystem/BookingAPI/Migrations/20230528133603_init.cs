using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingAPI.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    CheckIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "CheckIn", "CheckOut", "HotelId", "ReservationDate", "RoomId", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2023, 5, 28, 19, 6, 2, 881, DateTimeKind.Local).AddTicks(5615), 101, "mathan" },
                    { 2, new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2023, 5, 28, 19, 6, 2, 881, DateTimeKind.Local).AddTicks(5628), 201, "raj" },
                    { 3, new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2023, 5, 28, 19, 6, 2, 881, DateTimeKind.Local).AddTicks(5632), 102, "kishore" },
                    { 4, new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2023, 5, 28, 19, 6, 2, 881, DateTimeKind.Local).AddTicks(5635), 301, "gokulan" },
                    { 5, new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2023, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "RobertDavis" },
                    { 6, new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2023, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "EmilyJohnson" },
                    { 7, new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "DavidBrown" },
                    { 8, new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2023, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "JessicaSmith" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");
        }
    }
}
