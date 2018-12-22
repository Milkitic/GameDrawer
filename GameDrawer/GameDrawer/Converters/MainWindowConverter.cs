using GameDrawer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GameDrawer.Converters
{
    internal class WindowMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var state = (WindowState)value;
            return state == WindowState.Maximized ? new Thickness(8, 7, 8, 8) : new Thickness(1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    internal class TitleBarHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var state = (WindowState)value;
            return state == WindowState.Maximized ? 24 : 30;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    internal class ChromeForegroundConverter : IValueConverter
    {
        private static System.Drawing.Color? _color1;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var windowActivated = (bool)value;
            if (windowActivated)
            {
                if (_color1 == null)
                    _color1 = ColorExtension.GetChromeColor();
                if (_color1 != null)
                {
                    var color = _color1.Value;
                    var average = (color.R + color.G + color.B) / 3f;
                    return average >= 128
                        ? new SolidColorBrush(Color.FromRgb(0, 0, 0))
                        : new SolidColorBrush(Color.FromRgb(255, 255, 255));
                }

                return new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }

            return new SolidColorBrush(Color.FromRgb(164, 164, 164));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    internal class ChromeHoverForegroundConverter : IValueConverter
    {
        private static System.Drawing.Color? _color1;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var windowActivated = (bool)value;
            if (windowActivated)
            {
                if (_color1 == null)
                    _color1 = ColorExtension.GetChromeColor();
                if (_color1 != null)
                {
                    var color = _color1.Value;
                    var average = (color.R + color.G + color.B) / 3f;
                    return average >= 128
                        ? new SolidColorBrush(Color.FromRgb(0, 0, 0))
                        : new SolidColorBrush(Color.FromRgb(255, 255, 255));
                }

                return new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }

            return new SolidColorBrush(Color.FromRgb(0, 0, 0));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    internal class ChromeHoverBackgroundConverter : IValueConverter
    {
        private static System.Drawing.Color? _color1;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var windowActivated = (bool)value;
            if (windowActivated)
            {
                if (_color1 == null)
                    _color1 = ColorExtension.GetChromeColor();
                if (_color1 != null)
                {
                    var color = _color1.Value;
                    var average = (color.R + color.G + color.B) / 3f;
                    return average >= 128
                        ? new SolidColorBrush(Color.FromArgb(25, 0, 0, 0))
                        : new SolidColorBrush(Color.FromArgb(25, 255, 255, 255));
                }

                return new SolidColorBrush(Color.FromArgb(25, 255, 255, 255));
            }

            return new SolidColorBrush(Color.FromArgb(25, 0, 0, 0));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    internal class QuoteHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value - 9;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    internal class CanShowConverter : IValueConverter
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
    internal class CanShowMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var positive = System.Convert.ToBoolean(parameter);
            var model = (ConsoleMachine)values[0];
            var b = values[1] as bool?;
            if (model is null)
                return Visibility.Collapsed;

            if (positive) return b == true ? Visibility.Visible : Visibility.Collapsed;
            return b == true ? Visibility.Collapsed : Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    internal class CannotShowConverter : IValueConverter
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

    internal class ContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var b = (bool?)value;
            return Application.Current.FindResource(b == true ? "WhiteOkControl" : "WhiteEditControl");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    internal class IconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = (string)value;
            if (!string.IsNullOrEmpty(path))
            {
                if (File.Exists(path))
                    try
                    {
                        return GetImage(path);
                    }
                    catch (Exception e)
                    {

                    }
            }

            return Application.Current.FindResource("DefaultIcon");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static BitmapImage GetImage(string imagePath)
        {
            BitmapImage bitmap = new BitmapImage();

            if (File.Exists(imagePath))
            {
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;

                using (Stream ms = new MemoryStream(File.ReadAllBytes(imagePath)))
                {
                    bitmap.StreamSource = ms;
                    bitmap.EndInit();
                    bitmap.Freeze();
                }
            }

            return bitmap;
        }
    }
    internal class IconSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = (string)value;
            if (!string.IsNullOrEmpty(path))
            {
                var fi = new FileInfo(path);
                if (fi.Exists &&
                    Path.GetFullPath(fi.Directory.FullName) == Path.GetFullPath(Config.IconCacheDirectory))
                {
                    return 32;
                }
            }

            return 48;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    internal class IsEnabledConverter : IMultiValueConverter
    {
        //public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    return !(value is null);
        //}

        //public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    throw new NotImplementedException();
        //}

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 1)
            {
                return !(values[0] is null);
            }
            else
            {
                var consoleObj = values[0];
                var isRefreshing = (bool)values[1];
                return !(values[0] is null) && !isRefreshing;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    internal class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is null ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    internal class GameInfoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var args = (parameter as string)?.Split('|');
            if (args != null)
            {
                foreach (var arg in args)
                {
                    var par = arg.Split(';');
                    switch (par[0])
                    {
                        case "concat":
                            return par[1] + value + par[2];
                        case "convert-size":
                            value = Extension.GetBytesReadable((long)value);
                            break;
                    }
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    internal class NotEmptyShowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = (string)value;
            return string.IsNullOrEmpty(str) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    internal class SyncRunningConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isRunning = (bool)value;
            switch (parameter)
            {
                case "Visibility":
                    return isRunning ? Visibility.Visible : Visibility.Hidden;
                case "VisibilityNegative":
                    return isRunning ? Visibility.Hidden : Visibility.Visible;
                case "IsIndeterminate":
                    return isRunning;
                default:

                    return !isRunning;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    internal class RefreshRunningConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isRunning = (bool)value;
            switch (parameter)
            {
                case "Visibility":
                    return isRunning ? Visibility.Visible : Visibility.Hidden;
                case "VisibilityNegative":
                    return isRunning ? Visibility.Hidden : Visibility.Visible;
                case "IsIndeterminate":
                    return isRunning;
                default:

                    return !isRunning;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    internal class SortConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            //var type = (string)values[1];
            //if (values[0] is IEnumerable<FileModelBase> fileModelBases)
            //{
            //    throw new NotImplementedException();
            //    return new ObservableCollection<FileModelBase>(SortFiles(type, fileModelBases));
            //}

            return values[0];
        }

        private static IEnumerable<FileModelBase> SortFiles(string type, IEnumerable<FileModelBase> consoleMachines)
        {
            switch (type)
            {
                case "DateTime":
                    return consoleMachines.OrderBy(k => k.NameWithoutExtension);
                case "Name":
                default:
                    return consoleMachines.OrderBy(k => k.NameWithoutExtension);
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
