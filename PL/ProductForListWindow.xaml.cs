namespace PL;

using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Xml;

/// <summary>
/// Interaction logic for ProductListWindow.xaml
/// </summary>
public partial class ProductListWindow : Window
{
    BlApi.IBl? bl = BlApi.Factory.Get();
    /// <summary>
    /// constructive action
    /// </summary>
    public ObservableCollection<BO.ProductForList?> ProductList
    {
        get { return (ObservableCollection<BO.ProductForList?>) GetValue(ProductListProperty);}
        set { SetValue(ProductListProperty, value); }
    }
    //public   Category Category { get; set; } = Category.None;
    //public static readonly DependencyProperty CategoryDp =
    //    DependencyProperty.Register("Category", typeof(Category), typeof(Window));

    // Using a DependencyProperty as the backing store for ProductList.  This enables animation, styling, binding, etc...

    public static readonly DependencyProperty ProductListProperty =
           DependencyProperty.Register("ProductList", typeof(ObservableCollection<BO.ProductForList?>), typeof(Window));
   

    public ProductListWindow()
    {
        InitializeComponent();
        ProductList = new(bl?.Product.GetProducts()!);
      
        AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
    }
    /// <summary>
    /// Filter button by category
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    
    public void AddToOb(int proId)
    {

        ProductList.Add(bl.Product.GetProducts(x => x.ID==proId).First());
    }
    private void SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        BO.Category c = (BO.Category)AttributeSelector.SelectedItem;
        try
        {
            if (c==Category.None)
            {
                ProductList=new(bl?.Product.GetProducts()!);
            }
            else
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
        
        new ProductWindow(AddToOb).ShowDialog();

        // BO.Product p = (BO.Product)((Dataender);
        //.Product p = (BO.Product)sender;
        // ProductList.Add(new BO.ProductForList { ID=p.ID, Category=p.Category, Price=p.Price, Name=p.Name });

        ProductList = new(bl?.Product.GetProducts()!);
        
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
            ProductList = new(bl?.Product.GetProducts()!);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

    }

    /// <summary>
    /// A button that allows updating products
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    private void productForListDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        e.Handled= true;
        BO.ProductForList? p = (ProductForList)((DataGrid)sender).SelectedItem;
        
        ProductWindow windoProduct = new ProductWindow(p.ID);
        windoProduct.ShowDialog();
        BO.ProductForList element = ProductList.First(x => x?.ID == p.ID)!;
        int index = ProductList.IndexOf(element);
        ProductList.RemoveAt(index);
        ProductList.Add(bl?.Product.GetProducts(x => x?.ID ==p.ID).First());
            //[index] = (bl?.Product.GetProducts(x=> x?.ID ==p.ID)).First();
            // RaisePropertyChanged(nameof( ProductList));
            //CollectionViewSource.GetDefaultView(ProductList).Refresh();

        // ProductListWindow.s
    }

    
}
