using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Data;
using System.Windows;

namespace PL;
/// <summary>
/// Convert class
/// </summary>
class ConvertImagePathToBitmap : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            string imageRelativeName = (string)value;
            string currenDir = Environment.CurrentDirectory[..^4];
            string imageFullName = currenDir + imageRelativeName;
            BitmapImage bitmapImage = new (new Uri(imageFullName));
            return bitmapImage;
        }
        catch (Exception)
        {
            string imageRelativeName = @"\pictures\IMG_FAILURE.png";
            string currenDir = Environment.CurrentDirectory[..^4];
            string imageFullName = currenDir + imageRelativeName;
            BitmapImage bitmapImage = new(new Uri(imageFullName));
            return bitmapImage;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}


