using COP4870.ECommerce.Models;
using COP4870.ECommerce.Services;

namespace COP4870.ECommerce.UI.MAUI
{
    [QueryProperty(nameof(ProductId), "productId")]
    public partial class ProductDetailPage : ContentPage
    {
        private readonly ProductService _productService;
        private int? _productId;
        private Product _product;

        public string ProductId
        {
            set
            {
                if (int.TryParse(value, out int id))
                {
                    _productId = id;
                    LoadProduct();
                }
            }
        }

        public ProductDetailPage(ProductService productService)
        {
            InitializeComponent();
            _productService = productService;
        }

        private void LoadProduct()
        {
            if (_productId.HasValue)
            {
                _product = _productService.GetProduct(_productId.Value);
                if (_product != null)
                {
                    // Load product data into UI
                    NameEntry.Text = _product.Name;
                    DescriptionEditor.Text = _product.Description;
                    PriceEntry.Text = _product.Price.ToString("F2");
                    QuantityEntry.Text = _product.Quantity.ToString();

                    // Show delete button for existing products
                    DeleteButton.IsVisible = true;
                }
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameEntry.Text))
            {
                await DisplayAlert("Error", "Product name is required", "OK");
                return;
            }

            if (!decimal.TryParse(PriceEntry.Text, out decimal price))
            {
                await DisplayAlert("Error", "Invalid price format", "OK");
                return;
            }

            if (!int.TryParse(QuantityEntry.Text, out int quantity))
            {
                await DisplayAlert("Error", "Invalid quantity format", "OK");
                return;
            }

            if (_product == null)
            {
                // Create new product
                _product = new Product
                {
                    Name = NameEntry.Text,
                    Description = DescriptionEditor.Text,
                    Price = price,
                    Quantity = quantity
                };
                _productService.CreateProduct(_product);
            }
            else
            {
                // Update existing product
                _product.Name = NameEntry.Text;
                _product.Description = DescriptionEditor.Text;
                _product.Price = price;
                _product.Quantity = quantity;
                _productService.UpdateProduct(_product);
            }

            // Navigate back
            await Shell.Current.GoToAsync("..");
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Confirm", "Are you sure you want to delete this product?", "Yes", "No");
            if (answer && _product != null)
            {
                _productService.DeleteProduct(_product.Id);
                await Shell.Current.GoToAsync("..");
            }
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}