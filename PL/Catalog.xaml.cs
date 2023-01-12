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
    /// Interaction logic for ProductItem.xaml
    /// </summary>
    public partial class Catalog : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public BO.Cart Cb;
        public ObservableCollection<BO.ProductForList?> ProductList
        {
            get { return (ObservableCollection<BO.ProductForList?>)GetValue(ProductListProperty); }
            set { SetValue(ProductListProperty, value); }
        }

        public static readonly DependencyProperty ProductListProperty =
               DependencyProperty.Register("ProductList", typeof(ObservableCollection<BO.ProductForList?>), typeof(Window));
        public Catalog(BO.Cart cb)
        {
            Cb = cb;
            InitializeComponent();

            ProductList = new(bl.Product.GetProducts());

            SelectedCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void CategorySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
            BO.Category c = (BO.Category)SelectedCategory.SelectedItem;
            try
            {

                if (c == BO.Category.None)
                {
                    ProductList = new(bl?.Product.GetProducts()!);
                }
                else
                    ProductList = new(bl?.Product.GetListedProductByCategory(c)!);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void productItemDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled=true;
            BO.ProductForList? pfl = (BO.ProductForList)((DataGrid)sender).SelectedItem;
            int pflId = pfl.ID;
            BO.Product p = bl.Product.ItemProduct(pflId);
            BO.ProductItem pb = new BO.ProductItem { ID=p.ID, Category=p.Category, isStock=p.InStock>0 ? true : false, Name=p.Name, Price=p.Price };
            ProductItem windoProductItem = new ProductItem(pb,Cb);
            windoProductItem.ShowDialog();
            


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Cart cartWindow = new Cart(Cb);
            cartWindow.ShowDialog();
        }

     
    }
}
