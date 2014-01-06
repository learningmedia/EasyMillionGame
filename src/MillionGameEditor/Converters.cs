using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MillionGameEditor
{
    public static class Converters
    {
        public static IValueConverter IndexToVisibility
        {
            get
            {
                return new LambdaConverter(x => ((int) x != -1) ? Visibility.Visible : Visibility.Collapsed);
            }
        }
        
        public static IValueConverter NotNullToVisibility
        {
            get
            {
                return new LambdaConverter(x => (x != null) ? Visibility.Visible : Visibility.Collapsed);
            }
        }

        private sealed class LambdaConverter : IValueConverter
        {
            private readonly Func<object, object> func;

            public LambdaConverter(Func<object, object> func)
            {
                this.func = func;
            }

            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return func(value);
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }
}
