using Microsoft.Maui.Controls;

namespace EcommerceApp.Views;

public partial class MainMenuPage : ContentPage
{
    public MainMenuPage()
    {
        InitializeComponent();
    }

    private async void OnInventoryClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("inventory");
    }

    private async void OnCartClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("cart");
    }
}
