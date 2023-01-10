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

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderItemWindow.xaml
    /// </summary>
    public partial class OrderItemWindow : Window
    {
        public delegate void UpdateOrder(int proId);
        BlApi.IBl? bl = BlApi.Factory.Get();
        static readonly DependencyProperty OrderctDep = DependencyProperty.Register(nameof(Order), typeof(BO.Order), typeof(OrderItemWindow));
       BO.Order Order { get => (BO.Order)GetValue(OrderctDep); set => SetValue(OrderctDep, value); }
        static readonly DependencyProperty IsBossDep = DependencyProperty.Register(nameof(IsBoss), typeof(bool), typeof(OrderItemWindow));
        bool IsBoss { get => (bool)GetValue(IsBossDep); set => SetValue(IsBossDep, value); }
        public OrderItemWindow(int id, UpdateOrder updateOrder)
        {
            Order=bl?.Order.ItemOrder(id)!;
            InitializeComponent();
            IsBoss=true;

           // status.ItemsSource = Enum.GetValues(typeof(BO.OrderStatus));
        }
        public OrderItemWindow(int id)
        {
            Order=bl?.Order.ItemOrder(id)!;
            InitializeComponent();
            IsBoss=false;

            // status.ItemsSource = Enum.GetValues(typeof(BO.OrderStatus));
        }


    }
}
