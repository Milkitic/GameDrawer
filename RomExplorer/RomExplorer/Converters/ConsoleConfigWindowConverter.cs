using RomExplorer.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace RomExplorer.Converters
{
    class LabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = (string)parameter;
            bool isConsole = value is ConsoleMachine;
            switch (type)
            {
                case "FileModel":
                    return isConsole ? "分类设置" : "游戏设置";
                case "Name":
                    return isConsole ? "分类名称" : "游戏名称";
                case "Host":
                    return isConsole ? "默认启动程序" : "启动程序";
                case "Arg":
                    return isConsole ? "默认启动参数" : "启动参数";
                case "Des":
                    return isConsole ? "分类描述" : "文件描述";
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class ShowLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is ConsoleMachine ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    class IfCheckConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isPositive = System.Convert.ToBoolean(parameter);
            return isPositive ? value : !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isPositive = System.Convert.ToBoolean(parameter);
            return isPositive ? value : !(bool)value;
        }
    }
}
