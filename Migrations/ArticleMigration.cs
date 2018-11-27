using DbContexts.Article;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrations.Migrations
{
    [DbContext(typeof(ArticleDbContext))]
    //[Migration("ArticleMigration")]
    public class ArticleMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerLocation",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerLocation", x => x.ID);
                });

            migrationBuilder.Sql("ALTER TABLE CustomerLocation " +
                                 "ADD GeogCol1 geography, " +
                                 "GeogCol2 AS GeogCol1.STAsText()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerLocation");
            // TODO: Implemnent this how you see fit
            base.Down(migrationBuilder);
        }
    }
}
