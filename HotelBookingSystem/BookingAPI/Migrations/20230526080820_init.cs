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
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    { 1, new DateTime(2023, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2023, 5, 26, 13, 38, 20, 717, DateTimeKind.Local).AddTicks(743), 101, "mathan" },
                    { 2, new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2023, 5, 26, 13, 38, 20, 717, DateTimeKind.Local).AddTicks(751), 201, "raj" },
                    { 3, new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2023, 5, 26, 13, 38, 20, 717, DateTimeKind.Local).AddTicks(753), 102, "kishore" },
                    { 4, new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2023, 5, 26, 13, 38, 20, 717, DateTimeKind.Local).AddTicks(754), 301, "gokulan" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");
        }
    }
}
