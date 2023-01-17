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
        public BO.Cart AddToCart(BO.Cart c, BO.ProductItem? pro)
        {
            BO.ProductItem? keep = bl.Product.GetProductItem(Cb, x => x.ID == pro.ID).First();


            if (keep.Amount == 0)
            {
                try
                {
                    bl.Cart.Add(c, pro.ID);
                    try
                    {
                        bl.Cart.Update(c, pro.ID, pro.Amount);

                       



                    }

                    catch (BO.BlMissingEntityException add1)
                    {
                        bl.Cart.Update(c, pro.ID, keep.Amount);

                        pro.Amount = bl.Product.ItemProduct(pro.ID, c).Amount;

                        MessageBox.Show(add1.Message, "cant add", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                }
                catch (BO.BlMissingEntityException add)
                {
                    MessageBox.Show(add.Message, "cant add", MessageBoxButton.OK, MessageBoxImage.Information);
                    pro.Amount = bl.Product.ItemProduct(pro.ID, c).Amount;
                }

            }
            else
            {
                try
                {
                    c = bl.Cart.Update(c, pro.ID, pro.Amount);
                    
                }



                catch (BO.BlMissingEntityException update)
                {
                    MessageBox.Show(update.Message, "cant add", MessageBoxButton.OK, MessageBoxImage.Information);
                }


            }



            pro.Amount = bl.Product.ItemProduct(pro.ID, c).Amount;
            
            Cb = c;
            return c;
        }
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

        private void orderItemDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.ProductItem pro=bl.Product.ItemProduct(((BO.ProductItem)sender).ID,Cb);
            ProductItem p = new ProductItem(pro, Cb, AddToCart);
            p.ShowDialog();
        }
    }
}
