using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingControlPanel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedRolesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName])
                VALUES (NEWID(), 'Admin', 'ADMIN'),
                       (NEWID(), 'User', 'USER');
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM [dbo].[AspNetRoles]
                WHERE [Name] IN ('Admin', 'User');
            ");
        }
    }
}
