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
        readonly BlApi.IBl? bl = BlApi.Factory.Get();
        readonly BO.Cart CB;
        public delegate BO.Cart? AddingToCrt(BO.Cart c, BO.ProductItem pro);
        readonly AddingToCrt? addtocart;

        static readonly DependencyProperty PBkkeperDep = DependencyProperty.Register(nameof(PBKeeper), typeof(BO.ProductItem), typeof(ProductItem));
        BO.ProductItem PBKeeper { get => (BO.ProductItem)GetValue(PBkkeperDep); set => SetValue(PBkkeperDep, value); }

        static readonly DependencyProperty PBForWindowDep = DependencyProperty.Register(nameof(PBForWindow), typeof(BO.ProductItem), typeof(ProductItem));
        BO.ProductItem PBForWindow { get => (BO.ProductItem)GetValue(PBForWindowDep); set => SetValue(PBForWindowDep, value); }


        public ProductItem(BO.ProductItem pb, BO.Cart c, AddingToCrt add)
        {
            InitializeComponent();
            CB = c;
            PBKeeper = pb;
            PBForWindow = new BO.ProductItem { ID = pb.ID, Amount=pb.Amount, Category=pb.Category, isStock=pb.isStock, ImageRelativeName=pb.ImageRelativeName, Name=pb.Name, Price=pb.Price};
            addtocart = add;
        }

        private void Add_Button(object sender, RoutedEventArgs e)
        {
            PBForWindow.Amount++;
            e.Handled = true;
            addtocart!(CB, PBForWindow);
            PBForWindow = bl!.Product.ItemProduct(PBForWindow.ID, CB);
            PBKeeper.Amount = PBForWindow.Amount;
            PBKeeper.isStock = PBForWindow.isStock;
        }

        private void Cart_Button(object sender, RoutedEventArgs e)
        {
            Cart cartwindow = new Cart(CB);
            
            cartwindow.ShowDialog();
            
            
                  BO.ProductItem p= bl?.Product.ItemProduct(PBKeeper.ID, CB)!;
            PBKeeper.Amount = p.Amount;
            PBKeeper.isStock = p.isStock;
            PBForWindow = p;
            
          
        }

        private void Delete_Button(object sender, RoutedEventArgs e)
        {
            PBForWindow.Amount--;
            e.Handled = true;
            addtocart!(CB, PBForWindow);
            PBForWindow = bl!.Product.ItemProduct(PBForWindow.ID, CB);
            PBKeeper.Amount = PBForWindow.Amount;
            PBKeeper.isStock = PBForWindow.isStock;

        }
    }
}
