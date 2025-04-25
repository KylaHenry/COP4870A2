using COP4870.ECommerce.Models;
using COP4870.ECommerce.Services;

namespace COP4870.ECommerce.UI.MAUI
{
    public partial class ShoppingCartPage : ContentPage
    {
        private readonly ProductService _productService;
        private readonly ShoppingCartService _cartService;
        private const decimal TaxRate = 0.07m; // 7% tax rate

        public ShoppingCartPage(ProductService productService, ShoppingCartService cartService)
        {
            InitializeComponent();
            _productService = productService;
            _cartService = cartService;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshCart();
        }

        private void RefreshCart()
        {
            var cartItems = _cartService.GetCartItems();
            CartItemsListView.ItemsSource = cartItems;

            // Calculate totals
            decimal subtotal = cartItems.Sum(item => item.LineTotal);
            decimal tax = subtotal * TaxRate;
            decimal total = subtotal + tax;

            // Update labels
            SubtotalLabel.Text = $"Subtotal: ${subtotal:F2}";
            TaxLabel.Text = $"Tax (7%): ${tax:F2}";
            TotalLabel.Text = $"Total: ${total:F2}";
        }

        private void OnIncreaseQuantityClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is int productId)
            {
                var product = _productService.GetProduct(productId);
                if (product == null)
                    return;

                // Check if there's inventory available
                if (product.Quantity <= 0)
                {
                    DisplayAlert("Error", "No more inventory available for this product", "OK");
                    return;
                }

                // Add one to cart
                _cartService.AddToCart(product, 1);

                // Decrease inventory
                product.Quantity--;
                _productService.UpdateProduct(product);

                // Refresh display
                RefreshCart();
            }
        }

        private void OnDecreaseQuantityClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is int productId)
            {
                var product = _productService.GetProduct(productId);
                if (product == null)
                    return;

                // Remove one from cart
                _cartService.RemoveFromCart(productId, 1);

                // Return one to inventory
                product.Quantity++;
                _productService.UpdateProduct(product);

                // Refresh display
                RefreshCart();
            }
        }

        private void OnRemoveFromCartClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is int productId)
            {
                // Get quantity in cart before removal
                int quantityInCart = _cartService.GetQuantityInCart(productId);

                // Remove all from cart
                _cartService.RemoveAllFromCart(productId);

                // Return all to inventory
                var product = _productService.GetProduct(productId);
                if (product != null)
                {
                    product.Quantity += quantityInCart;
                    _productService.UpdateProduct(product);
                }

                // Refresh display
                RefreshCart();
            }
        }

        private async void OnContinueShoppingClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//inventory");
        }

        private async void OnCheckoutClicked(object sender, EventArgs e)
        {
            if (_cartService.GetCartItems().Count == 0)
            {
                await DisplayAlert("Error", "Your cart is empty", "OK");
                return;
            }

            await Shell.Current.GoToAsync("//checkout");
        }
    }
}