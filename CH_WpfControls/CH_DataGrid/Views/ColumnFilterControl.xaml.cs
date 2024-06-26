﻿using CH_WpfControls.CH_DataGrid.Enums;
using CH_WpfControls.CH_DataGrid.Helpers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using LinExp = System.Linq.Expressions;
using System.Linq.Expressions;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Data;

namespace CH_WpfControls.CH_DataGrid.Views
{
    /// <summary>
    /// Interaktionslogik für ColumnFilterControl.xaml
    /// </summary>
    public partial class ColumnFilterControl : UserControl
    {
        private Func<object, object>? _boundColumnPropertyAccessor;

        private int _filterPeriod;

        public ObservableCollection<FilterOperationItem> FilterOperations { get; set; } = [];

        public ObservableCollection<CheckboxComboItem> DistinctPropertyValues { get; set; } = [];

        public bool HasPredicate => txtFilter.Text.Length > 0 || DistinctPropertyValues.Any(d => d.IsChecked);

        public OptionColumnInfo FilterColumnInfo { get; set; }

        public CH_DataGrid Grid { get; set; }

        private bool _canUserFilter = true;
        public bool CanUserFilter
        {
            get
            {
                return _canUserFilter;
            }
            set
            {
                _canUserFilter = value;
                CalcControlVisibility();
            }
        }

        private bool _CanUserSelectDistinct = false;
        public bool CanUserSelectDistinct
        {
            get
            {
                return _CanUserSelectDistinct;
            }
            set
            {
                _CanUserSelectDistinct = value;
                CalcControlVisibility();
            }
        }

        public Visibility FilterVisibility
        {
            get
            {
                return Visibility;
            }
            set
            {
                Visibility = value;
            }
        }

        public Brush FilterBackGround
        {
            get
            {
                if (DistinctPropertyValues.Where(i => i.IsChecked).Count() > 0)
                    return SystemColors.ControlBrush;
                else
                    return Brushes.White;
            }
        }

        public Action<ColumnFilterControl> FilterChanged;

