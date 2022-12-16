using BlApi;
using BlImplementation;
using System.Windows;
namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private IBl bl = new BL();
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ShowProductsButton_Click(object sender, RoutedEventArgs e) => new ProductListWindow().Show();

}
