using System;
using System.Globalization;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Resources.ValueConverters
{
    public class BoolToValueConverter<T> : IValueConverter
    {

        public T FalseValue { get; set; }
        public T TrueValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null)
            {
                return FalseValue;
            }
            else
            {
                return (bool)value ? TrueValue : FalseValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? value.Equals(TrueValue) : false;
        }
    }

    //List - Implementations of BoolToValueConverter
    public class BoolToStringConverter : BoolToValueConverter<String> { }
    public class InverseBoolConverter : BoolToValueConverter<bool> { }

       



}
