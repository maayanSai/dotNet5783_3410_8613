using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Data;
namespace PL
{
 class ConvertImagePathToBitmap : IValueConverter
{
    public object Convert(object value, Type targetType,object parameter,CultureInfo culture)
    {
        try
        {
            string imageRelativeName = (string)value;
            string currenDir = Environment.CurrentDirectory[..^4];
            string imageFullName = currenDir + imageRelativeName;
            BitmapImage bitmapImage = new BitmapImage(new Uri(imageFullName));
            return bitmapImage;
        }
        catch(Exception ex)
        {
            string imageRelativeName = @"\pictures\IMG_FAILURE.png";
            string currenDir = Environment.CurrentDirectory[..^4];
            string imageFullName = currenDir + imageRelativeName;
            BitmapImage bitmapImage = new BitmapImage(new Uri(imageFullName));
            return bitmapImage;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
//public class ConvertIsBossToVisability : IValueConverter
//    {
//        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            if (((bool)value)==true)
//                return Visibility.Visible;
//            else
//                return Visibility.Collapsed;
//        }

//        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            throw new NotImplementedException();
//        }
//    }
}

