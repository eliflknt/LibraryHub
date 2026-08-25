using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLoanDueDateIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Loans_DueDate",
                table: "Loans",
                column: "DueDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Loans_DueDate",
                table: "Loans");
        }
    }
}
