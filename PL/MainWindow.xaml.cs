namespace PL;
using System.Windows;


/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    BlApi.IBl? bl = BlApi.Factory.Get();
    /// <summary>
    /// constructive action
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
    }
    /// <summary>
    /// Entering the product menu
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ShowProductsButton_Click(object sender, RoutedEventArgs e) => new ProductListWindow().Show();
}
