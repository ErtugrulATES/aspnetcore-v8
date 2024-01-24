using Microsoft.EntityFrameworkCore;
namespace StoreApp.Data.Concrete;

public class StoreDbContext:DbContext
{
    public StoreDbContext(DbContextOptions<StoreDbContext> options): base(options)
    {
        
    }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().HasData(
            new List<Product>(){
                new(){Id=1,Name="Nokia 3200", Price=350, Description="İlk Telefonum", Category="Telefon"},
                new(){Id=2,Name="Nokia 5800", Price=480, Description="İkinci Telefonum", Category="Telefon"},
                new(){Id=3,Name="Nokia Lumia 735", Price=520, Description="İlk Akıllı Telefonum", Category="Telefon"},
                new(){Id=4,Name="Xiaomi Redmi Note 8 Pro", Price=2160, Description="İlk Android Telefonum", Category="Telefon"},
                new(){Id=5,Name="Huawei MatPad 11.5 PaperMatte", Price=13099, Description="İlk Tabletim", Category="Tablet"}
            }
        );/*Has data içerisinde mutlaka ID bilgisi verilmelidir. Bu kodlar bir migration oluşturup uyguladığımız zaman oluşturulur ve uygulanır*/
    }
}