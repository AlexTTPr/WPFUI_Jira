using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WPFUI_Jira.Migrations
{
    /// <inheritdoc />
    public partial class Migration25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskCards_Users_ExecutorId",
                table: "TaskCards");

            migrationBuilder.DropIndex(
                name: "IX_TaskCards_ExecutorId",
                table: "TaskCards");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TaskCards_ExecutorId",
                table: "TaskCards",
                column: "ExecutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskCards_Users_ExecutorId",
                table: "TaskCards",
                column: "ExecutorId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
