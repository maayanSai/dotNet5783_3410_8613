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
    /// Interaction logic for ProductItem.xaml
    /// </summary>
    public partial class ProductItem : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Cart CB;
            static readonly DependencyProperty PBDep = DependencyProperty.Register(nameof(PB), typeof(BO.ProductItem), typeof(ProductItem));
        BO.ProductItem PB {  get=> (BO.ProductItem)GetValue(PBDep); set => SetValue(PBDep, value); }
        
        public ProductItem(BO.ProductItem pb,BO.Cart c)
        {
            InitializeComponent();
            CB = c;
            PB=pb;
        
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BO.OrderItem orderItem= new BO.OrderItem { Amount= 1,Name=PB.Name,Price=PB.Price,ProductID=PB.ID,Totalprice=PB.Price};
            CB.Items.Add(orderItem);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Cart cartwindow = new Cart(CB);
            cartwindow.ShowDialog();

        }
    }
}
