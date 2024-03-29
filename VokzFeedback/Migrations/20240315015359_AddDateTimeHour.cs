using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VokzFeedback.Migrations
{
    /// <inheritdoc />
    public partial class AddDateTimeHour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateHour",
                table: "Feedbacks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateHour",
                table: "Feedbacks");
        }
    }
}
