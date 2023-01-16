namespace PL;

using BO;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    public delegate void AddingOrUpdate(int proId);
    AddingOrUpdate? add;
    BlApi.IBl? bl = BlApi.Factory.Get();

    /// <summary>
    /// Constructive action to add products
    /// </summary>

  
    static readonly DependencyProperty ProductDep = DependencyProperty.Register(nameof(Product), typeof(Product), typeof(ProductWindow));
    Product Product {  get=> (Product)GetValue(ProductDep); set => SetValue(ProductDep, value); }

    static readonly DependencyProperty ModeDep = DependencyProperty.Register(nameof(Mode), typeof(bool), typeof(ProductWindow));
    bool Mode { get => (bool)GetValue(ModeDep); set => SetValue(ModeDep, value); }


  
    string BtnAddOrUpdetProductContent { get => (string)GetValue(BtnAddOrUpdetProductContentDp); set => SetValue(BtnAddOrUpdetProductContentDp, value); }

    static readonly DependencyProperty BtnAddOrUpdetProductContentDp = DependencyProperty.Register(nameof(BtnAddOrUpdetProductContent), typeof(string), typeof(ProductWindow));

    public ProductWindow(AddingOrUpdate ad)
    {
        InitializeComponent();
        Product = new Product();  
        Mode= false;
        CategoryForNewProduct.ItemsSource = Enum.GetValues(typeof(BO.Category));

        BtnAddOrUpdetProductContent = "Add";
       add = ad;
        
    }
    /// <summary>
    /// Constructive action to update products
    /// </summary>
    /// <param name="id"></param>
    public ProductWindow(int id)
    {
        InitializeComponent();
        Mode=true;
            
        BtnAddOrUpdetProductContent = "Updete";
        CategoryForNewProduct.ItemsSource = Enum.GetValues(typeof(BO.Category));// for the comboBox
        Product = bl!.Product.ItemProduct(id);//getting the details from bl about the  

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
          
            if (BtnAddOrUpdetProductContent == "Add")
            {
                bl!.Product.Add(Product);
                add(Product.ID);
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

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if (BtnAddOrUpdetProductContent == "Add")


    }
}
