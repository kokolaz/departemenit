using DepartemenIT.Models;
using DepartemenIT.ViewModel;

namespace DepartemenIT.Repository.Interface
{
    public interface ISaleRepository
    {
        IEnumerable<Sale> GetAll();
        Sale Get(int id);
        int Insert (SaleVM saleVM);
        int Update(UpdateSaleVM updateSaleVM);
        int Delete(int SaleId);
    }
}
