namespace COP4870.ECommerce.UI.MAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register routes for navigation
            Routing.RegisterRoute("productdetail", typeof(ProductDetailPage));
        }
    }
}