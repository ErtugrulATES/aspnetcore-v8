using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StoreApp.Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Telefon", "İlk Telefonum", "Nokia 3200", 350m },
                    { 2, "Telefon", "İkinci Telefonum", "Nokia 5800", 480m },
                    { 3, "Telefon", "İlk Akıllı Telefonum", "Nokia Lumia 735", 520m },
                    { 4, "Telefon", "İlk Android Telefonum", "Xiaomi Redmi Note 8 Pro", 2160m },
                    { 5, "Tablet", "İlk Tabletim", "Huawei MatPad 11.5 PaperMatte", 13099m },
                    { 6, "Aksesuar", "İlk Kablosuz Kulaklığım", "Huawei Free Buds 5 Se", 1099m },
                    { 7, "Aksesuar", "Mavi Renk", "Sony Kablolu Kulaklık", 160m },
                    { 8, "Aksesuar", "Pembe Renk", "Ttec Kulaklık", 140m },
                    { 9, "Araba", "2009 Model Panelvan", "Renault Kangoo Multix", 13099m },
                    { 10, "Aksesuar", "Tableti taşımak için", "Colin's Çanta", 13099m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
