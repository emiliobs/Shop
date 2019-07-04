namespace FourWays.Core.Converter
{
    using MvvmCross.Converters;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    public class DecimalToStringValueConverter : MvxValueConverter<decimal, string>
    {
        protected override string Convert(decimal value, Type targetType, object parameter, 
                                          CultureInfo culture)
        {
            return $"{value:C2}";
        }
    }
}
