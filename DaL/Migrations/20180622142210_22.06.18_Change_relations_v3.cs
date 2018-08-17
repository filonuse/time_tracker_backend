using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DaL.Migrations
{
    public partial class _220618_Change_relations_v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Groups_GroupManagerId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeGroups_AspNetUsers_UserId",
                table: "EmployeeGroups");

            migrationBuilder.DropTable(
                name: "UserWorkTimeData");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_EmployeeGroups_GroupId_UserId",
                table: "EmployeeGroups");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GroupManagerId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GroupManagerId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "EmployeeGroups",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeGroups_UserId",
                table: "EmployeeGroups",
                newName: "IX_EmployeeGroups_EmployeeId");

            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "LogModel",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_EmployeeGroups_GroupId_EmployeeId",
                table: "EmployeeGroups",
                columns: new[] { "GroupId", "EmployeeId" });

            migrationBuilder.CreateTable(
                name: "ManagerGroupModel",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    GroupId = table.Column<string>(nullable: false),
                    ManagerId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagerGroupModel", x => x.Id);
                    table.UniqueConstraint("AK_ManagerGroupModel_GroupId_ManagerId", x => new { x.GroupId, x.ManagerId });
                    table.ForeignKey(
                        name: "FK_ManagerGroupModel_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ManagerGroupModel_AspNetUsers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserWorkSessionData",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    StartWork = table.Column<DateTime>(nullable: false),
                    StopWork = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    WorkDuration = table.Column<TimeSpan>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWorkSessionData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWorkSessionData_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogModel_SessionId",
                table: "LogModel",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ManagerGroupModel_ManagerId",
                table: "ManagerGroupModel",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkSessionData_UserId",
                table: "UserWorkSessionData",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeGroups_AspNetUsers_EmployeeId",
                table: "EmployeeGroups",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LogModel_UserWorkSessionData_SessionId",
                table: "LogModel",
                column: "SessionId",
                principalTable: "UserWorkSessionData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeGroups_AspNetUsers_EmployeeId",
                table: "EmployeeGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_LogModel_UserWorkSessionData_SessionId",
                table: "LogModel");

            migrationBuilder.DropTable(
                name: "ManagerGroupModel");

            migrationBuilder.DropTable(
                name: "UserWorkSessionData");

            migrationBuilder.DropIndex(
                name: "IX_LogModel_SessionId",
                table: "LogModel");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_EmployeeGroups_GroupId_EmployeeId",
                table: "EmployeeGroups");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "LogModel");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "EmployeeGroups",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeGroups_EmployeeId",
                table: "EmployeeGroups",
                newName: "IX_EmployeeGroups_UserId");

            migrationBuilder.AddColumn<string>(
                name: "GroupManagerId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_EmployeeGroups_GroupId_UserId",
                table: "EmployeeGroups",
                columns: new[] { "GroupId", "UserId" });

            migrationBuilder.CreateTable(
                name: "UserWorkTimeData",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    StartWork = table.Column<DateTime>(nullable: false),
                    StopWork = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    WorkDuration = table.Column<TimeSpan>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWorkTimeData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWorkTimeData_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GroupManagerId",
                table: "AspNetUsers",
                column: "GroupManagerId",
                unique: true,
                filter: "[GroupManagerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkTimeData_UserId",
                table: "UserWorkTimeData",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Groups_GroupManagerId",
                table: "AspNetUsers",
                column: "GroupManagerId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeGroups_AspNetUsers_UserId",
                table: "EmployeeGroups",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
