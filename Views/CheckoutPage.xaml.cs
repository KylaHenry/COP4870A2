using COP4870.ECommerce.Services;
using COP4870.ECommerce.Models;

namespace COP4870.ECommerce.UI.MAUI
{
    public partial class CheckoutPage : ContentPage
    {
        private readonly ShoppingCartService _cartService;
        private const decimal TaxRate = 0.07m; // 7% tax rate

        public CheckoutPage(ShoppingCartService cartService)
        {
            InitializeComponent();
            _cartService = cartService;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GenerateReceipt();
        }

        private void GenerateReceipt()
        {
            // Set date and time
            DateTimeLabel.Text = DateTime.Now.ToString("MMMM dd, yyyy hh:mm tt");

            // Clear any existing items
            ReceiptItemsLayout.Clear();

            // Get cart items
            var cartItems = _cartService.GetCartItems();
            decimal subtotal = 0;

            // Add items to receipt
            int row = 1;
            foreach (var item in cartItems)
            {
                // Create a grid for this item
                var grid = new Grid
                {
                    ColumnDefinitions = new ColumnDefinitionCollection
                    {
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = GridLength.Auto },
                        new ColumnDefinition { Width = GridLength.Auto },
                        new ColumnDefinition { Width = GridLength.Auto }
                    }
                };

                // Add item details
                grid.Add(new Label { Text = item.Product.Name }, 0, 0);
                grid.Add(new Label { Text = item.Quantity.ToString(), HorizontalOptions = LayoutOptions.Center }, 1, 0);
                grid.Add(new Label { Text = $"${item.Product.Price:F2}", HorizontalOptions = LayoutOptions.End }, 2, 0);
                grid.Add(new Label { Text = $"${item.LineTotal:F2}", HorizontalOptions = LayoutOptions.End }, 3, 0);

                // Add to layout
                ReceiptItemsLayout.Add(grid);

                subtotal += item.LineTotal;
                row++;
            }

            // Calculate tax and total
            decimal tax = subtotal * TaxRate;
            decimal total = subtotal + tax;

            // Update totals
            SubtotalLabel.Text = $"${subtotal:F2}";
            TaxLabel.Text = $"${tax:F2}";
            TotalLabel.Text = $"${total:F2}";

            // Clear the cart after checkout
            _cartService.ClearCart();
        }

        private async void OnMainMenuClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//main");
        }
    }
}