namespace PL;

using BlApi;
using BlImplementation;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BO;
using System.Data;

/// <summary>
/// Interaction logic for ProductListWindow.xaml
/// </summary>
public partial class ProductListWindow : Window
{
    private IBl bl = new BL();
    public ProductListWindow()
    {
        InitializeComponent();
        ProductListview.ItemsSource = bl.Product.GetProducts();
        AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
    }

    private void SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Category c = (Category)AttributeSelector.SelectedItem;
        try
        {
            ProductListview.ItemsSource = bl.Product.GetProducts(x => x!.Category == c);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    private void Button_Click(object sender, RoutedEventArgs e)
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
    private void NewProductButton_Click(object sender, RoutedEventArgs e)
    {
        new ProductWindow().Show();
        ProductListview.ItemsSource = bl?.Product.GetProducts();
    }
    private void Update(object sender, MouseButtonEventArgs e)
    {
        int id = ((ProductForList)ProductListview.SelectedItem).ID;
        if (ProductListview.SelectedItem is ProductForList productForList)
            new ProductWindow(id).ShowDialog();
        ProductListview.ItemsSource = bl?.Product.GetProducts();

    }
}
