using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace GameDrawer.Converters
{
    public class CanShowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var b = (bool?)value;
            return b == true ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CanShowMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string)parameter)
            {
                case "Any":
                    return values.Select(k => k == DependencyProperty.UnsetValue ? null : (bool?)k)
                        .Any(k => k == true) ? Visibility.Visible : Visibility.Collapsed;
                case "All":
                default:
                    return values.Select(k => k == DependencyProperty.UnsetValue ? null : (bool?)k)
                        .All(k => k == true) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CannotShowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var b = (bool?)value;
            return b == true ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CannotShowMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string)parameter)
            {
                case "All":
                    return values.Select(k => k == DependencyProperty.UnsetValue ? null : (bool?)k)
                        .All(k => k == true) ? Visibility.Collapsed : Visibility.Visible;
                case "Any":
                default:
                    return values.Select(k => k == DependencyProperty.UnsetValue ? null : (bool?)k)
                        .Any(k => k == true) ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CannotShowNullsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string)parameter)
            {
                case "All":
                    return values.All(k => k is null) ? Visibility.Collapsed : Visibility.Visible;
                case "Any":
                default:
                    return values.Any(k => k is null) ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}