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
    /// Interaction logic for PicturesWindow.xaml
    /// </summary>
    public partial class PicturesWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public ObservableCollection<string> PictursPathList
        {
            get { return (ObservableCollection<string>)GetValue(PictursPathListdp); }
            set { SetValue(PictursPathListdp, value); }
        }

        public static readonly DependencyProperty PictursPathListdp =
               DependencyProperty.Register("PictursPathList", typeof(ObservableCollection<string>), typeof(Window));


        public PicturesWindow()
        {
            


            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //var v = ((Button)sender).Tag;
            
        }

    }
}
