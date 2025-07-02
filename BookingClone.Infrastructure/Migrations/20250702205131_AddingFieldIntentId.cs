using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingClone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingFieldIntentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "BookingDate",
                table: "reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 2, 23, 51, 29, 331, DateTimeKind.Local).AddTicks(1260),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 2, 23, 12, 7, 813, DateTimeKind.Local).AddTicks(3893));

            migrationBuilder.AddColumn<string>(
                name: "IntentId",
                table: "payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IntentId",
                table: "payments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BookingDate",
                table: "reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 2, 23, 12, 7, 813, DateTimeKind.Local).AddTicks(3893),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 2, 23, 51, 29, 331, DateTimeKind.Local).AddTicks(1260));
        }
    }
}
