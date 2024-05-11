using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ShopManagement.Converters
{
    internal class ProdusConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int intValue;
            if (values[0] != null && values[1] != null && int.TryParse(values[2].ToString(), out intValue))
            {
                
                return Tuple.Create(values[0].ToString(), values[1].ToString(), System.Convert.ToInt32(values[2].ToString()));
            }
            else
            {
                return null;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
