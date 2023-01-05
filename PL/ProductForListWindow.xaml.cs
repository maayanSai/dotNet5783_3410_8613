namespace PL;

using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

/// <summary>
/// Interaction logic for ProductListWindow.xaml
/// </summary>
public partial class ProductListWindow : Window
{
    BlApi.IBl? bl = BlApi.Factory.Get();
    /// <summary>
    /// constructive action
    /// </summary>
    /// 
  //  DependencyProperty ProductDep = DependencyProperty.Register(nameof(ProductList), typeof(IEnumerable<ProductForList>), typeof(ProductListWindow));
   // IEnumerable<ProductForList> ProductList { get => (IEnumerable<ProductForList>)GetValue(ProductDep); set => SetValue(ProductDep, value); }


    public ObservableCollection<BO.ProductForList?> ProductList
    {
        get { return (ObservableCollection<BO.ProductForList?>)GetValue(ProductListProperty); }
        set { SetValue(ProductListProperty, value); }
    }
    public Category Category { get; set; } = Category.None;
    // Using a DependencyProperty as the backing store for ProductList.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ProductListProperty =
        DependencyProperty.Register("ProductList", typeof(ObservableCollection<BO.ProductForList?>), typeof(Window));


    public ProductListWindow()
    {
        InitializeComponent();
        ProductList = new(bl?.Product.GetProducts()!);
       // productForListDataGrid.ItemsSource = bl?.Product.GetProducts();
        AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
    }
    /// <summary>
    /// Filter button by category
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        BO.Category c = (BO.Category)AttributeSelector.SelectedItem;
        try
        {
            ProductList = new(bl?.Product.GetListedProductByCategory(c)!);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    /// <summary>
    /// A button that allows you to add products
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NewProductButton_Click(object sender, RoutedEventArgs e)
    {
        new ProductWindow().Show();
        ProductList = new(bl?.Product.GetProducts()!);
    }
    /// <summary>
    /// A button that allows updating products
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    

    /// <summary>
    /// A button that returns all products after filtering
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ButtonAfterFiltering_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            ProductList = new(bl?.Product.GetProducts()!);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void productForListDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        e.Handled= true;
        BO.ProductForList? p = (ProductForList)((DataGrid)sender).SelectedItem;

        ProductWindow windoProduct = new ProductWindow(p.ID);
        windoProduct.ShowDialog();

    }

    private void ProductListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
}
