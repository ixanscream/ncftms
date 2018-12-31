using Microsoft.EntityFrameworkCore.Migrations;

namespace ncframework.Migrations
{
    public partial class db1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lookup",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    ParentId = table.Column<string>(maxLength: 36, nullable: true),
                    Group = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lookup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lookup_Lookup_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Lookup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Index = table.Column<int>(nullable: false),
                    Controller = table.Column<string>(nullable: true),
                    Action = table.Column<string>(maxLength: 50, nullable: false),
                    Icon = table.Column<string>(maxLength: 50, nullable: true),
                    ParentId = table.Column<string>(maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menu_Menu_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Code = table.Column<int>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    Photo = table.Column<string>(maxLength: 255, nullable: true),
                    Phone = table.Column<string>(maxLength: 50, nullable: true),
                    ParentId = table.Column<string>(maxLength: 36, nullable: true),
                    GroupId = table.Column<string>(maxLength: 36, nullable: true),
                    GroupMenu = table.Column<string>(maxLength: 370, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Lookup_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Lookup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employee_Employee_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Access",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    GroupId = table.Column<string>(maxLength: 36, nullable: true),
                    MenuId = table.Column<string>(maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Access", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Access_Lookup_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Lookup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Access_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<string>(maxLength: 36, nullable: false),
                    Password = table.Column<string>(maxLength: 36, nullable: true),
                    EmployeeId = table.Column<string>(maxLength: 36, nullable: true),
                    IsAdmin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Access_GroupId",
                table: "Access",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Access_MenuId",
                table: "Access",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Code",
                table: "Employee",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_GroupId",
                table: "Employee",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ParentId",
                table: "Employee",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Lookup_Code",
                table: "Lookup",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lookup_ParentId",
                table: "Lookup",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_ParentId",
                table: "Menu",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_User_EmployeeId",
                table: "User",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Access");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Lookup");
        }
    }
}
