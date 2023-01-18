using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL;

/// <summary>
/// Interaction logic for OrdersAndProducts.xaml
/// </summary>
public partial class OrdersAndProducts : Window
{
    public OrdersAndProducts()
    {
        InitializeComponent();
    }
    private void Order_Click(object sender, RoutedEventArgs e) => new OrderForListWindow().Show();
    private void Product_Click(object sender, RoutedEventArgs e) => new ProductListWindow().Show();

}
