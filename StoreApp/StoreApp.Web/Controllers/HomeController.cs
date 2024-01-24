using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Abstract;
using StoreApp.Web.Models;

namespace StoreApp.Web.Controllers;/*namespace nin yeni gelen özelliği sayesinde
parantez yerine ; kullanabiliyoruz*/

public class HomeController : Controller
{
    /*Burada interface ile çalıştığımız için tamamen bağımsız bir
    yapıda çalışmış oluyoruz [IStoreRepository => EFStoreRepository]*/
    private IStoreRepository _storeRepository;
    public HomeController(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    /*Sayfaya verileri dönüştürerek gönderelim*/
    public IActionResult Index()
    {
        var products = _storeRepository.Products.Select(p => new ProductViewModel
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price
        }).ToList();

        return View(new ProductListViewModel{
            Products = products
        });
    } //Modeli dönüştürerek sayfa üzerine gönderelim
}