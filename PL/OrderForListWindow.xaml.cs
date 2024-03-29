﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
/// Interaction logic for OrderForListWindow.xaml
/// </summary>
/// 
public partial class OrderForListWindow : Window
{
    BlApi.IBl? bl = BlApi.Factory.Get();

        public void Update(int orderId)
        {
            BO.OrderForList o = OrderList.First(x => x?.ID==orderId);
            int index = OrderList.IndexOf(o);
            OrderList.RemoveAt(index);
            OrderList.Add(bl?.Order.GetOrderForList(orderId)!);
        }
        public ObservableCollection<BO.OrderItem> OrderItemViewSource
        {
            get { return (ObservableCollection<BO.OrderItem>)GetValue(OrdersDep); }
            set { SetValue(OrdersDep, value); }
        }
        public static readonly DependencyProperty OrderItemViewSourceDp = DependencyProperty.Register("OrderItemViewSource", typeof(ObservableCollection<BO.OrderItem>), typeof(OrderForListWindow));
        public ObservableCollection<BO.OrderForList> OrderList
        {
            get { return (ObservableCollection<BO.OrderForList>)GetValue(OrdersDep); }
            set  { SetValue(OrdersDep, value); }
        }
        public static readonly DependencyProperty OrdersDep = DependencyProperty.Register("OrderList", typeof(IEnumerable<BO.OrderForList>), typeof(OrderForListWindow));
        public OrderForListWindow()
        {
            InitializeComponent();
            OrderList = new (bl!.Order.GetOrders()!);
        }
       private void OrderForListDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled= true;
            BO.OrderForList? O = (BO.OrderForList)((DataGrid)sender).SelectedItem;
            OrderItemWindow? orderWindow = new(O.ID, Update);
            orderWindow.ShowDialog(); 
       }

    private void Statisticks_Click(object sender, RoutedEventArgs e)
    {
        StatisticksOrdersWindow p = new();
        Content = p;
    }
}
