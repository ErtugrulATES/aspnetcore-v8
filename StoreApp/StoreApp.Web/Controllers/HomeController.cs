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
        return View(new ProductListViewModel
        {
            Products = _storeRepository.GetProductsByCategory(kategori, page, pageSize)
            .Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            }),
            PageInfo = new PageInfo
            {/*sayfa bilgisi verisi*/
                ItemsPerPage = pageSize,/*sayfa başına gösterilecek veri sayısı*/
                CurrentPage = page,/*seçili sayfanın sayfa numarası*/
                /*Toplam veya filtrelenmiş veri sayisi*/
                TotalItems = _storeRepository.GetProductCount(kategori)
            }
        });
    } //Modeli dönüştürerek sayfa üzerine gönderelim
}