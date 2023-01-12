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
            e.Handled = true;
            BO.ProductForList? p = (BO.ProductForList)((DataGrid)sender).SelectedItem;

            ProductWindow windoProduct = new ProductWindow(p.ID);
            windoProduct.ShowDialog();
            BO.ProductForList element = ProductList.First(x => x?.ID == p.ID)!;
            int index = ProductList.IndexOf(element);
            ProductList.RemoveAt(index);
            ProductList.Add(bl?.Product.GetProducts(x => x?.ID == p.ID).First());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Cart cartWindow = new Cart(Cb);
            cartWindow.ShowDialog();
        }
    }
}
