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
    /// Interaction logic for Cart.xaml
    /// </summary>
    public partial class Cart : Window
    {
        private static readonly DalApi.IDal dal = DalApi.Factory.Get()!;
       
             public ObservableCollection<BO.OrderItem?> OrderItemtList
        {
            get { return (ObservableCollection<BO.OrderItem?>)GetValue(OrderItemtListProperty); }
            set { SetValue(OrderItemtListProperty, value); }
        }

        public static readonly DependencyProperty OrderItemtListProperty =
               DependencyProperty.Register("OrderItemtList", typeof(ObservableCollection<BO.OrderItem?>), typeof(Window));
        public Cart(BO.Cart c)
        {
    
                
            InitializeComponent();
        }
    }
}
