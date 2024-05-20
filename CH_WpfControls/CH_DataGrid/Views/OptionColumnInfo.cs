using System.Globalization;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Data;

namespace CH_WpfControls.CH_DataGrid.Views
{
    public class OptionColumnInfo
    {
        public DataGridColumn Column { get; set; }
        public string? PropertyPath { get; set; }
        public IValueConverter? Converter { get; set; }
        public object? ConverterParameter { get; set; }
        public CultureInfo? ConverterCultureInfo { get; set; }
        public Type? PropertyType { get; set; }

        public OptionColumnInfo(DataGridBoundColumn boundColumn, Type boundObjectType)
        {
            Column = boundColumn;
            if (boundColumn.Binding is Binding binding && !string.IsNullOrWhiteSpace(binding.Path.Path))
            {
                PropertyInfo? propInfo = null;

                if (boundObjectType != null)
                    propInfo = boundObjectType.GetProperty(binding.Path.Path);

                if (propInfo != null)
                {
                    PropertyPath = binding.Path.Path;
                    PropertyType = propInfo != null ? propInfo.PropertyType : typeof(string);
                    Converter = binding.Converter;
                    ConverterCultureInfo = binding.ConverterCulture;
                    ConverterParameter = binding.ConverterParameter;
                }
            }
        }

        public override string ToString()
        {
            if (PropertyPath != null)
                return PropertyPath;
            else
                return string.Empty;
        }
    }
}
