using System;
using System.Globalization;
using Xamarin.Forms;

namespace TD2Sarget
{
    public class IdToUrl : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int id = (int)value;

            return "https://td-api.julienmialon.com/images/"+id;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}