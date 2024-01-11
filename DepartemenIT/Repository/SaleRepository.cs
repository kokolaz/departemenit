using DepartemenIT.Context;
using DepartemenIT.Models;
using DepartemenIT.ViewModel;
using DepartemenIT.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DepartemenIT.Repository
{
    public class SaleRepository : ISaleRepository
    {
        private readonly MyContext context;
        public SaleRepository(MyContext context)
        {
            this.context = context;
        }
        public int Insert(SaleVM sale)
        {
            Sale sale1 = new()
            {
                ProductId = sale.ProductId,
                Quantity = sale.Quantity
            };
            context.Sales.Add(sale1);
            var result = context.SaveChanges();

            return result;
        }
        public IEnumerable<Sale> GetAll()
        {
            return context.Sales.Include(s => s.Product).ToList();
        }
        public Sale Get(int id)
        {
            var entity = context.Sales.Find(id);
            return entity;
        }
        public int Update(UpdateSaleVM updateSaleVM)
        {
            var entity = context.Sales.Find(updateSaleVM.SaleId);
            if (entity == null)
            {
                return 0;
            }
            else
            {
                entity.ProductId = updateSaleVM.ProductId;
                entity.Quantity = updateSaleVM.Quantity;
                context.Entry(entity).State = EntityState.Modified;
                var result = context.SaveChanges();
                return result;
            }
        }
        public int Delete(int id)
        {
            var entity = context.Sales.Find(id);
            context.Remove(entity);
            var result = context.SaveChanges();
            return result;
        }
    }
}
