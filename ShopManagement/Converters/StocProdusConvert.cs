using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using static System.Net.Mime.MediaTypeNames;

namespace ShopManagement.Converters
{
    internal class StocProdusConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime? date = null;
            DateTime? date2 = null;

            // Verifică dacă valoarea pentru dataAprovizionare (values[2]) poate fi convertită într-un DateTime
            if (DateTime.TryParseExact(values[2]?.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                date = parsedDate;
            }

            // Verifică dacă valoarea pentru dataExpirare (values[3]) poate fi convertită într-un DateTime
            if (DateTime.TryParseExact(values[3]?.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate2))
            {
                date2 = parsedDate2;
            }

            // Verifică dacă toate valorile sunt diferite de null
            if (values[0] != null && values[1] != null && date != null && date2 != null && values[4] != null && values[5] != null && values[6] != null)
            {
                // Creează și returnează obiectul Tuple cu valorile convertite
                return Tuple.Create(System.Convert.ToInt32(values[0].ToString()), System.Convert.ToInt32(values[1].ToString()), date, date2, values[4].ToString(), System.Convert.ToDouble(values[5].ToString()), System.Convert.ToDouble(values[6].ToString()));
            }
            else
            {
                // Una sau mai multe valori sunt nule, întoarce null
                return null;
            }
        }


        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
