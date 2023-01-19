using System.Data.Common;
using System.Windows;
namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public BO.Cart c;
    public BO.OrderTracking Ot;
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
        Ot = new BO.OrderTracking
        {
            ID = 0,
            Status =BO.OrderStatus.Delivered,
            Tracking = new()
        };
    }
    /// <summary>
    /// Entering the product menu
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Admin_Click(object sender, RoutedEventArgs e) => new OrdersAndProducts().Show();
    private void Catalog_Click(object sender, RoutedEventArgs e)=>new Catalog(c).ShowDialog();
    private void Tracking_Click(object sender, RoutedEventArgs e) =>new OrderTracking().ShowDialog();
}
