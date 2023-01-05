namespace PL;

using BO;
using System;
using System.Collections.Generic;
using System.Windows;

/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    BlApi.IBl? bl = BlApi.Factory.Get();

    /// <summary>
    /// Constructive action to add products
    /// </summary>
    static readonly DependencyProperty categoriesdp = DependencyProperty.Register("enum category", typeof(List<string>), typeof(ProductWindow));
   // List<string> Categories { get=>
    static readonly DependencyProperty ProductDep = DependencyProperty.Register(nameof(Product), typeof(Product), typeof(ProductWindow));
    Product Product {  get=> (Product)GetValue(ProductDep); set => SetValue(ProductDep, value); }

    static readonly DependencyProperty ModeDep = DependencyProperty.Register(nameof(Mode), typeof(bool), typeof(ProductWindow));
    bool Mode { get => (bool)GetValue(ModeDep); set => SetValue(ModeDep, value); }

    static readonly DependencyProperty CategoryEmunDep= DependencyProperty.Register(nameof(BO.Category), typeof(BO.Category), typeof(ProductWindow));

    BO.Category category { get => (BO.Category)GetValue(CategoryEmunDep); set => SetValue(CategoryEmunDep, value); }
    public ProductWindow()
    {
        InitializeComponent();
        Product = new Product();  
        Mode= false;
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
        Mode=true;
        CategoryForNewProduct.ItemsSource = Enum.GetValues(typeof(BO.Category));// for the comboBox
        Product = bl!.Product.ItemProduct(id);//getting the details from bl about the 
        BtnAddOrUpdetProduct.Content = "Updet";
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
                bl?.Product.Add(Product);
                MessageBox.Show("Product Add succefully", "succefully", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                bl!.Product.Update(Product);
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
