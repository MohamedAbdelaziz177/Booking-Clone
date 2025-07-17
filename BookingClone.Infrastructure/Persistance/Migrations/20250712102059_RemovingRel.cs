using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingClone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovingRel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_feedBacks_rooms_RoomId",
                table: "feedBacks");

            migrationBuilder.DropIndex(
                name: "IX_feedBacks_RoomId",
                table: "feedBacks");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "feedBacks");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BookingDate",
                table: "reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 12, 13, 20, 58, 969, DateTimeKind.Local).AddTicks(1757),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 12, 12, 42, 17, 260, DateTimeKind.Local).AddTicks(6621));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "BookingDate",
                table: "reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 12, 12, 42, 17, 260, DateTimeKind.Local).AddTicks(6621),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 7, 12, 13, 20, 58, 969, DateTimeKind.Local).AddTicks(1757));

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "feedBacks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_feedBacks_RoomId",
                table: "feedBacks",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_feedBacks_rooms_RoomId",
                table: "feedBacks",
                column: "RoomId",
                principalTable: "rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
