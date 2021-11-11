using System;
using System.Collections.Generic;
using System.Linq;
using Store.Models;

namespace Store.Data
{
    public class ProductRepo : IProductRepo
    {
        private readonly Context _context;
        public ProductRepo(Context context)
        {
            _context = context;
            
        }
        public void CreateProduct(Product prdt)
        {
            if(prdt == null)
            {
                throw new ArgumentNullException(nameof(prdt));
            }
            _context.Products.Add(prdt);
        }

        public void DeleteProduct(Product prdt)
        {
            if (prdt == null)
            {
                throw new ArgumentNullException(nameof(prdt));
            }
            _context.Products.Remove(prdt);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateProduct(Product prdt)
        {
            // _context.Products.Update(prdt);
        }
    }
}