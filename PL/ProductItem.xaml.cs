using System;
using System.Collections.Generic;
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
    public partial class ProductItem : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Cart CB;
        public delegate BO.Cart? AddingToCrt(BO.Cart c, BO.ProductItem pro);
        AddingToCrt? addtocart;
        static readonly DependencyProperty PBDep = DependencyProperty.Register(nameof(PB), typeof(BO.ProductItem), typeof(ProductItem));
        BO.ProductItem PB {  get=> (BO.ProductItem)GetValue(PBDep); set => SetValue(PBDep, value); }
        
        public ProductItem(BO.ProductItem pb,BO.Cart c, AddingToCrt add)
        {
            InitializeComponent();
            CB = c;
            PB=pb;
            addtocart=add;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //BO.OrderItem orderItem= new BO.OrderItem { Amount= 1,Name=PB.Name,Price=PB.Price,ProductID=PB.ID,Totalprice=PB.Price};
            e.Handled = true;

            CB=addtocart(CB, PB);
            PB=bl.Product.ItemProduct(PB.ID, CB);


        }
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    e.Handled = true;

        //    BO.OrderItem? p=CB.Items.FirstOrDefault(x => x.ProductID==PB.ID);
        //    if (p!=null)
        //    {
        //        try
        //        {
        //            bl.Cart.Update(CB, PB.ID,PB.Amount);
        //            PB=bl.Product.ItemProduct(PB.ID, CB);
        //        }
        //        catch (BO.BlMissingEntityException boexp)
        //        {
        //            MessageBox.Show(boexp.Message, "cant change for that amaunt", MessageBoxButton.OK, MessageBoxImage.Information);
        //        }


        //    }
        //    else
        //    {


        //        try
        //        {
        //            bl.Cart.Add(CB, PB.ID);
        //            try
        //            {

        //                bl.Cart.Update(CB, PB.ID, PB.Amount);
        //                PB=bl.Product.ItemProduct(PB.ID, CB);
        //            }

        //            catch (BO.BlMissingEntityException boexp)
        //            {
        //                MessageBox.Show(boexp.Message, "cant change for that amaunt", MessageBoxButton.OK, MessageBoxImage.Information);
        //                PB.Amount=0;
        //                try
        //                {
        //                    bl.Cart.Update(CB, PB.ID, PB.Amount);
        //                    PB=bl.Product.ItemProduct(PB.ID, CB);
        //                }
        //                catch (BO.BlMissingEntityException bo)
        //                {
        //                    PB.Amount=0;
        //                    MessageBox.Show(bo.Message, "cant change for that amaunt", MessageBoxButton.OK, MessageBoxImage.Information);

        //                }
        //            }
        //        }
        //        catch (BO.BlMissingEntityException boexp)
        //        {
        //            MessageBox.Show(boexp.Message, "cant change for that amaunt", MessageBoxButton.OK, MessageBoxImage.Information);
        //            PB.Amount=0;
        //            BO.ProductItem p1 = PB;

        //            PB=p1;
        //        }



        //    }

        //}

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Cart cartwindow = new Cart(CB);
            cartwindow.ShowDialog();

        }
    }
}
