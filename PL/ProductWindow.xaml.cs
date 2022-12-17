namespace PL;
using BlImplementation;
using System;
using System.Windows;
using BlApi;

/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    private IBl bl = new BL()

    /// <summary>
    /// Constructive action to add products
    /// </summary>
; public ProductWindow()
    {
        InitializeComponent();
        CategoryForNewProduct.ItemsSource = Enum.GetValues(typeof(BO.Category));
        BtnAddOrUpdetProduct.Content = "Add";
    }
    /// <summary>
    /// Constructive action to update products
    /// </summary>
    /// <param name="id"></param>
    public ProductWindow(int id)
    {
        InitializeComponent();
        CategoryForNewProduct.ItemsSource = Enum.GetValues(typeof(BO.Category));// for the comboBox
        BO.Product product = bl.Product.ItemProduct(id);//getting the details from bl about the 
        BtnAddOrUpdetProduct.Content = "Updet";
        Id.Text = product.ID.ToString();
        Name.Text = product.Name;
        CategoryForNewProduct.Text = product.Category.ToString();
        InStock.Text = product.InStock.ToString();
        Price.Text = product.Price.ToString();
    }
    /// <summary>
    /// A button that adds or updates products
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ButtonAddOrApdet_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            string s = BtnAddOrUpdetProduct.Content.ToString()!;
            if (s == "Add")
            {
                BO.Product product = new BO.Product()
                {
                    ID = int.Parse(Id.Text),
                    Name = Name.Text,
                    InStock = int.Parse(InStock.Text),
                    Price = double.Parse(Price.Text),
                    Category = (BO.Category)CategoryForNewProduct.SelectedItem!,
                };
                bl.Product.Add(product);
                MessageBox.Show("Product Add succefully", "succefully", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                bl!.Product.Update(new BO.Product()
                {
                    ID = int.Parse(Id.Text),
                    Name = Name.Text,
                    InStock = int.Parse(InStock.Text),
                    Price = int.Parse(Price.Text),
                    Category = (BO.Category)CategoryForNewProduct.SelectedItem!,
                });
                MessageBox.Show("Product Updet succefully", "succefully", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}
