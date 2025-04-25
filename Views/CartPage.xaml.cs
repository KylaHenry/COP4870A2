using COP4870.ECommerce.Services;

namespace EcommerceApp.Views;

public partial class CartPage : ContentPage
{
    private readonly ProductService _service;

    public CartPage(ProductService service)
    {
        InitializeComponent();
        _service = service;
        _service.CartChanged += RefreshList;
        RefreshList();
    }

    private void RefreshList()
    {
        CartList.ItemsSource = null;
        CartList.ItemsSource = _service.Cart;
    }

    private void OnRemoveClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is COP4870.ECommerce.Models.Product product)
        {
            _service.RemoveFromCart(product);
        }
    }

    private async void OnCheckoutClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("checkout");
    }
}
