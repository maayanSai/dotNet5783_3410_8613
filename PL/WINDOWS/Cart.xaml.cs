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

namespace PL
{
    /// <summary>
    /// Interaction logic for Cart.xaml
    /// </summary>
    public partial class Cart : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public BO.Cart Cb
        {
            get { return (BO.Cart)GetValue(cbdp); }
            set { SetValue(cbdp, value); }
        }

        public static readonly DependencyProperty cbdp =
               DependencyProperty.Register("Cb", typeof(BO.Cart), typeof(Window));

        public string category
        {
            get { return (string)GetValue(categorydp); }
            set { SetValue(categorydp, value); }
        }

        public static readonly DependencyProperty categorydp =
               DependencyProperty.Register("category", typeof(string), typeof(Window));
        public ObservableCollection<BO.OrderItem?> OrderItemtList
        {
            get { return (ObservableCollection<BO.OrderItem?>)GetValue(OrderItemtListProperty); }
            set { SetValue(OrderItemtListProperty, value); }
        }

        public static readonly DependencyProperty OrderItemtListProperty =
               DependencyProperty.Register("OrderItemtList", typeof(ObservableCollection<BO.OrderItem?>), typeof(Window));
        public Cart(BO.Cart c)
        {

            Cb=c;
            OrderItemtList= new(c.Items);
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int orderid = bl.Cart.MakeAnOrder(Cb);
                MessageBox.Show("the order orderd sexxsesfully", "the order orderd sexxsesfully", MessageBoxButton.OK, MessageBoxImage.Information);
                OrderItemWindow? orderWindow = new OrderItemWindow(orderid);
                orderWindow.ShowDialog();
            }
            catch (BO.BlMissingEntityException c) 
            {
                MessageBox.Show(c.Message, "acnt make an order because of:"+c.Message, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (BO.BlAlreadyExistEntityException c)
            {
                MessageBox.Show(c.Message, "acnt make an order because of:"+c.Message, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (BO.BlNullPropertyException c)
            {
                MessageBox.Show(c.Message, "acnt make an order because of:"+c.Message, MessageBoxButton.OK, MessageBoxImage.Information);
            }
         
            catch (BO.BlWorngCategoryException c)
            {
                MessageBox.Show(c.Message, "acnt make an order because of:"+c.Message, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (BO.BlInCorrectException c)
            {
                MessageBox.Show(c.Message, "acnt make an order because of:"+c.Message, MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
    }
}
