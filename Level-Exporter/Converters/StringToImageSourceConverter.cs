// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringToImageSourceConverter.cs">
//   Copyright (c) 2022
// </copyright>
// <summary>
//   A string to image source converter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Level_Exporter
{
    /// <summary> A string to image source converter. </summary>
    public class StringToImageSourceConverter : IValueConverter
    {
        /// <summary> Converts a value. </summary>
        ///
        /// <param name="value">      The value produced by the binding source. </param>
        /// <param name="targetType"> The type of the binding target property. </param>
        /// <param name="parameter">  The converter parameter to use. </param>
        /// <param name="culture">    The culture to use in the converter. </param>
        ///
        /// <returns> A converted value. If the method returns <see langword="null" />, the valid null value is used. </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), @"ImageSource name is null");
            }

            var assemblyName = Assembly.GetExecutingAssembly().GetName();
            var path = $"pack://application:,,,/{assemblyName};component/Resources/Assets/{value}";
            return new BitmapImage(new Uri(path, UriKind.Absolute));
        }

        /// <summary> Converts a value. </summary>
        ///
        /// <param name="value">      The value that is produced by the binding target. </param>
        /// <param name="targetType"> The type to convert to. </param>
        /// <param name="parameter">  The converter parameter to use. </param>
        /// <param name="culture">    The culture to use in the converter. </param>
        ///
        /// <returns> A converted value. If the method returns <see langword="null" />, the valid null value is used. </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
