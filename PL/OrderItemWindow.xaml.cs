using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Globalization;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderItemWindow.xaml
    /// </summary>
    public partial class OrderItemWindow : Window
    {
        public delegate void UpdateOrder(int proId);

        UpdateOrder? update;
        BlApi.IBl? bl = BlApi.Factory.Get();
        static readonly DependencyProperty OrderctDep = DependencyProperty.Register(nameof(Order), typeof(BO.Order), typeof(OrderItemWindow));
       BO.Order Order { get => (BO.Order)GetValue(OrderctDep); set => SetValue(OrderctDep, value); }
        static readonly DependencyProperty IsBossDep = DependencyProperty.Register(nameof(IsBoss), typeof(bool), typeof(OrderItemWindow));
        bool IsBoss { get => (bool)GetValue(IsBossDep); set => SetValue(IsBossDep, value); }
        public OrderItemWindow(int id, UpdateOrder updateOrder)
        {
            InitializeComponent();
            update=updateOrder;
            Order =bl?.Order.ItemOrder(id)!;
            var a = Order.Items;
           
            
            IsBoss=true;


        }
        public OrderItemWindow(int id)
        {
            Order=bl?.Order.ItemOrder(id)!;
            InitializeComponent();
            IsBoss=false;

        }

     

        //private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    e.Handled=true;
        //    try
        //    {
        //        bl.Order.Updateshipping(Order.ID);
        //        update(Order.ID);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void UpdateShip(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            try
            {
                Order.ShipDate = bl?.Order.Updateshipping(Order.ID).ShipDate;
                update(Order.ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateDelivery(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            try
            {
                Order.DeliveryrDate = bl?.Order.Updatesupply(Order.ID).DeliveryrDate;
                update(Order.ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
