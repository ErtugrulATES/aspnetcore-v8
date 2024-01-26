using StoreApp.Data.Concrete;

namespace StoreApp.Data.Abstract;

public interface IStoreRepository
{
    /*sorguyu gelen parametrelere göre filtreleyip
    en son aşamada veri tabanından verileri filtreleyerek alır,
    daha performanslı bir sorgudur.*/
    IQueryable<Product> Products {get;}  
    IQueryable<Category> Categories {get;}  
    void CreateProduct(Product entity);
}