        public ColumnFilterControl()
        {
            InitializeComponent();

            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                Loaded += new RoutedEventHandler(ColumnFilterControl_Loaded);
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

        private void ColumnFilterControl_Loaded(object sender, RoutedEventArgs args)
        {
            DataGridColumn? column = null;
            DataGridColumnHeader? colHeader = null;

            UIElement parent = (UIElement)VisualTreeHelper.GetParent(this);
            while (parent != null)
            {
                parent = (UIElement)VisualTreeHelper.GetParent(parent);
                if (colHeader == null && parent is DataGridColumnHeader dataGridColumnHeader)
                    colHeader = dataGridColumnHeader;

                if (Grid == null && parent is CH_DataGrid cH_DataGrid)
                    Grid = cH_DataGrid;
            }

            if (colHeader != null && colHeader.Column is DataGridBoundColumn dataGridBound)
                column = colHeader.Column;
            else
                return;

            if (Grid == null)
                return;

            CanUserSelectDistinct = Grid.CanUserSelectDistinct;

            if (Grid.FilterType == null)
                return;

            FilterColumnInfo = new OptionColumnInfo(dataGridBound, Grid.FilterType);

            Grid.RegisterOptionControl(this);

            FilterOperations.Clear();
            if (FilterColumnInfo.PropertyType != null)
            {
                if (FilterColumnInfo.PropertyType == typeof(string))
                {
                    FilterOperations.Add(new FilterOperationItem(FilterEnum.FilterOperation.Contains, "Contains", "../Images/Contains.png"));
                    FilterOperations.Add(new FilterOperationItem(FilterEnum.FilterOperation.StartsWith, "Starts With", "../Images/StartsWith.png"));
                    FilterOperations.Add(new FilterOperationItem(FilterEnum.FilterOperation.EndsWith, "Ends With", "../Images/EndsWith.png"));
                }

                if (FilterColumnInfo.PropertyType == typeof(string) || IsNumericType(FilterColumnInfo.PropertyType))
                    FilterOperations.Add(new FilterOperationItem(FilterEnum.FilterOperation.Equals, "Equals", "../Images/Equal.png"));

                if (IsNumericType(FilterColumnInfo.PropertyType))
                    FilterOperations.Add(new FilterOperationItem(FilterEnum.FilterOperation.NotEquals, "Not Equal", "../Images/NotEqual.png"));

                if (IsNumericType(FilterColumnInfo.PropertyType) || FilterColumnInfo.PropertyType == typeof(DateTime))
                {
                    FilterOperations.Add(new FilterOperationItem(FilterEnum.FilterOperation.GreaterThan, "Greater Than", "../Images/GreaterThan.png"));
                    FilterOperations.Add(new FilterOperationItem(FilterEnum.FilterOperation.GreaterThanEqual, "Greater Than or Equal", "../Images/GreaterThanEqual.png"));
                    FilterOperations.Add(new FilterOperationItem(FilterEnum.FilterOperation.LessThan, "Less Than", "../Images/LessThan.png"));
                    FilterOperations.Add(new FilterOperationItem(FilterEnum.FilterOperation.LessThanEqual, "Less Than or Equal", "../Images/LessThanEqual.png"));
                }

                if (FilterColumnInfo.PropertyType == typeof(DateTime))
                {
                    CanUserSelectDistinct = false;
                    FilterOperations.Add(new FilterOperationItem(FilterEnum.FilterOperation.Today, "Today", "../Images/Today.png", false));
                    FilterOperations.Add(new FilterOperationItem(FilterEnum.FilterOperation.Yesterday, "Yesterday", "../Images/Yesterday.png", false));
                    FilterOperations.Add(new FilterOperationItem(FilterEnum.FilterOperation.LastXDays, "Last X Days", "../Images/LastX.png", false));
                    FilterOperations.Add(new FilterOperationItem(FilterEnum.FilterOperation.LastXWeeks, "Last X Weeks", "../Images/LastX.png", false));
                    FilterOperations.Add(new FilterOperationItem(FilterEnum.FilterOperation.LastXMonths, "Last X Months", "../Images/LastX.png", false));
                }
                cbOperation.ItemsSource = FilterOperations;
                cbOperation.SelectedItem = FilterOperations[0];
            }

            if (FilterColumnInfo != null && FilterColumnInfo.PropertyPath != null)
            {
                foreach (var i in DistinctPropertyValues.Where(i => i.IsChecked))
                    i.IsChecked = false;
                DistinctPropertyValues.Clear();
                txtFilter.Text = string.Empty;
                _boundColumnPropertyAccessor = null;

                if (!string.IsNullOrWhiteSpace(FilterColumnInfo.PropertyPath))
                {
                    if (FilterColumnInfo.PropertyPath.Contains('.'))
                        throw new ArgumentException(string.Format("This version of the grid does not support a nested property path such as '{0}'.  Please make a first-level property for filtering and bind to that.", FilterColumnInfo.PropertyPath));

                    Visibility = Visibility.Visible;
                    ParameterExpression arg = LinExp.Expression.Parameter(typeof(object), "x");
                    LinExp.Expression expr = LinExp.Expression.Convert(arg, Grid.FilterType);
                    expr = LinExp.Expression.Property(expr, Grid.FilterType, FilterColumnInfo.PropertyPath);
                    LinExp.Expression conversion = LinExp.Expression.Convert(expr, typeof(object));
                    _boundColumnPropertyAccessor = LinExp.Expression.Lambda<Func<object, object>>(conversion, arg).Compile();
                }
                else
                {
                    Visibility = Visibility.Collapsed;
                }
                object oDefaultFilter = column.GetValue(ColumnConfiguration.DefaultFilterProperty);
                if (oDefaultFilter != null)
                    txtFilter.Text = (string)oDefaultFilter;
            }
            CalcControlVisibility();
        }

        private void ExecutePredicateGeneration(string value)
        {
            Grid.FirePredicationGeneration();
            ResetControl();
        }

        private void TxtFilter_KeyUp(object sender, KeyEventArgs args)
        {
            txtFilter.Text = ((TextBox)sender).Text;
            FilterChanged?.Invoke(this);
        }

        public Predicate<object>? GeneratePredicate()
        {
            Predicate<object>? predicate = null;

            if (cbOperation.SelectedItem is FilterOperationItem filterOperationItem)
            {
                if (DistinctPropertyValues.Any(i => i.IsChecked))
                {
                    foreach (var item in DistinctPropertyValues.Where(i => i.IsChecked))
                    {
                        if (FilterColumnInfo.PropertyPath != null && FilterColumnInfo.PropertyType != null)
                        {
                            if (predicate == null)
                            {
                                predicate = GenerateFilterPredicate(FilterColumnInfo.PropertyPath, item.ToString(), Grid.FilterType,
                                    FilterColumnInfo.PropertyType, filterOperationItem);
                            }
                            else
                                predicate = predicate.Or(GenerateFilterPredicate(FilterColumnInfo.PropertyPath, item.ToString(),
                                    Grid.FilterType, FilterColumnInfo.PropertyType.UnderlyingSystemType, filterOperationItem));
                        }
                    }
                }
                else
                {
                    if (FilterColumnInfo.PropertyPath != null && FilterColumnInfo.PropertyType != null)
                        predicate = GenerateFilterPredicate(FilterColumnInfo.PropertyPath, txtFilter.Text, Grid.FilterType,
                            FilterColumnInfo.PropertyType.UnderlyingSystemType, filterOperationItem);
                }
            }
            return predicate;
        }

        protected Predicate<object> GenerateFilterPredicate(string propertyName, string filterValue, Type objType, Type propType, FilterOperationItem filterItem)
        {
            ParameterExpression objParam = LinExp.Expression.Parameter(typeof(object), "x");
            UnaryExpression param = LinExp.Expression.TypeAs(objParam, objType);
            var prop = LinExp.Expression.Property(param, propertyName);
            var val = LinExp.Expression.Constant(filterValue);

            switch (filterItem.FilterOption)
            {
                case FilterEnum.FilterOperation.Contains:
                    return ExpressionHelper.GenerateGeneric(prop, val, propType, objParam, "Contains");
                case FilterEnum.FilterOperation.EndsWith:
                    return ExpressionHelper.GenerateGeneric(prop, val, propType, objParam, "EndsWith");
                case FilterEnum.FilterOperation.StartsWith:
                    return ExpressionHelper.GenerateGeneric(prop, val, propType, objParam, "StartsWith");
                case FilterEnum.FilterOperation.Equals:
                    return ExpressionHelper.GenerateEquals(prop, filterValue, propType, objParam);
                case FilterEnum.FilterOperation.NotEquals:
                    return ExpressionHelper.GenerateNotEquals(prop, filterValue, propType, objParam);
                case FilterEnum.FilterOperation.GreaterThanEqual:
                    return ExpressionHelper.GenerateGreaterThanEqual(prop, filterValue, propType, objParam);
                case FilterEnum.FilterOperation.LessThanEqual:
                    return ExpressionHelper.GenerateLessThanEqual(prop, filterValue, propType, objParam);
                case FilterEnum.FilterOperation.GreaterThan:
                    return ExpressionHelper.GenerateGreaterThan(prop, filterValue, propType, objParam);
                case FilterEnum.FilterOperation.LessThan:
                    return ExpressionHelper.GenerateLessThan(prop, filterValue, propType, objParam);
                case FilterEnum.FilterOperation.Today:
                    return ExpressionHelper.GenerateBetweenValues(prop, DateTime.Today.ToString(), DateTime.Today.AddDays(1.0).ToString(), propType, objParam);

                case FilterEnum.FilterOperation.Yesterday:
                    return ExpressionHelper.GenerateBetweenValues(prop, DateTime.Today.AddDays(-1.0).ToString(), DateTime.Today.ToString(), propType, objParam);

                case FilterEnum.FilterOperation.LastXDays:
                    if (_filterPeriod == 0)
                        return ExpressionHelper.GenerateBetweenValues(prop, DateTime.Today.AddDays(-1.0).ToString(), DateTime.Today.ToString(), propType, objParam);
                    else
                        return ExpressionHelper.GenerateBetweenValues(prop, DateTime.Today.AddDays(-1 * _filterPeriod).ToString(), DateTime.Today.ToString(), propType, objParam);

                case FilterEnum.FilterOperation.LastXWeeks:
                    if (_filterPeriod == 0)
                        return ExpressionHelper.GenerateBetweenValues(prop, DateTime.Today.AddDays(-7.0).ToString(), DateTime.Today.ToString(), propType, objParam);
                    else
                        return ExpressionHelper.GenerateBetweenValues(prop, DateTime.Today.AddDays(-7 * _filterPeriod).ToString(), DateTime.Today.ToString(), propType, objParam);

                case FilterEnum.FilterOperation.LastXMonths:
                    if (_filterPeriod == 0)
                        return ExpressionHelper.GenerateBetweenValues(prop, DateTime.Today.AddMonths(-1).ToString(), DateTime.Today.ToString(), propType, objParam);
                    else
                        return ExpressionHelper.GenerateBetweenValues(prop, DateTime.Today.AddMonths(-1 * _filterPeriod).ToString(), DateTime.Today.ToString(), propType, objParam);

                default:
                    throw new ArgumentException("Could not decode Search Mode.  Did you add a new value to the enum, or send in Unknown?");
            }

        }

        public void ResetControl()
        {
            foreach (var i in DistinctPropertyValues)
                i.IsChecked = false;
            txtFilter.Text = string.Empty;

            DistinctPropertyValues.Clear();
        }

        public void ResetDistinctList()
        {
            DistinctPropertyValues.Clear();
        }

        private void CalcControlVisibility()
        {
            if (CanUserFilter)
            {
                cbOperation.Visibility = Visibility.Visible;
                if (CanUserSelectDistinct)
                {
                    cbDistinctProperties.Visibility = Visibility.Visible;
                    txtFilter.Visibility = Visibility.Collapsed;
                }
                else
                {
                    cbDistinctProperties.Visibility = Visibility.Collapsed;
                    txtFilter.Visibility = Visibility.Visible;
                }
            }
            else
            {
                cbOperation.Visibility = Visibility.Collapsed;
                cbDistinctProperties.Visibility = Visibility.Collapsed;
                txtFilter.Visibility = Visibility.Collapsed;
            }
        }

        private void CbDistinctProperties_DropDownOpened(object sender, EventArgs args)
        {
            if (_boundColumnPropertyAccessor != null)
            {
                if (DistinctPropertyValues.Count == 0)
                {
                    List<object> result = [];

                    try
                    {
                        foreach (var item in Grid.ItemsSource)
                        {
                            if (item == CollectionView.NewItemPlaceholder)
                                continue;
                            object value = _boundColumnPropertyAccessor(item);

                            if (value != null)
                                if (!result.Where(o => o.ToString() == value.ToString()).Any())
                                    result.Add(value);
                        }
                        result.Sort();
                    }
                    catch
                    {
                        if (System.Diagnostics.Debugger.IsLogging())
                            System.Diagnostics.Debugger.Log(0, "Warning", "There is no default compare set for the object type");
                    }
                    foreach (var obj in result)
                    {
                        CheckboxComboItem item = new()
                        {
                            Description = GetFormattedValue(obj),
                            IsChecked = false
                        };
                        DistinctPropertyValues.Add(item);
                    }
                    cbDistinctProperties.ItemsSource = DistinctPropertyValues;
                }
            }
        }

        private string GetFormattedValue(object obj)
        {
            if (FilterColumnInfo.Converter != null)
                return FilterColumnInfo.Converter.Convert(obj, typeof(string), FilterColumnInfo.ConverterParameter, FilterColumnInfo.ConverterCultureInfo).ToString();
            else
                return obj.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Filter_CheckedChanged(object sender, RoutedEventArgs args)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is CheckboxComboItem checkboxComboItem)
            {
                List<CheckboxComboItem> list = DistinctPropertyValues.Where(i => i.IsChecked).ToList();

                if (list.Count > 0)
                {
                    StringBuilder sb = new();
                    foreach (var i in DistinctPropertyValues.Where(i => i.IsChecked))
                        sb.AppendFormat("{0}{1}", sb.Length > 0 ? "," : "", i);
                    txtFilter.Text = sb.ToString();
                }
                else
                {
                    txtFilter.Text = string.Empty;
                }
                if (checkBox.IsChecked is bool isChecked)
                    checkboxComboItem.IsChecked = isChecked;
                FilterChanged?.Invoke(this);
            }
        }

