using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Morgenmadsbuffeten.Data.Migrations
{
    public partial class AddingBreakfatOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BreakfastOrders",
                columns: table => new
                {
                    BreakfastOrderId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomNumber = table.Column<long>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Adults = table.Column<int>(nullable: false),
                    CheckedInAdults = table.Column<int>(nullable: true),
                    Children = table.Column<int>(nullable: false),
                    CheckedInChildren = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreakfastOrders", x => x.BreakfastOrderId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BreakfastOrders");
        }
    }
}
