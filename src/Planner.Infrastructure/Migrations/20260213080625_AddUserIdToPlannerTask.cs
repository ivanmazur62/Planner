using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Planner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToPlannerTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PlannerTasks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PlannerTasks_UserId",
                table: "PlannerTasks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlannerTasks_AspNetUsers_UserId",
                table: "PlannerTasks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlannerTasks_AspNetUsers_UserId",
                table: "PlannerTasks");

            migrationBuilder.DropIndex(
                name: "IX_PlannerTasks_UserId",
                table: "PlannerTasks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PlannerTasks");
        }
    }
}
