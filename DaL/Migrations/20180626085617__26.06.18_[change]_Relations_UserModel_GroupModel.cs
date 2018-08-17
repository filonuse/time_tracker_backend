using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DaL.Migrations
{
    public partial class _260618_change_Relations_UserModel_GroupModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileModel_LogModel_LogModelId",
                table: "FileModel");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupAppModel_AppInfoModel_AppId",
                table: "GroupAppModel");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupAppModel_Groups_GroupId",
                table: "GroupAppModel");

            migrationBuilder.DropForeignKey(
                name: "FK_LogModel_UserWorkSessionData_SessionId",
                table: "LogModel");

            migrationBuilder.DropForeignKey(
                name: "FK_LogModel_AspNetUsers_UserId",
                table: "LogModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcessInfoModel_LogModel_LogModelId",
                table: "ProcessInfoModel");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAppModel_AppInfoModel_AppId",
                table: "UserAppModel");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAppModel_AspNetUsers_UserId",
                table: "UserAppModel");

            migrationBuilder.DropTable(
                name: "EmployeeGroups");

            migrationBuilder.DropTable(
                name: "ManagerGroupModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAppModel",
                table: "UserAppModel");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserAppModel_UserId_AppId",
                table: "UserAppModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LogModel",
                table: "LogModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupAppModel",
                table: "GroupAppModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileModel",
                table: "FileModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppInfoModel",
                table: "AppInfoModel");

            migrationBuilder.RenameTable(
                name: "UserAppModel",
                newName: "UserApps");

            migrationBuilder.RenameTable(
                name: "LogModel",
                newName: "Logs");

            migrationBuilder.RenameTable(
                name: "GroupAppModel",
                newName: "GroupApps");

            migrationBuilder.RenameTable(
                name: "FileModel",
                newName: "Files");

            migrationBuilder.RenameTable(
                name: "AppInfoModel",
                newName: "AppInfos");

            migrationBuilder.RenameIndex(
                name: "IX_UserAppModel_AppId",
                table: "UserApps",
                newName: "IX_UserApps_AppId");

            migrationBuilder.RenameIndex(
                name: "IX_LogModel_UserId",
                table: "Logs",
                newName: "IX_Logs_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_LogModel_SessionId",
                table: "Logs",
                newName: "IX_Logs_SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupAppModel_GroupId",
                table: "GroupApps",
                newName: "IX_GroupApps_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupAppModel_AppId",
                table: "GroupApps",
                newName: "IX_GroupApps_AppId");

            migrationBuilder.RenameIndex(
                name: "IX_FileModel_LogModelId",
                table: "Files",
                newName: "IX_Files_LogModelId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsUseful",
                table: "UserApps",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<bool>(
                name: "IsUseful",
                table: "GroupApps",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserApps",
                table: "UserApps",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserApps_UserId_AppId",
                table: "UserApps",
                columns: new[] { "UserId", "AppId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Logs",
                table: "Logs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupApps",
                table: "GroupApps",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppInfos",
                table: "AppInfos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserGroups",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    GroupId = table.Column<string>(nullable: false),
                    IsManager = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroups", x => x.Id);
                    table.UniqueConstraint("AK_UserGroups_GroupId_UserId", x => new { x.GroupId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroups_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_UserId",
                table: "UserGroups",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Logs_LogModelId",
                table: "Files",
                column: "LogModelId",
                principalTable: "Logs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupApps_AppInfos_AppId",
                table: "GroupApps",
                column: "AppId",
                principalTable: "AppInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupApps_Groups_GroupId",
                table: "GroupApps",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_UserWorkSessionData_SessionId",
                table: "Logs",
                column: "SessionId",
                principalTable: "UserWorkSessionData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_AspNetUsers_UserId",
                table: "Logs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessInfoModel_Logs_LogModelId",
                table: "ProcessInfoModel",
                column: "LogModelId",
                principalTable: "Logs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserApps_AppInfos_AppId",
                table: "UserApps",
                column: "AppId",
                principalTable: "AppInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserApps_AspNetUsers_UserId",
                table: "UserApps",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Logs_LogModelId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupApps_AppInfos_AppId",
                table: "GroupApps");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupApps_Groups_GroupId",
                table: "GroupApps");

            migrationBuilder.DropForeignKey(
                name: "FK_Logs_UserWorkSessionData_SessionId",
                table: "Logs");

            migrationBuilder.DropForeignKey(
                name: "FK_Logs_AspNetUsers_UserId",
                table: "Logs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcessInfoModel_Logs_LogModelId",
                table: "ProcessInfoModel");

            migrationBuilder.DropForeignKey(
                name: "FK_UserApps_AppInfos_AppId",
                table: "UserApps");

            migrationBuilder.DropForeignKey(
                name: "FK_UserApps_AspNetUsers_UserId",
                table: "UserApps");

            migrationBuilder.DropTable(
                name: "UserGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserApps",
                table: "UserApps");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserApps_UserId_AppId",
                table: "UserApps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Logs",
                table: "Logs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupApps",
                table: "GroupApps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Files",
                table: "Files");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppInfos",
                table: "AppInfos");

            migrationBuilder.RenameTable(
                name: "UserApps",
                newName: "UserAppModel");

            migrationBuilder.RenameTable(
                name: "Logs",
                newName: "LogModel");

            migrationBuilder.RenameTable(
                name: "GroupApps",
                newName: "GroupAppModel");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "FileModel");

            migrationBuilder.RenameTable(
                name: "AppInfos",
                newName: "AppInfoModel");

            migrationBuilder.RenameIndex(
                name: "IX_UserApps_AppId",
                table: "UserAppModel",
                newName: "IX_UserAppModel_AppId");

            migrationBuilder.RenameIndex(
                name: "IX_Logs_UserId",
                table: "LogModel",
                newName: "IX_LogModel_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Logs_SessionId",
                table: "LogModel",
                newName: "IX_LogModel_SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupApps_GroupId",
                table: "GroupAppModel",
                newName: "IX_GroupAppModel_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupApps_AppId",
                table: "GroupAppModel",
                newName: "IX_GroupAppModel_AppId");

            migrationBuilder.RenameIndex(
                name: "IX_Files_LogModelId",
                table: "FileModel",
                newName: "IX_FileModel_LogModelId");

            migrationBuilder.AlterColumn<int>(
                name: "IsUseful",
                table: "UserAppModel",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IsUseful",
                table: "GroupAppModel",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAppModel",
                table: "UserAppModel",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserAppModel_UserId_AppId",
                table: "UserAppModel",
                columns: new[] { "UserId", "AppId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_LogModel",
                table: "LogModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupAppModel",
                table: "GroupAppModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileModel",
                table: "FileModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppInfoModel",
                table: "AppInfoModel",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "EmployeeGroups",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    EmployeeId = table.Column<string>(nullable: false),
                    GroupId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeGroups", x => x.Id);
                    table.UniqueConstraint("AK_EmployeeGroups_GroupId_EmployeeId", x => new { x.GroupId, x.EmployeeId });
                    table.ForeignKey(
                        name: "FK_EmployeeGroups_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeGroups_EmployeeId",
                table: "EmployeeGroups",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ManagerGroupModel_ManagerId",
                table: "ManagerGroupModel",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileModel_LogModel_LogModelId",
                table: "FileModel",
                column: "LogModelId",
                principalTable: "LogModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupAppModel_AppInfoModel_AppId",
                table: "GroupAppModel",
                column: "AppId",
                principalTable: "AppInfoModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupAppModel_Groups_GroupId",
                table: "GroupAppModel",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LogModel_UserWorkSessionData_SessionId",
                table: "LogModel",
                column: "SessionId",
                principalTable: "UserWorkSessionData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LogModel_AspNetUsers_UserId",
                table: "LogModel",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessInfoModel_LogModel_LogModelId",
                table: "ProcessInfoModel",
                column: "LogModelId",
                principalTable: "LogModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAppModel_AppInfoModel_AppId",
                table: "UserAppModel",
                column: "AppId",
                principalTable: "AppInfoModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAppModel_AspNetUsers_UserId",
                table: "UserAppModel",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
