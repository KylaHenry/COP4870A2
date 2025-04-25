using COP4870.ECommerce.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace COP4870.ECommerce.Services
{
    public class ProductService
    {
        private readonly List<Product> _products = new List<Product>();
        private int _nextId = 1;
        internal Action CartChanged;

        public IEnumerable? Cart { get; internal set; }

        public Product CreateProduct(Product product)
        {
            product.Id = _nextId++;
            _products.Add(product);
            return product;
        }

        public Product GetProduct(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public List<Product> ListProducts()
        {
            return _products.ToList();
        }

        public void UpdateProduct(Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.Quantity = product.Quantity;
            }
        }

        public void DeleteProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _products.Remove(product);
            }
        }

        internal void RemoveFromCart(Product product)
        {
            throw new NotImplementedException();
        }
    }
}