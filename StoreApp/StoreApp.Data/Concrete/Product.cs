namespace StoreApp.Data.Concrete;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    /*Kategoriler tablosuna bir referans verdik
    Çoka çok tablo oluşturmak için*/
    public List<Category> Categories { get; set; } = new();
}