        private void CbOperation_SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            if (args.AddedItems.Count > 0 && args.AddedItems[0] is FilterOperationItem filterOperationItem)
            {
                switch (filterOperationItem.Description)
                {
                    case "Last X Days":
                        if (Grid.IsFilterLoaded)
                        {
                            _filterPeriod = Grid.LastX;
                        }

                        ExecutePredicateGeneration(_filterPeriod.ToString());

                        break;

                    case "Last X Months":
                        if (Grid.IsFilterLoaded)
                        {
                            _filterPeriod = Grid.LastX;
                        }

                        ExecutePredicateGeneration(_filterPeriod.ToString());

                        break;

                    case "Last X Weeks":
                        if (Grid.IsFilterLoaded)
                        {
                            _filterPeriod = Grid.LastX;
                        }

                        ExecutePredicateGeneration(_filterPeriod.ToString());

                        break;
                }

                if (filterOperationItem != null && !filterOperationItem.NeedsFilterValue)
                {
                    if (DoesFilterTextNeedToBeEmpty(filterOperationItem))
                    {
                        txtFilter.Text = " ";
                    }
                }
            }
            FilterChanged?.Invoke(this);
        }

        private bool DoesFilterTextNeedToBeEmpty(FilterOperationItem filterOperationItem) =>
            !((filterOperationItem.FilterOption == FilterEnum.FilterOperation.LastXDays ||
            filterOperationItem.FilterOption == FilterEnum.FilterOperation.LastXWeeks ||
            filterOperationItem.FilterOption == FilterEnum.FilterOperation.LastXMonths) && _filterPeriod == 0);
    }
}
