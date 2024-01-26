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
}