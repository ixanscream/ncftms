using Microsoft.EntityFrameworkCore.Migrations;

namespace ncframework.Migrations
{
    public partial class db3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GroupMenu",
                table: "Employee",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 370);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GroupMenu",
                table: "Employee",
                maxLength: 370,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
