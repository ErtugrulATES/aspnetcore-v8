using AutoMapper;
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
    private readonly IStoreRepository _storeRepository;

    private readonly IMapper _mapper; /*AutoMapper nesnesini inject edelim*/
    public HomeController(IStoreRepository storeRepository, IMapper mapper)
    {
        _storeRepository = storeRepository;
        _mapper = mapper;
    }

    /*Sayfaya verileri dönüştürerek gönderelim*/
    /* localhost:XXXX/?page=2 QueryString yapısı için aşağıdaki fonksiyonu düzenleyelim*/
    public IActionResult Index(string kategori, int page = 1)
    {
        return View(new ProductListViewModel
        {
            Products = _storeRepository.GetProductsByCategory(kategori, page, pageSize)
            /*AutoMapper ile kod yazımını kısalttık*/
            .Select(product => _mapper.Map<ProductViewModel>(product)),
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