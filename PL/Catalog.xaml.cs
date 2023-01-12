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
        public ObservableCollection<BO.ProductItem?> ProductItem
        {
            get { return (ObservableCollection<BO.ProductItem?>)GetValue(ProductListProperty); }
            set { SetValue(ProductListProperty, value); }
        }

        public static readonly DependencyProperty ProductListProperty =
               DependencyProperty.Register("ProductItem", typeof(ObservableCollection<BO.ProductItem?>), typeof(Window));
        public Catalog()
        {
            InitializeComponent();
            ProductItem = bl?.Cart.Item;

            SelectedCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Category c = (BO.Category)SelectedCategory.SelectedItem;
            try
            {
                if (c == BO.Category.None)
                {
                    ProductItem = new(bl?.Product.GetProducts()!);
                }
                else
                    ProductItem = new(bl?.Product.GetListedProductByCategory(c)!);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
