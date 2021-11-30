using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class DeleteWordReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WordReferences");

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "Words",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Max",
                table: "Words",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Min",
                table: "Words",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BlogWord",
                columns: table => new
                {
                    BlogsBlogId = table.Column<int>(type: "INTEGER", nullable: false),
                    WordsWordId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogWord", x => new { x.BlogsBlogId, x.WordsWordId });
                    table.ForeignKey(
                        name: "FK_BlogWord_Blogs_BlogsBlogId",
                        column: x => x.BlogsBlogId,
                        principalTable: "Blogs",
                        principalColumn: "BlogId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogWord_Words_WordsWordId",
                        column: x => x.WordsWordId,
                        principalTable: "Words",
                        principalColumn: "WordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogWord_WordsWordId",
                table: "BlogWord",
                column: "WordsWordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogWord");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "Max",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "Min",
                table: "Words");

            migrationBuilder.CreateTable(
                name: "WordReferences",
                columns: table => new
                {
                    WordReferenceId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BlogId = table.Column<int>(type: "INTEGER", nullable: false),
                    Count = table.Column<int>(type: "INTEGER", nullable: false),
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
    }
}
