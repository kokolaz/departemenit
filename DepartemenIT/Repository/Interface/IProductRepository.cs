using DepartemenIT.Models;
using DepartemenIT.ViewModel;

namespace DepartemenIT.Repository.Interface
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(int ProductId);
        int Insert(ProductVM Product);
        int Update(Product Product);
        int Delete(int ProductId);
    }
}
