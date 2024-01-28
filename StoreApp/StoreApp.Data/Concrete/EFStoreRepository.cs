using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;

namespace StoreApp.Data.Concrete;

/*İlerde veri tabanı türünü değiştirmek istersek tek değiştirmemiz gereken
kısım EFStoreRepository kısmıdır (yani bu dosya) diğer her yerde 
EFStoreRepository yerine onun interface versiyonu olan IStoreRepository
ögesini kullandığımız için programın geri kalanında değişiklik yapmamız
gerekmeyecek (Program.cs dosyasında da interface atamasını değiştirmeliyiz)
*/
public class EFStoreRepository : IStoreRepository
{
    private StoreDbContext _context;

    public EFStoreRepository(StoreDbContext context)
    {
        _context = context;
    }
    public IQueryable<Product> Products => _context.Products;

    public IQueryable<Category> Categories => _context.Categories;

    public void CreateProduct(Product entity)
    {
        throw new NotImplementedException();
    }

    public int GetProductCount(string category)
    {
        return category == null ? Products.Count() : Products.Include(p => p.Categories)
        .Where(p => p.Categories.Any(a => a.Url == category)).Count();
    }

    public IEnumerable<Product> GetProductsByCategory(string kategori, int sayfa, int sayfaBoyutu)
    {
        var urunler = Products;
        if (!string.IsNullOrEmpty(kategori))
        {
            urunler = urunler.Include(p => p.Categories).Where(p => p.Categories.Any(a => a.Url == kategori));
        }

        /*veri tabanındaki ilk ((page-1)*pageSize) adet veriyi atlar ve devamındaki verileri getirir.*/
       return urunler.Skip((sayfa - 1) * sayfaBoyutu).Take(sayfaBoyutu);/* veri tabanından pageSize adedi kadar veriyi getirir*/;
    }
}