using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DaL.Migrations
{
    public partial class _250618_create_AppInfoModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppInfoModel",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    AppName = table.Column<string>(nullable: true),
                    AppShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInfoModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupAppModel",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    AppId = table.Column<string>(nullable: true),
                    GroupId = table.Column<string>(nullable: true),
                    IsUseful = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupAppModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupAppModel_AppInfoModel_AppId",
                        column: x => x.AppId,
                        principalTable: "AppInfoModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupAppModel_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserAppModel",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    AppId = table.Column<string>(nullable: false),
                    IsUseful = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAppModel", x => x.Id);
                    table.UniqueConstraint("AK_UserAppModel_UserId_AppId", x => new { x.UserId, x.AppId });
                    table.ForeignKey(
                        name: "FK_UserAppModel_AppInfoModel_AppId",
                        column: x => x.AppId,
                        principalTable: "AppInfoModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAppModel_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupAppModel_AppId",
                table: "GroupAppModel",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupAppModel_GroupId",
                table: "GroupAppModel",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAppModel_AppId",
                table: "UserAppModel",
                column: "AppId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupAppModel");

            migrationBuilder.DropTable(
                name: "UserAppModel");

            migrationBuilder.DropTable(
                name: "AppInfoModel");
        }
    }
}
