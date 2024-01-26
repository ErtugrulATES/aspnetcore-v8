using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;
using StoreApp.Data.Concrete;
using StoreApp.Web.Models;

namespace StoreApp.Web.Controllers;/*namespace nin yeni gelen özelliği sayesinde
parantez yerine ; kullanabiliyoruz*/

public class HomeController : Controller
{
    public int pageSize = 3; /*bir sayfada kaç adet ürünün gösterileceği bilgisi*/
    /*Burada interface ile çalıştığımız için tamamen bağımsız bir
    yapıda çalışmış oluyoruz [IStoreRepository => EFStoreRepository]*/
    private IStoreRepository _storeRepository;
    public HomeController(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    /*Sayfaya verileri dönüştürerek gönderelim*/
    /* localhost:XXXX/?page=2 QueryString yapısı için aşağıdaki fonksiyonu düzenleyelim*/
    public IActionResult Index(string kategori, int page = 1)
    {
        int veriSayisi = 0;

        var sorgu = _storeRepository.Products;
        if (!string.IsNullOrEmpty(kategori))
        {
            sorgu = sorgu.Include(p => p.Categories).Where(p => p.Categories.Any(a => a.Url == kategori));
        }
        veriSayisi = sorgu.Count();
        /*veri tabanındaki ilk ((page-1)*pageSize) adet veriyi atlar ve devamındaki verileri getirir.*/
        sorgu = sorgu.Skip((page - 1) * pageSize);

        var products = sorgu
        .Select(p => new ProductViewModel
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price
        }).Take(pageSize);/* veri tabanından pageSize adedi kadar veriyi getirir*/

        return View(new ProductListViewModel
        {
            Products = products, /*numarası belirtilen sayfada gösterilecek veriler*/
            PageInfo = new PageInfo
            {/*sayfa bilgisi verisi*/
                ItemsPerPage = pageSize,/*sayfa başına gösterilecek veri sayısı*/
                CurrentPage = page,/*seçili sayfanın sayfa numarası*/
                /*Toplam veya filtrelenmiş veri sayisi*/
                TotalItems = veriSayisi
            }
        });
    } //Modeli dönüştürerek sayfa üzerine gönderelim
}