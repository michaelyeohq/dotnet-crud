using System.Collections.Generic;
using Store.Models;

namespace Store.Data.Products
{
    public interface IProductRepo
    {
        // Save db changes
        bool SaveChanges();
        // Get All Products
        IEnumerable<Product> GetAllProducts();
        // Get One Product
        Product GetProductById(int id);
        // Create One Product
        void CreateProduct(Product prdt);
        // Update One Product
        void UpdateProduct(Product prdt);
        // Delete One Product
        void DeleteProduct(Product prdt);
    }
}