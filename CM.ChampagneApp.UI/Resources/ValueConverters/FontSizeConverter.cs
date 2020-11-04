using System;
using System.Globalization;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Resources.ValueConverters
{
	public class FontSizeConverter : IValueConverter
    {
        public double ResizingFactor { get; set; } = 0.2;

        public double LargeResizingFactor { get; set; } = 0;

        public FontSizeConverter()
		{
		}

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			try
			{
				var convertedSize = double.Parse((string)parameter);
				if(Device.iOS.Equals("iOS"))
				{
					if(App.DisplaySettings.Width < 325)
					{
						convertedSize *= 1.0 - ResizingFactor; 
					}
					else if(App.DisplaySettings.Width < 380)
					{
						convertedSize *= 1.0;
					}
					else if(App.DisplaySettings.Width < 420)
					{
                        var factor = ResizingFactor;
                        if(LargeResizingFactor > 0)
                        {
                            factor = LargeResizingFactor;
                        }

                        convertedSize *= 1.0 + factor;
					}
				}

				return convertedSize;

			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}

			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}

