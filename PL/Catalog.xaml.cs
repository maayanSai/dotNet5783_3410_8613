using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace PL;

/// <summary>
/// Interaction logic for ProductItem.xaml
/// </summary>
public partial class Catalog : Window
{
    readonly BlApi.IBl? bl = BlApi.Factory.Get();
    public BO.Cart Cb;
    public CollectionView CollectionViewProduct { set; get; }
    private readonly string group = "Category";
    PropertyGroupDescription propertyGroupDescription;
    public BO.Cart AddToCart(BO.Cart c, BO.ProductItem? pro)
    {
        int amount = 0;
        try
        {
            BO.ProductItem? keepProduct = bl?.Product.ItemProduct(pro!.ID, Cb);
            amount = keepProduct!.Amount;
        }
        catch(BO.BlMissingEntityException exp)
        {
            MessageBox.Show(exp.Message, "the product dont find", MessageBoxButton.OK, MessageBoxImage.Information);

        }
        if (amount == 0)
        {
            try
            {
                bl?.Cart.Add(c, pro!.ID);
                try
                {
                    bl?.Cart.Update(c, pro!.ID, pro.Amount);
                    int index = ProducitemtList.IndexOf(ProducitemtList.FirstOrDefault(x => x?.ID == pro!.ID));
                    ProducitemtList.RemoveAt(index);
                    ProducitemtList.Insert(index, pro);
                }
                catch (BO.BlMissingEntityException add1)
                {
                    bl?.Cart.Update(c, pro!.ID, amount);
                    pro!.Amount = bl!.Product.ItemProduct(pro.ID, c).Amount;
                    MessageBox.Show(add1.Message, "cant add", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch(BO.BlInCorrectException add1)
                {
                    bl?.Cart.Update(c, pro!.ID, amount);
                    pro!.Amount = bl!.Product.ItemProduct(pro.ID, c).Amount;
                    MessageBox.Show(add1.Message, "cant add", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (BO.BlMissingEntityException add)
            {
                MessageBox.Show(add.Message, "cant add", MessageBoxButton.OK, MessageBoxImage.Information);
                pro!.Amount = bl!.Product.ItemProduct(pro.ID, c).Amount;
            }
            catch (BO.BlInCorrectException add)
            {
                MessageBox.Show(add.Message, "cant add", MessageBoxButton.OK, MessageBoxImage.Information);
                pro!.Amount = bl!.Product.ItemProduct(pro.ID, c).Amount;
            }
        }
        else
        {
            try
            {
                c = bl?.Cart.Update(c, pro!.ID, pro.Amount)!;
                int index = ProducitemtList.IndexOf(ProducitemtList.FirstOrDefault(x => x?.ID == pro!.ID));
                ProducitemtList.RemoveAt(index);
                ProducitemtList.Insert(index, pro);
            }
            catch (BO.BlMissingEntityException update)
            {
                MessageBox.Show(update.Message, "cant add", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (BO.BlInCorrectException update)
            {
                MessageBox.Show(update.Message, "cant add", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        pro!.Amount = bl!.Product.ItemProduct(pro.ID, c).Amount;
        int index1 = ProducitemtList.IndexOf(ProducitemtList.FirstOrDefault(x => x?.ID == pro.ID));
        ProducitemtList.RemoveAt(index1);
        ProducitemtList.Insert(index1, pro);
        Cb = c;
        return c;
    }
    public ObservableCollection<BO.ProductItem?> ProducitemtList
    {
        get { return (ObservableCollection<BO.ProductItem?>)GetValue(ProductItemProperty); }
        set { SetValue(ProductItemProperty, value); }
    }

    public static readonly DependencyProperty ProductItemProperty =
           DependencyProperty.Register("ProducitemtList", typeof(ObservableCollection<BO.ProductItem?>), typeof(Window));
    public Catalog(BO.Cart cb)
    {
        Cb = cb;
        InitializeComponent();
        ProducitemtList = new ObservableCollection<BO.ProductItem?>(bl!.Product.GetProductItem(Cb));
        SelectedCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));

    }

    private void CategorySelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        e.Handled = true;
        BO.Category c = (BO.Category)SelectedCategory.SelectedItem;
        try
        {
            if (c == BO.Category.None)
            {
                ProducitemtList = new(bl?.Product.GetProductItem(Cb)!);
            }
            else
                ProducitemtList = new(bl?.Product.GetProductItem(Cb, x => x.Category == c)!);
        }

        catch (BO.BlMissingEntityException exp)
        {
            MessageBox.Show(exp.Message, "can not found entity", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
    private void Cart_Click(object sender, RoutedEventArgs e)=> new Cart(Cb).ShowDialog();

    private void Image_Click(object sender, MouseButtonEventArgs e)
    {
        e.Handled = true;
        var p = (BO.ProductItem)((Button)sender).Tag;
        ProductItem pro = new (p, Cb, AddToCart);
        pro.ShowDialog();
    }
    private void Gruoping_Click(object sender, RoutedEventArgs e)
    {
        CollectionViewProduct = (CollectionView)CollectionViewSource.GetDefaultView(ProducitemtList);
        propertyGroupDescription = new PropertyGroupDescription(group);
        CollectionViewProduct.GroupDescriptions.Add(propertyGroupDescription);
    }
}

