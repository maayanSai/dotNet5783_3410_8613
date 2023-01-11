namespace PL;
using System.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public BO.Cart c;
    BlApi.IBl? bl = BlApi.Factory.Get();
    /// <summary>
    /// constructive action
    /// </summary>
    
    public MainWindow()
    {
        InitializeComponent();
        c=new BO.Cart
        {
            CustomerName="ayala",
            CustomerEmail="aya@gmail.com",
            CustomerAdress="hapalmach 15  tel-Aviv",
            TotalPrice=0,
            Items=new()
        };
    }
    /// <summary>
    /// Entering the product menu
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ShowProductsButton_Click(object sender, RoutedEventArgs e) => new OrdersAndProducts().Show();

    private void Button_Click(object sender, RoutedEventArgs e)=>new Catalog(c).ShowDialog();    
   
}
