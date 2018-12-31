using Microsoft.EntityFrameworkCore.Migrations;

namespace ncframework.Migrations
{
    public partial class db2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 36,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GroupMenu",
                table: "Employee",
                maxLength: 370,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 370,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                maxLength: 36,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<string>(
                name: "GroupMenu",
                table: "Employee",
                maxLength: 370,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 370);
        }
    }
}
