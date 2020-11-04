using System;
using System.Globalization;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Resources.ValueConverters.d
{
	public class BoolToImageSourceConverter<T> : IValueConverter
	{
		public BoolToImageSourceConverter()
		{
		}

		public T FalseValue { get; set; }
        public T TrueValue { get; set; }

		public ImageSource FalseImage { get; set; }
		public ImageSource TrueImage { get; set; }


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return FalseValue;
            }
            else
            {
				FalseImage = ImageSource.FromResource(FalseValue as String);
				TrueImage = ImageSource.FromResource(TrueValue as String);

				return (bool)value ? TrueImage : FalseImage;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? value.Equals(TrueValue) : false;
        }
	}

	public class BoolToImageConverter : BoolToImageSourceConverter<String> { }
}

