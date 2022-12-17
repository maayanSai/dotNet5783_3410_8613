namespace PL;
using BlApi;
using BlImplementation;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

/// <summary>
/// Interaction logic for ProductListWindow.xaml
/// </summary>
public partial class ProductListWindow : Window
{
    private IBl bl = new BL();
    /// <summary>
    /// constructive action
    /// </summary>
    public ProductListWindow()
    {
        InitializeComponent();
        ProductListview.ItemsSource = bl.Product.GetProducts();
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
            ProductListview.ItemsSource = bl.Product.GetProducts(x => x!.Category == c);
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
        ProductListview.ItemsSource = bl?.Product.GetProducts();
    }
    /// <summary>
    /// A button that allows updating products
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Update(object sender, MouseButtonEventArgs e)
    {
        int id = ((BO.ProductForList)ProductListview.SelectedItem).ID;
        if (ProductListview.SelectedItem is BO.ProductForList productForList)
            new ProductWindow(id).ShowDialog();
        ProductListview.ItemsSource = bl?.Product.GetProducts();
    }

    private void ProductListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    /// <summary>
    /// A button that returns all products after filtering
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ButtonAfterFiltering_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            ProductListview.ItemsSource = bl.Product.GetProducts();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}
