using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderForListWindow.xaml
    /// </summary>
    public partial class OrderForListWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        //public  ObservableCollection<OrderForList> OrderList;
        //IEnumerable<OrderForList> List;

        DependencyProperty OrdersDep = DependencyProperty.Register(nameof(OrderList), typeof(IEnumerable<OrderForList>), typeof(OrderForListWindow));
        IEnumerable<OrderForList> OrderList { get => (IEnumerable<OrderForList>)GetValue(OrdersDep); set => SetValue(OrdersDep, value); }
            public OrderForListWindow()
        {
            InitializeComponent();
            OrderList = bl?.Order.GetOrders()!;
        }

        private void OrderItem(object sender, MouseButtonEventArgs e)
        {
            new OrderItemWindow().Show();
        }
    }
}
