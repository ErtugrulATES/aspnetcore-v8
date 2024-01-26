using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;
using StoreApp.Data.Concrete;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();/*MVC yapısını projeye tanıtır*/

builder.Services.AddDbContext<StoreDbContext>(options =>
{
   options.UseSqlite(builder.Configuration["ConnectionStrings:StoreDbConnection"], b => b.MigrationsAssembly("StoreApp.Web"));
}); /* "StoreApp.Web" klasörünü projeye tanıtalım*/

builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();/* bu kod
IStoreRepository bizden ne zaman çağırılırsa bize EFStoreRepository ögesini döndürür
bu özellik .NET 8.0.0 kütüphanesi ile geldi.*/

var app = builder.Build();/*Projeyi inşa eder*/

app.UseStaticFiles();/*İnşa edilen projede Statik dosyaların kullanılmasını sağlar*/

#region Routing İşlemleri
/* host/urunler/telefon => kategori ürün listesi (? => OPSİYONEL DEMEK)
daha özel şemaları yukarıya taşı ki program ilk bulduğunu çalıştırdığında önce özel olan çalışsın */
app.MapControllerRoute("kategorideki_urunler", "urunler/{kategori?}", new { controller = "Home", Action = "Index" });

/* host/nokia3200 => ürün detayı  */
app.MapControllerRoute("urun_detaylari", "{urunAdi}", new { controller = "Home", Action = "Details" });
#endregion
/*controller/view/{id} formatında varsayılan rota şeması oluşturur.*/
app.MapDefaultControllerRoute();

app.Run();/*Uygulamayı çalıştırır*/