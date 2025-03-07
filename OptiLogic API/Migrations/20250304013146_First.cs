using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OptiLogic_API.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parking_Lot",
                columns: table => new
                {
                    pl_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pl_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parking_Lot", x => x.pl_id);
                });

            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    car_id = table.Column<int>(type: "int", nullable: false),
                    x_axis = table.Column<float>(type: "real", nullable: false),
                    y_axis = table.Column<float>(type: "real", nullable: false),
                    width = table.Column<float>(type: "real", nullable: false),
                    height = table.Column<float>(type: "real", nullable: false),
                    confidence = table.Column<float>(type: "real", nullable: false),
                    parking_lot_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car", x => x.car_id);
                    table.ForeignKey(
                        name: "FK_Car_Parking_Lot_parking_lot_id",
                        column: x => x.parking_lot_id,
                        principalTable: "Parking_Lot",
                        principalColumn: "pl_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parking_Spot",
                columns: table => new
                {
                    parking_spot_id = table.Column<int>(type: "int", nullable: false),
                    x_axis = table.Column<float>(type: "real", nullable: false),
                    y_axis = table.Column<float>(type: "real", nullable: false),
                    width = table.Column<float>(type: "real", nullable: false),
                    height = table.Column<float>(type: "real", nullable: false),
                    occupied = table.Column<bool>(type: "bit", nullable: false),
                    parking_lot_id = table.Column<int>(type: "int", nullable: false),
                    car_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parking_Spot", x => x.parking_spot_id);
                    table.ForeignKey(
                        name: "FK_Parking_Spot_Parking_Lot_parking_lot_id",
                        column: x => x.parking_lot_id,
                        principalTable: "Parking_Lot",
                        principalColumn: "pl_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Car_parking_lot_id",
                table: "Car",
                column: "parking_lot_id");

            migrationBuilder.CreateIndex(
                name: "IX_Parking_Spot_parking_lot_id",
                table: "Parking_Spot",
                column: "parking_lot_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Car");

            migrationBuilder.DropTable(
                name: "Parking_Spot");

            migrationBuilder.DropTable(
                name: "Parking_Lot");
        }
    }
}
