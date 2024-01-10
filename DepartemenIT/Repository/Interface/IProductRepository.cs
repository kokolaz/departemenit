using DepartemenIT.Models;

namespace DepartemenIT.Repository.Interface
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(int ProductId);
        int Insert(Product Product);
        int Update(Product Product);
        int Delete(int ProductId);
    }
}
