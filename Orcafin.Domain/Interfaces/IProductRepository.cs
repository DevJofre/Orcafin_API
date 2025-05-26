using Orcafin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orcafin.Domain.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<Product> AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(int id);
        Task<bool> ProductExists(int id);
        Task<IEnumerable<Product>> GetProductsWithLowStock(int threshold = 5);
    }
}