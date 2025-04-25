using COP4870.ECommerce.Models;
using COP4870.ECommerce.Services;

namespace COP4870.ECommerce.UI.MAUI
{
    [QueryProperty(nameof(RefreshRequired), "refresh")]
    public partial class InventoryPage : ContentPage
    {
        private readonly ProductService _productService;
        private readonly ShoppingCartService _cartService;
        private bool _refreshRequired;

        public string RefreshRequired
        {
            set
            {
                _refreshRequired = string.IsNullOrEmpty(value) ? false : Convert.ToBoolean(value);
                if (_refreshRequired)
                {
                    LoadProducts();
                }
            }
        }

        public InventoryPage(ProductService productService, ShoppingCartService cartService)
        {
            InitializeComponent();
            _productService = productService;
            _cartService = cartService;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadProducts();
        }

        private void LoadProducts()
        {
            ProductsListView.ItemsSource = _productService.ListProducts();
        }

        private async void OnProductSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Product product)
            {
                // Clear selection
                ((ListView)sender).SelectedItem = null;

                // Navigate to product detail
                var navigationParameter = new Dictionary<string, object>
                {
                    { "productId", product.Id.ToString() }
                };
                await Shell.Current.GoToAsync("productdetail", navigationParameter);
            }
        }

        private async void OnAddProductClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("productdetail");
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//main");
        }

        private async void OnAddToCartClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is int productId)
            {
                var product = _productService.GetProduct(productId);
                if (product == null)
                {
                    await DisplayAlert("Error", "Product not found", "OK");
                    return;
                }

                if (product.Quantity <= 0)
                {
                    await DisplayAlert("Error", "Product is out of stock", "OK");
                    return;
                }

                // Add one of this product to cart
                _cartService.AddToCart(product, 1);

                // Update inventory quantity
                product.Quantity--;
                _productService.UpdateProduct(product);

                // Update the UI
                LoadProducts();
                StatusLabel.Text = $"Added {product.Name} to cart";

                // Clear status after 3 seconds
                await Task.Delay(3000);
                StatusLabel.Text = "";
            }
        }
    }
}