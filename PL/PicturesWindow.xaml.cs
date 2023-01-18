using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

        static readonly DependencyProperty PictursPathListdp =
               DependencyProperty.Register("PictursPathList", typeof(ObservableCollection<string>), typeof(Window));

        string Pictures { get => (string)GetValue(PictursPathListdp); set => SetValue(PictursPathListdp, value); }

        public PicturesWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog()==true)
            {
                image.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                //Pictures.ImageRelativeName = openFileDialog.FileName;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var v = ((Button)sender).Tag;

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
