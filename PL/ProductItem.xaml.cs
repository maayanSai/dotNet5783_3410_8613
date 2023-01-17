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
        public delegate BO.Cart? AddingToCrt(BO.Cart c, BO.ProductItem pro);
        AddingToCrt? addtocart;

        static readonly DependencyProperty PBDep = DependencyProperty.Register(nameof(PB), typeof(BO.ProductItem), typeof(ProductItem));
        BO.ProductItem PB { get => (BO.ProductItem)GetValue(PBDep); set => SetValue(PBDep, value); }

        public ProductItem(BO.ProductItem pb, BO.Cart c, AddingToCrt add)
        {
            InitializeComponent();
            CB = c;
            PB = pb;
            addtocart = add;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            e.Handled = true;

            addtocart(CB, PB);

            PB = bl.Product.ItemProduct(PB.ID, CB);

            


        }
  

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Cart cartwindow = new Cart(CB);
            cartwindow.ShowDialog();

        }

        //~ProductItem(){ PB.Amount=bl.Product.ItemProduct(PB.ID, CB).Amount; }
            
    }
}
