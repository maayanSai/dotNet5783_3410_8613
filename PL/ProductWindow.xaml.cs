using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
namespace PL;

/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    public delegate void update(string productImagepPth,int id);
    readonly update? upd;
    public delegate void AddingOrUpdate(int proId);
    readonly AddingOrUpdate? add;
    readonly BlApi.IBl? bl = BlApi.Factory.Get();

    /// <summary>
    /// Constructive action to add products
    /// </summary>

    static readonly DependencyProperty ProductDep = DependencyProperty.Register(nameof(Product), typeof(BO.Product), typeof(ProductWindow));
    BO.Product Product {  get=> (BO.Product)GetValue(ProductDep); set => SetValue(ProductDep, value); }

    static readonly DependencyProperty ModeDep = DependencyProperty.Register(nameof(Mode), typeof(bool), typeof(ProductWindow));
    bool Mode { get => (bool)GetValue(ModeDep); set => SetValue(ModeDep, value); }
    string BtnAddOrUpdetProductContent { get => (string)GetValue(BtnAddOrUpdetProductContentDp); set => SetValue(BtnAddOrUpdetProductContentDp, value); }
    static readonly DependencyProperty BtnAddOrUpdetProductContentDp = DependencyProperty.Register(nameof(BtnAddOrUpdetProductContent), typeof(string), typeof(ProductWindow));
    public ProductWindow(AddingOrUpdate ad)
    {
        InitializeComponent();
        Product = new BO.Product();  
        Mode= false;
        CategoryForNewProduct.ItemsSource = Enum.GetValues(typeof(BO.Category));
        BtnAddOrUpdetProductContent = "Add";
        add = ad;
        
    }
    /// <summary>
    /// Constructive action to update products
    /// </summary>
    /// <param name="id"></param>
    public ProductWindow(int id, update upd1)
    {
        InitializeComponent();
        Mode=true;      
        BtnAddOrUpdetProductContent = "Updete";
        CategoryForNewProduct.ItemsSource = Enum.GetValues(typeof(BO.Category));// for the comboBox
        try
        {
            Product = bl!.Product.ItemProduct(id); //getting the details from bl about the  
        }
        catch (BO.BlInCorrectException exp)
        {
            MessageBox.Show(exp.Message, "can not found the product", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        upd=upd1;   
    }
    /// <summary>
    /// A button that adds or updates products
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ButtonAddOrApdet_Click(object sender, RoutedEventArgs e)
    {
        //if (Product.ImageRelativeName != null)
        //{
            string imageName = Product.ImageRelativeName.Substring(Product.ImageRelativeName.LastIndexOf("\\"));
            if (!File.Exists(Environment.CurrentDirectory[..^4] + @"\pictures\" + imageName))
            {
                File.Copy(Product.ImageRelativeName, Environment.CurrentDirectory[..^4] + @"\pictures\" + imageName);
                var v = Product;
                Product = v;
            }
            Product.ImageRelativeName = imageName;
            try
            {
                if (BtnAddOrUpdetProductContent == "Update")
                {
                    bl!.Product.Update(Product);
                    upd!(Product.ImageRelativeName, Product.ID);
                }
            }
            catch (BO.BlInCorrectException exp)
            {
                MessageBox.Show(exp.Message, "can not update the product", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        //}
        try
        {
            if (BtnAddOrUpdetProductContent == "Add")
            {
                bl!.Product.Add(Product);
                add!(Product.ID);
                MessageBox.Show("Product Add succefully", "succefully", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                bl!.Product.Update(Product);
                MessageBox.Show("Product Updet succefully", "succefully", MessageBoxButton.OK, MessageBoxImage.Information);
                upd!(Product.ImageRelativeName!, Product.ID);
                this.Close();
            }
        }
        catch (BO.BlInCorrectException exp)
        {
            MessageBox.Show(exp.Message, "can not update the product", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
    private void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        OpenFileDialog openFileDialog = new();
        if (openFileDialog.ShowDialog() == true)
        {
            image.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            Product.ImageRelativeName = openFileDialog.FileName;
        }
    }
}
