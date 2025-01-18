using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITstudyv4.Migrations
{
    /// <inheritdoc />
    public partial class AddThreadTestRD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Threads_Posts_AnswersId",
                table: "Threads");

            migrationBuilder.AlterColumn<int>(
                name: "AnswersId",
                table: "Threads",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_Posts_AnswersId",
                table: "Threads",
                column: "AnswersId",
                principalTable: "Posts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Threads_Posts_AnswersId",
                table: "Threads");

            migrationBuilder.AlterColumn<int>(
                name: "AnswersId",
                table: "Threads",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_Posts_AnswersId",
                table: "Threads",
                column: "AnswersId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
