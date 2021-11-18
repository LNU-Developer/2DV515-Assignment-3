using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class AddWordReferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Words_Blogs_BlogId",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Words_BlogId",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Words");

            migrationBuilder.CreateTable(
                name: "WordReferences",
                columns: table => new
                {
                    WordReferenceId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Count = table.Column<int>(type: "INTEGER", nullable: false),
                    BlogId = table.Column<int>(type: "INTEGER", nullable: false),
                    WordId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordReferences", x => x.WordReferenceId);
                    table.ForeignKey(
                        name: "FK_WordReferences_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "BlogId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WordReferences_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "WordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WordReferences_BlogId",
                table: "WordReferences",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_WordReferences_WordId",
                table: "WordReferences",
                column: "WordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WordReferences");

            migrationBuilder.AddColumn<int>(
                name: "BlogId",
                table: "Words",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Words",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Words_BlogId",
                table: "Words",
                column: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Words_Blogs_BlogId",
                table: "Words",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "BlogId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
