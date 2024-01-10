using DepartemenIT.Context;
using DepartemenIT.Models;
using DepartemenIT.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DepartemenIT.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyContext context;
        public ProductRepository(MyContext context)
        {
            this.context = context;
        }
        public int Insert(Product product)
        {
            Product product1 = new()
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price
            };
            context.Products.Add(product1);
            var result = context.SaveChanges();

            return result;
        }

        public IEnumerable<Product> GetAll()
        {
            return context.Products.ToList();
        }
        public Product GetById(int ProductId)
        {
            var entity = context.Products.Find(ProductId);
            return entity;
        }
        public int Update(Product product)
        {
            var entity = context.Products.Find(product.ProductId);
            if(entity == null)
            {
                return 0;
            }
            else
            {
                entity.ProductId = product.ProductId;
                entity.Name = product.Name;
                entity.Price = product.Price;
                context.Entry(entity).State = EntityState.Modified;
                var result = context.SaveChanges();
                return result;
            }
        }
        public int Delete(int ProductId)
        {
            var entity = context.Products.Find(ProductId);
            context.Remove(entity);
            var result = context.SaveChanges();
            return result;
        }
    }
}
