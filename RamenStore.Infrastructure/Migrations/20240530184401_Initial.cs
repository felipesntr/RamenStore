using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RamenStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Broths",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ImageInactive = table.Column<string>(type: "TEXT", nullable: false),
                    ImageActive = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Broths", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    BrothId = table.Column<string>(type: "TEXT", nullable: false),
                    ProteinId = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proteins",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ImageInactive = table.Column<string>(type: "TEXT", nullable: false),
                    ImageActive = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proteins", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Broths",
                columns: new[] { "Id", "Description", "ImageActive", "ImageInactive", "Name", "Price" },
                values: new object[,]
                {
                    { "1", "Simple like the seawater, nothing more", "https://tech.redventures.com.br/icons/salt/active.svg", "https://tech.redventures.com.br/icons/salt/inactive.svg", "Salt", 10m },
                    { "2", "The good old and traditional soy sauce", "https://tech.redventures.com.br/icons/shoyu/active.svg", "https://tech.redventures.com.br/icons/shoyu/inactive.svg", "Shoyu", 10m },
                    { "3", "Paste made of fermented soybeans", "https://tech.redventures.com.br/icons/miso/active.svg", "https://tech.redventures.com.br/icons/miso/inactive.svg", "Miso", 12m }
                });

            migrationBuilder.InsertData(
                table: "Proteins",
                columns: new[] { "Id", "Description", "ImageActive", "ImageInactive", "Name", "Price" },
                values: new object[,]
                {
                    { "1", "A sliced flavourful pork meat with a selection of season vegetables.", "https://tech.redventures.com.br/icons/pork/active.svg", "https://tech.redventures.com.br/icons/pork/inactive.svg", "Chasu", 10m },
                    { "2", "A delicious vegetarian lamen with a selection of season vegetables.", "https://tech.redventures.com.br/icons/yasai/active.svg", "https://tech.redventures.com.br/icons/yasai/inactive.svg", "Yasai Vegetarian", 10m },
                    { "3", "Three units of fried chicken, moyashi, ajitama egg and other vegetables.", "https://tech.redventures.com.br/icons/chicken/active.svg", "https://tech.redventures.com.br/icons/chicken/inactive.svg", "Karaague", 12m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Broths");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Proteins");
        }
    }
}
