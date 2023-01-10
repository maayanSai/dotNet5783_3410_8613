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
        public ObservableCollection<OrderForList> OrderList
        {
            get { return (ObservableCollection<OrderForList>)GetValue(OrdersDep); }
            set  { SetValue(OrdersDep, value); }
        }
        public static readonly DependencyProperty OrdersDep = DependencyProperty.Register("OrderList", typeof(IEnumerable<OrderForList>), typeof(OrderForListWindow));
       
            public OrderForListWindow()
        {
            InitializeComponent();
            OrderList = new (bl?.Order.GetOrders()!);
        }

       
       private void OrderForListDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled= true;
            BO.OrderForList? O = (OrderForList)((DataGrid)sender).SelectedItem;
            OrderItemWindow? orderWindow = new OrderItemWindow(O.ID);
            orderWindow.ShowDialog(); 


        }
    }
}
