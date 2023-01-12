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

namespace PL
{
    /// <summary>
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        static readonly DependencyProperty OrderctDep = DependencyProperty.Register(nameof(Order), typeof(BO.Order), typeof(OrderItemWindow));
        BO.Order Order { get => (BO.Order)GetValue(OrderctDep); set => SetValue(OrderctDep, value); }
        public OrderWindow(int id)
        {
            Order = bl?.Order.ItemOrder(id)!;
            InitializeComponent();
        }
    }
}
