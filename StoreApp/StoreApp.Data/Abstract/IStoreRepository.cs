using StoreApp.Data.Concrete;

namespace StoreApp.Data.Abstract;

public interface IStoreRepository
{
    IQueryable<Product> Products {get;}  //sorguyu gelen parametrelere göre filtreleyip en son aşamada veri tabanından verileri filtreleyerek alır, daha performanslı bir sorgudur.

    void CreateProduct(Product entity);
}