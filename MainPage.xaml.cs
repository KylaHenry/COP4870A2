using COP4870.ECommerce.Services;

namespace COP4870.ECommerce.UI.MAUI;

public partial class MainPage : ContentPage
{
    private readonly ProductService _productService;
    private readonly ShoppingCartService _cartService;

    public MainPage(ProductService productService, ShoppingCartService cartService)
    {
        InitializeComponent();
        _productService = productService;
        _cartService = cartService;
    }

    private async void OnInventoryClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//inventory");
    }

    private async void OnCartClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//cart");
    }

    private async void OnCheckoutClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//checkout");
    }
}