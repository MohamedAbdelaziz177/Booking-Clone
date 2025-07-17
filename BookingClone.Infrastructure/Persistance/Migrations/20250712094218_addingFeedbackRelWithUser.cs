using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingClone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingFeedbackRelWithUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "BookingDate",
                table: "reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 12, 12, 42, 17, 260, DateTimeKind.Local).AddTicks(6621),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 10, 2, 14, 45, 85, DateTimeKind.Local).AddTicks(8839));

            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "feedBacks",
                type: "float(8)",
                precision: 8,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(3)",
                oldPrecision: 3,
                oldScale: 1);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "feedBacks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_feedBacks_UserId",
                table: "feedBacks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_feedBacks_AspNetUsers_UserId",
                table: "feedBacks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_feedBacks_AspNetUsers_UserId",
                table: "feedBacks");

            migrationBuilder.DropIndex(
                name: "IX_feedBacks_UserId",
                table: "feedBacks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "feedBacks");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BookingDate",
                table: "reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 10, 2, 14, 45, 85, DateTimeKind.Local).AddTicks(8839),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 12, 12, 42, 17, 260, DateTimeKind.Local).AddTicks(6621));

            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "feedBacks",
                type: "float(3)",
                precision: 3,
                scale: 1,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(8)",
                oldPrecision: 8,
                oldScale: 2);
        }
    }
}
