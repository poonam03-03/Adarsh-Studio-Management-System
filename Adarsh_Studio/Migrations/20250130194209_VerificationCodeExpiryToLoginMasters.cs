using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adarsh_Studio.Migrations
{
    /// <inheritdoc />
    public partial class VerificationCodeExpiryToLoginMasters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VerificationCode",
                table: "LoginMaster",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "VerificationCodeExpiry",
                table: "LoginMaster",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerificationCode",
                table: "LoginMaster");

            migrationBuilder.DropColumn(
                name: "VerificationCodeExpiry",
                table: "LoginMaster");
        }
    }
}
