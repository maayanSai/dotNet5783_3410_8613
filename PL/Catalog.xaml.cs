using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductItem.xaml
    /// </summary>
    public partial class Catalog : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public BO.Cart Cb;
        public BO.Cart AddToCart(BO.Cart c, BO.ProductItem? pro)
        {
            
            BO.OrderItem b = new BO.OrderItem { Name=pro.Name, ProductID=pro.ID, Amount=1, Price=pro.Price, Totalprice=pro.Price };
            try
            {
                bl.Cart.Add(c, pro.ID);
                
                pro.Amount=  c.Items.Find(x => x.ProductID==b.ProductID).Amount;
            
                 ProducitemtList.Remove(ProducitemtList.FirstOrDefault(x=>x.ID==pro.ID));
                ProducitemtList.Add(pro);
           
            }
           catch (BO.BlMissingEntityException boexp)
            {
                MessageBox.Show(boexp.Message, "cant add", MessageBoxButton.OK, MessageBoxImage.Information);
            }
           
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
            Cb=cb;
            InitializeComponent();

            ProducitemtList= new(bl.Product.GetProductItem(Cb)); 
        
            SelectedCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void CategorySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled=true;
            BO.Category c = (BO.Category)SelectedCategory.SelectedItem;
            try
            {

                if (c == BO.Category.None)
                {
                    ProducitemtList = new(bl?.Product.GetProductItem(Cb)!) ;
                }
                else
                    ProducitemtList = new(bl?.Product.GetProductItem(Cb, x => x.Category==c)!);
            }
                        
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void productItemDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled=true;
            BO.ProductItem? pil = (BO.ProductItem?)((DataGrid)sender).SelectedItem;
            ProductItem windoProductItem = new ProductItem(pil,Cb, AddToCart);
            windoProductItem.ShowDialog();
            

        }

        private void ChangeAmaunt(object sender, RoutedEventArgs e)
        {
            e.Handled=true;
            BO.ProductItem? pil = (BO.ProductItem?)((DataGrid)sender).SelectedItem;
           BO.OrderItem b= Cb.Items.FirstOrDefault(x => x.ProductID==pil.ID);
            if (b!=null)
            {
                try
                {
                    bl.Cart.Update(Cb, pil.ID, pil.Amount);
                }
                catch (BO.BlMissingEntityException boexp)
                {
                    MessageBox.Show(boexp.Message, "cant change for that amaunt", MessageBoxButton.OK, MessageBoxImage.Information);
                }
             

            }
            else
            {
                ProductItem windoProductItem = new ProductItem(pil, Cb, AddToCart);
                windoProductItem.ShowDialog();

            }


            Cart cartWindow = new Cart(Cb);
            cartWindow.ShowDialog();
        }

     
    }
}

