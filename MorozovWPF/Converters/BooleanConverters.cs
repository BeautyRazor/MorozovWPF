using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace MorozovWPF.Converters {

    public class ReverseBoolToVisibilityConverter : MarkupExtension, IValueConverter {
        public object Convert(object      value, Type targetType, object parameter,
                              CultureInfo culture) {
            if (value != null && (bool) value == false) {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object      value, Type targetType, object parameter,
                                  CultureInfo culture) {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return instance ?? (instance = new ReverseBoolToVisibilityConverter());
        }

        static ReverseBoolToVisibilityConverter instance = null;
    }

    public class BoolToVisibilityConverter : MarkupExtension, IValueConverter {
        public object Convert(object      value, Type targetType, object parameter,
                              CultureInfo culture) {
            if (value != null && (bool) value) {
                return Visibility.Visible;
            }

            return Visibility.Hidden;
        }

        public object ConvertBack(object      value, Type targetType, object parameter,
                                  CultureInfo culture) {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return instance ?? (instance = new BoolToVisibilityConverter());
        }

        static BoolToVisibilityConverter instance = null;
    }

    public class InverseBooleanConverter : MarkupExtension, IValueConverter {
        public object Convert(object      value, Type targetType, object parameter,
                              CultureInfo culture) {
            return !(bool) value;
        }

        public object ConvertBack(object      value, Type targetType, object parameter,
                                  CultureInfo culture) {
            throw new NotSupportedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return instance ?? (instance = new InverseBooleanConverter());
        }

        private static InverseBooleanConverter instance = null;
    }

    public class IndexToBoolConverter : MarkupExtension, IValueConverter {
        public object Convert(object                           value, Type targetType, object parameter,
                              CultureInfo culture) {
            return value != null && (int) value >= 0;
        }
        
        public object ConvertBack(object                           value, Type targetType, object parameter,
                                  CultureInfo culture) {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return instance ?? (instance = new IndexToBoolConverter());
        }

        private static IndexToBoolConverter instance = null;
    }
}