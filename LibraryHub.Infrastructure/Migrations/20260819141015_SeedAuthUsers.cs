using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAuthUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "MemberId", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { 1001, new DateTime(2026, 8, 19, 0, 0, 0, 0, DateTimeKind.Utc), "admin@libraryhub.com", true, null, "100000.MiuBPTHhq81y1s18F7hUKA==.aUyiwiIvJsPfqPeEVk6csHU00xsrRh95QALye1S1ot0=", "Admin" },
                    { 1002, new DateTime(2026, 8, 19, 0, 0, 0, 0, DateTimeKind.Utc), "librarian@libraryhub.com", true, null, "100000.FBn+sF9YO1ffISS9eN5NOg==.AlrLZ0auUnmkkHVpVszLHzXwp9Gi1seCQGMVaHBZCBI=", "Librarian" },
                    { 1003, new DateTime(2026, 8, 19, 0, 0, 0, 0, DateTimeKind.Utc), "member@libraryhub.com", true, null, "100000.IdYbp8ZEpCo9h36CKJdFPQ==.Nyvb2WCHQvPSJr4nW9yFTSloWiKdduA1guPLVgYHzv8=", "Member" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1003);
        }
    }
}
