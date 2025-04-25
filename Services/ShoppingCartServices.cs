using COP4870.ECommerce.Models;
using System.Collections.Generic;
using System.Linq;

namespace COP4870.ECommerce.Services
{
    public class ShoppingCartService
    {
        private readonly List<CartItem> _cartItems = new();

        public IReadOnlyList<CartItem> GetCartItems()
        {
            return _cartItems.AsReadOnly();
        }

        public void AddToCart(Product product, int quantity)
        {
            var existingItem = _cartItems.FirstOrDefault(i => i.Product.Id == product.Id);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                _cartItems.Add(new CartItem
                {
                    Product = product,
                    Quantity = quantity
                });
            }
        }

        public void RemoveFromCart(int productId, int quantity)
        {
            var existingItem = _cartItems.FirstOrDefault(i => i.Product.Id == productId);

            if (existingItem != null)
            {
                existingItem.Quantity -= quantity;

                // Remove item completely if quantity is zero or less
                if (existingItem.Quantity <= 0)
                {
                    _cartItems.Remove(existingItem);
                }
            }
        }

        public void RemoveAllFromCart(int productId)
        {
            var existingItem = _cartItems.FirstOrDefault(i => i.Product.Id == productId);

            if (existingItem != null)
            {
                _cartItems.Remove(existingItem);
            }
        }

        public int GetQuantityInCart(int productId)
        {
            var existingItem = _cartItems.FirstOrDefault(i => i.Product.Id == productId);
            return existingItem?.Quantity ?? 0;
        }

        public void ClearCart()
        {
            _cartItems.Clear();
        }
    }
}