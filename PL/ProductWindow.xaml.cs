using BlImplementation;
using System;
using System.Windows;
using BO;
using BlApi;
using System.Windows.Controls;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private IBl bl = new BL()
        
;       public ProductWindow()
        {
            InitializeComponent();
            CategoryForNewProduct.ItemsSource = Enum.GetValues(typeof(BO.Category));
            BtnAddOrUpdetProduct.Content = "Add";
        }
        public ProductWindow(int id)
        {
            InitializeComponent();
            CategoryForNewProduct.ItemsSource = Enum.GetValues(typeof(BO.Category));// for the comboBox
            Product product = bl.Product.ItemProduct(id);//getting the details from bl about the 
            BtnAddOrUpdetProduct.Content = "Updet";
        }

        private void ButtonAddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Id.Text.Length == 0 || Price.Text.Length == 0 || InStock.Text.Length == 0 || Name.Text.Length == 0 || CategoryForNewProduct.Text.Length == 0 || CategoryForNewProduct.SelectedIndex == 4 || CategoryForNewProduct.SelectedItem == null)
                {
                    MessageBox.Show("Fill in the missing fields", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (BtnAddOrUpdetProduct.Content == "Add")
                {
                    BO.Product product = new BO.Product()
                    {
                        ID = int.Parse(Id.Text),
                        Name = Name.Text,
                        InStock = int.Parse(InStock.Text),
                        Price = double.Parse(Price.Text),
                        Category = (BO.Category)CategoryForNewProduct.SelectedItem,
                    };
                    bl.Product.Add(product);
                    MessageBox.Show("Product Add succefully", "succefully", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    bl!.Product.Update(new Product()
                    {
                        ID = int.Parse(Id.Text),
                        Name = Name.Text,
                        InStock = int.Parse(InStock.Text),
                        Price = int.Parse(Price.Text),
                        Category = (BO.Category)CategoryForNewProduct.SelectedItem,
                    });
                    MessageBox.Show("Product Updet succefully", "succefully", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
