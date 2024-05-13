using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CH_WpfControls.CH_DataGrid.Views
{
    public class CH_GridColumn : DataGridBoundColumn
    {
        private static Style _defaultEditingElementStyle;
        private static Style _defaultElementStyle;
        private Type? _propertyType;

        static CH_GridColumn()
        {
            ElementStyleProperty.OverrideMetadata(typeof(CH_GridColumn), new FrameworkPropertyMetadata(DefaultElementStyle));
            EditingElementStyleProperty.OverrideMetadata(typeof(CH_GridColumn), new FrameworkPropertyMetadata(DefaultEditingElementStyle));
        }

        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            if (_propertyType != null)
            {
                if (_propertyType == typeof(string))
                {

                }
                else if (IsNumericType(_propertyType))
                {

                }
            }
            else if (_propertyType == typeof(DateTime))
            {
                DatePicker dp = new();
                ApplyStyle(true, false, dp);
                ApplyBinding(dp, DatePicker.SelectedDateProperty);
                return dp;
            }
            return new TextBox();
        }

        private void ApplyStyle(bool isEditing, bool defaultToElementStyle, FrameworkElement element)
        {
            Style style = PickStyle(isEditing, defaultToElementStyle);
            if (style != null)
            {
                element.Style = style;
            }
        }

        private Style PickStyle(bool isEditing, bool defaultToElementStyle)
        {
            Style elementStyle = isEditing ? EditingElementStyle : ElementStyle;
            if ((isEditing && defaultToElementStyle) && (elementStyle == null))
            {
                elementStyle = ElementStyle;
            }
            return elementStyle;
        }

        private void ApplyBinding(DependencyObject target, DependencyProperty property)
        {
            BindingBase binding = Binding;
            if (binding != null)
            {
                BindingOperations.SetBinding(target, property, binding);
            }
            else
            {
                BindingOperations.ClearBinding(target, property);
            }
        }

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            if (Binding is Binding binding)
            {
                PropertyInfo? propInfo = dataItem.GetType().GetProperty(binding.Path.Path);
                _propertyType = propInfo != null ? propInfo.PropertyType : typeof(string);
            }

            TextBlock tb = new();

            ApplyStyle(false, false, tb);
            ApplyBinding(tb, TextBlock.TextProperty);
            return tb;
        }

        public static Style DefaultEditingElementStyle
        {
            get
            {
                if (_defaultEditingElementStyle == null)
                {
                    Style style = new(typeof(DatePicker));
                    style.Setters.Add(new Setter(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Center));
                    style.Setters.Add(new Setter(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Top));
                    style.Seal();
                    _defaultEditingElementStyle = style;
                }
                return _defaultEditingElementStyle;
            }
        }

        public static Style DefaultElementStyle
        {
            get
            {
                if (_defaultElementStyle == null)
                {
                    Style style = new(typeof(TextBlock));
                    style.Setters.Add(new Setter(FrameworkElement.MarginProperty, new Thickness(2.0, 0.0, 2.0, 0.0)));
                    style.Seal();
                    _defaultElementStyle = style;
                }
                return _defaultElementStyle;
            }
        }

        private static bool IsNumericType(Type type)
        {
            bool result = false;

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    result = true;
                    break;
            }
            return result;
        }
    }
}
