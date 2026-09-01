using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adarsh_Studio.Migrations
{
    /// <inheritdoc />
    public partial class AddVerificationCodeToLoginMasters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CityMaster",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Created_On = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityMaster", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "EnquiryMaster",
                columns: table => new
                {
                    EnquiryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    EmailId = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    MobNo = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    QueryMsg = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnquiryMaster", x => x.EnquiryId);
                });

            migrationBuilder.CreateTable(
                name: "FeedbackMaster",
                columns: table => new
                {
                    FeedbackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    EmailId = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    MobileNo = table.Column<long>(type: "bigint", nullable: false),
                    TitleOfFeedback = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    FeedbackMsg = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    StarRating = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackMaster", x => x.FeedbackId);
                });

            migrationBuilder.CreateTable(
                name: "LoginMaster",
                columns: table => new
                {
                    AdminId = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    Admin_Pass = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    Login_Count = table.Column<int>(type: "int", nullable: true),
                    Last_Login_DT = table.Column<DateTime>(type: "datetime", nullable: true),
                    Is_Blocked = table.Column<bool>(type: "bit", nullable: true),
                    Created_On = table.Column<DateTime>(type: "datetime", nullable: false),
                    Updated_On = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginMaster", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "ServiceMaster",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceType = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: true),
                    Category = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Budget = table.Column<int>(type: "int", nullable: true),
                    DiscountedRate = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Inclusions = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    Exclusions = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    Created_On = table.Column<DateTime>(type: "datetime", nullable: true),
                    Updated_On = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceMaster", x => x.ServiceId);
                });

            migrationBuilder.CreateTable(
                name: "UpdatesMaster",
                columns: table => new
                {
                    UpdateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UpdateMsg = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpdatesMaster", x => x.UpdateId);
                });

            migrationBuilder.CreateTable(
                name: "BookingMaster",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: true),
                    ClientName = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    MobileNo = table.Column<long>(type: "bigint", nullable: true),
                    EmailId = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    CurrentCity = table.Column<int>(type: "int", nullable: true),
                    Address = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    LocationOfShooting = table.Column<int>(type: "int", nullable: true),
                    Remark = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    ShootingDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Price = table.Column<int>(type: "int", nullable: true),
                    Created_On = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingMaster", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_BookingMaster_CityMaster",
                        column: x => x.LocationOfShooting,
                        principalTable: "CityMaster",
                        principalColumn: "CityId");
                    table.ForeignKey(
                        name: "FK_BookingMaster_CityMaster1",
                        column: x => x.CurrentCity,
                        principalTable: "CityMaster",
                        principalColumn: "CityId");
                });

            migrationBuilder.CreateTable(
                name: "ServicePicMaster",
                columns: table => new
                {
                    PicId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: true),
                    PicFileName = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    PicFolderName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PicType = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    PicSize_InKB = table.Column<double>(type: "float", nullable: true),
                    Remark = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    Created_On = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePicMaster", x => x.PicId);
                    table.ForeignKey(
                        name: "FK_ServicePicMaster_ServiceMaster",
                        column: x => x.ServiceId,
                        principalTable: "ServiceMaster",
                        principalColumn: "ServiceId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingMaster_CurrentCity",
                table: "BookingMaster",
                column: "CurrentCity");

            migrationBuilder.CreateIndex(
                name: "IX_BookingMaster_LocationOfShooting",
                table: "BookingMaster",
                column: "LocationOfShooting");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePicMaster_ServiceId",
                table: "ServicePicMaster",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingMaster");

            migrationBuilder.DropTable(
                name: "EnquiryMaster");

            migrationBuilder.DropTable(
                name: "FeedbackMaster");

            migrationBuilder.DropTable(
                name: "LoginMaster");

            migrationBuilder.DropTable(
                name: "ServicePicMaster");

            migrationBuilder.DropTable(
                name: "UpdatesMaster");

            migrationBuilder.DropTable(
                name: "CityMaster");

            migrationBuilder.DropTable(
                name: "ServiceMaster");
        }
    }
}
