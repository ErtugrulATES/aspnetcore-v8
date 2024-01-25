namespace StoreApp.Web.Models;

public class ProductViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Category { get; set; } = string.Empty;
}

public class ProductListViewModel
{
    public IEnumerable<ProductViewModel> Products { get; set; } = Enumerable.Empty<ProductViewModel>();
    public PageInfo PageInfo { get; set; } = new();/* oluşturduğumuz yeni sınıfı sayfaya gönderelim*/
}

/*bu sınıfları ayrı dosyalar içerisinde de tanımlayabilirdik*/
/*buradaki amacımız veri sayısına göre sayfa sayısını ayarlamak ve boş sayfa oluşturulmasını önlemektir.*/
public class PageInfo
{
    public int TotalItems { get; set; } /*Toplam veri sayısı*/
    public int ItemsPerPage { get; set; } /*Sayfa başına veri sayısı*/   
    public int CurrentPage { get; set; } /*Şeçili olan sayfanın numarası*/
    public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);/*Sayfa sayısı*/
}