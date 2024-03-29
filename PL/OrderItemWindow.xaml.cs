﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Globalization;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderItemWindow.xaml
    /// </summary>
    public partial class OrderItemWindow : Window
    {
        public delegate void UpdateOrder(int proId);
        readonly UpdateOrder? update;
        BlApi.IBl? bl = BlApi.Factory.Get();
        static readonly DependencyProperty OrderctDep = DependencyProperty.Register(nameof(Order), typeof(BO.Order), typeof(OrderItemWindow));
        BO.Order Order { get => (BO.Order)GetValue(OrderctDep); set => SetValue(OrderctDep, value); }
        static readonly DependencyProperty IsBossDep = DependencyProperty.Register(nameof(IsBoss), typeof(Visibility), typeof(OrderItemWindow));
        Visibility IsBoss { get => (Visibility)GetValue(IsBossDep); set => SetValue(IsBossDep, value); }
        public OrderItemWindow(int id, UpdateOrder updateOrder)
        {
            InitializeComponent();
            update=updateOrder;
            Order =bl?.Order.ItemOrder(id)!;
            var a = Order.Items;
            IsBoss = Visibility.Visible;
        }
        public OrderItemWindow(int id)
        {
            Order=bl?.Order.ItemOrder(id)!;
            InitializeComponent();
            IsBoss = Visibility.Collapsed;
        }

        private void DeliveryUpdate_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            try
            {
                var a = bl?.Order.Updatesupply(Order.ID);
                Order = a!;
                update!(Order.ID);
            }
            catch (BO.BlInCorrectException exp)
            {
                MessageBox.Show(exp.Message, "can not update entity", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (BO.BlMissingEntityException exp)
            {
                MessageBox.Show(exp.Message, "can not update entity", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (BO.BlNullPropertyException exp)
            {
                MessageBox.Show(exp.Message, "can not update entity", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ShippingUpdate_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            try
            {
                var a = bl?.Order.Updateshipping(Order.ID);
                Order = a!;
                update!(Order.ID);
            }
            catch (BO.BlInCorrectException exp)
            {
                MessageBox.Show(exp.Message, "can not update entity", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (BO.BlMissingEntityException exp)
            {
                MessageBox.Show(exp.Message, "can not update entity", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (BO.BlNullPropertyException exp)
            {
                MessageBox.Show(exp.Message, "can not update entity", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
