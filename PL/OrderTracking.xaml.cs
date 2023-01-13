using Microsoft.VisualBasic;
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
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderTracking.xaml
    /// </summary>
    public partial class OrderTracking : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
      
        /// <summary>
        /// constructive action
        /// </summary>
        public ObservableCollection<BO.OrderTracking?> orderTracking
        {
            get { return (ObservableCollection<BO.OrderTracking?>)GetValue(orderTrackingProperty); }
            set { SetValue(orderTrackingProperty, value); }
        }

        public static readonly DependencyProperty orderTrackingProperty =
               DependencyProperty.Register("ProductList", typeof(ObservableCollection<BO.OrderTracking?>), typeof(OrderTracking));

        public static readonly DependencyProperty sDp = DependencyProperty.Register(nameof(s), typeof(string), typeof(OrderTracking));
        public string? s { get => (string)GetValue(sDp); set => SetValue(sDp, value); }

        public static readonly DependencyProperty OtDp = DependencyProperty.Register(nameof(Ot), typeof(BO.OrderTracking), typeof(OrderTracking));
        public BO.OrderTracking? Ot { get => (BO.OrderTracking)GetValue(OtDp); set => SetValue(OtDp, value); }
        public OrderTracking()
        {
            InitializeComponent();
        }

        private void enter_Click(object sender, RoutedEventArgs e)
        {
            BO.OrderTracking o = bl?.Order.Tracking(int.Parse(s!))!;
            Ot = o;
        }

        private void Order_click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            Order? O = bl?.Order.ItemOrder(Ot.ID);
            OrderItemWindow? orderWindow = new OrderItemWindow(O.ID);
            orderWindow.ShowDialog();
        }
    }
}
