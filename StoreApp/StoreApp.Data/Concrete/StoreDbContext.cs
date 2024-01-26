using Microsoft.EntityFrameworkCore;
namespace StoreApp.Data.Concrete;

public class StoreDbContext : DbContext
{
    public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
    {

    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        /*Bilinçli olarak veri tabanına çoka çok şema ekledik*/
        modelBuilder.Entity<Product>()
                .HasMany(e => e.Categories)
                .WithMany(e => e.Products)
                .UsingEntity<ProductCategory>();

        /*Category tablosunda Url bilgisi benzersiz yapıldı*/
        modelBuilder.Entity<Category>()
                .HasIndex(u => u.Url)
                .IsUnique();


        /*Bu yeni çıkan bir yöntemdir ve .NET 8 ile birlikte gelmiştir.*/
        modelBuilder.Entity<Product>().HasData(
            new List<Product>(){
                new(){Id=1,Name="Nokia 3200", Price=350, Description="İlk Telefonum"},
                new(){Id=2,Name="Nokia 5800", Price=480, Description="İkinci Telefonum"},
                new(){Id=3,Name="Nokia Lumia 735", Price=520, Description="İlk Akıllı Telefonum"},
                new(){Id=4,Name="Xiaomi Redmi Note 8 Pro", Price=2160, Description="İlk Android Telefonum"},
                new(){Id=5,Name="Huawei MatPad 11.5 PaperMatte", Price=13099, Description="İlk Tabletim"},
                new(){Id=6,Name="Huawei Free Buds 5 Se", Price=1099, Description="İlk Kablosuz Kulaklığım"},
                new(){Id=7,Name="Sony Kablolu Kulaklık", Price=160, Description="Mavi Renk"},
                new(){Id=8,Name="Ttec Kulaklık", Price=140, Description="Pembe Renk"},
                new(){Id=9,Name="Renault Kangoo Multix", Price=13099, Description="2009 Model Panelvan"},
                new(){Id=10,Name="Colin's Çanta", Price=13099, Description="Tableti taşımak için"}
            }
        );/*Has data içerisinde mutlaka ID bilgisi verilmelidir. 
        Bu kodlar bir migration oluşturup uyguladığımız zaman oluşturulur ve uygulanır*/

        modelBuilder.Entity<Category>().HasData(
            new List<Category>(){
                new(){Id=1,Name="Telefon", Url="telefon"},
                new(){Id=2,Name="Tablet", Url="tablet"},
                new(){Id=3,Name="Aksesuar", Url="aksesuar"},
                new(){Id=4,Name="Araç", Url="arac"},
            }
        );

        modelBuilder.Entity<ProductCategory>().HasData(
            new List<ProductCategory>(){
                new(){ProductId =  1, CategoryId = 1},
                new(){ProductId =  2, CategoryId = 1},
                new(){ProductId =  3, CategoryId = 1},
                new(){ProductId =  4, CategoryId = 1},
                new(){ProductId =  4, CategoryId = 2},
                new(){ProductId =  5, CategoryId = 2},
                new(){ProductId =  6, CategoryId = 3},
                new(){ProductId =  7, CategoryId = 3},
                new(){ProductId =  8, CategoryId = 3},
                new(){ProductId =  9, CategoryId = 4},
                new(){ProductId =  10, CategoryId = 3}
            }
        );
    }
}