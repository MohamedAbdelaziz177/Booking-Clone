using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingClone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Succ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "BookingDate",
                table: "reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 16, 7, 31, 46, 240, DateTimeKind.Local).AddTicks(1879),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 15, 8, 43, 46, 272, DateTimeKind.Local).AddTicks(2263));

            migrationBuilder.AlterColumn<string>(
                name: "IntentId",
                table: "payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "BookingDate",
                table: "reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 15, 8, 43, 46, 272, DateTimeKind.Local).AddTicks(2263),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 16, 7, 31, 46, 240, DateTimeKind.Local).AddTicks(1879));

            migrationBuilder.AlterColumn<string>(
                name: "IntentId",
                table: "payments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
