using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingClone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingDecimalConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PricePerNight",
                table: "rooms",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,1)",
                oldPrecision: 3,
                oldScale: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BookingDate",
                table: "reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 10, 2, 14, 45, 85, DateTimeKind.Local).AddTicks(8839),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 7, 3, 22, 46, 618, DateTimeKind.Local).AddTicks(771));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PricePerNight",
                table: "rooms",
                type: "decimal(3,1)",
                precision: 3,
                scale: 1,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BookingDate",
                table: "reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 7, 3, 22, 46, 618, DateTimeKind.Local).AddTicks(771),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 10, 2, 14, 45, 85, DateTimeKind.Local).AddTicks(8839));
        }
    }
}
