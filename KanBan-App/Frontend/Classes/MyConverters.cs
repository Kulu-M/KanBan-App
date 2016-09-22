using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Frontend
{
    public class Converter1 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((int)value == (int)parameter)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
