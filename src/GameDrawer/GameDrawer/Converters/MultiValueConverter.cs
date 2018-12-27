using System;
using System.Globalization;
using System.Windows.Data;
using GameDrawer.Model;

namespace GameDrawer.Converters
{
    class MultiCommandParameterConverter:IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Clone();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class WindowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = (string)parameter;
            bool isConsole = value is ConsoleMachine;
            switch (type)
            {
                case "Height":
                    return isConsole ? 365 : 300;
                case "Title":
                    return isConsole ? "添加游戏" : "添加分类";
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
