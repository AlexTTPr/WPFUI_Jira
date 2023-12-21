using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WPFUI_Jira.Migrations
{
    /// <inheritdoc />
    public partial class Migration22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SpendTime",
                table: "ActionRecord",
                newName: "TimeSpent");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeSpent",
                table: "ActionRecord",
                newName: "SpendTime");
        }
    }
}
