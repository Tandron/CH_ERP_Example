using CH_WpfControls.CH_DataGrid.Views;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CH_WpfControls.CH_DataGrid
{
    /// <summary>
    /// Interaktionslogik für CH_DataGrid.xaml
    /// </summary>
    public partial class CH_DataGrid : DataGrid
    {
        public delegate void FilterChangedEvent(object sender, FilterChangedEventArgs e);
        public delegate void CancelableFilterChangedEvent(object sender, CancelableFilterChangedEventArgs e);

        public event CancelableFilterChangedEvent BeforeFilterChanged;
        public event FilterChangedEvent AfterFilterChanged;

        private Action<ColumnFilterControl> _filterHandler;

        protected bool IsResetting { get; set; }

        public List<ColumnFilterControl> Filters { get; set; } = [];
        public Type FilterType { get; set; }

        public bool IsFilterLoaded { get; set; }

        public int LastX { get; set; }

        protected ICollectionView? CollectionView => ItemsSource as ICollectionView;

        #region FilteredItemsSource DependencyProperty
        public static readonly DependencyProperty FilteredItemsSourceProperty =
                DependencyProperty.Register("FilteredItemsSource", typeof(IEnumerable), typeof(CH_DataGrid),
                    new PropertyMetadata(null, new PropertyChangedCallback(OnFilteredItemsSourceChanged)));

        public IEnumerable FilteredItemsSource
        {
            get => (IEnumerable)GetValue(FilteredItemsSourceProperty);
            set => SetValue(FilteredItemsSourceProperty, value);
        }

        public static void OnFilteredItemsSourceChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            if (sender is CH_DataGrid cH_DataGrid)
            {
                var list = (IEnumerable)args.NewValue;
                var view = new CollectionViewSource
                {
                    Source = list
                };
                Type srcT = args.NewValue.GetType().GetInterfaces().First(i => i.Name.StartsWith("IEnumerable"));

                cH_DataGrid.FilterType = srcT.GetGenericArguments().First();
                cH_DataGrid.ItemsSource = CollectionViewSource.GetDefaultView(list);
                if (cH_DataGrid.Filters != null)
                    foreach (var filter in cH_DataGrid.Filters)
                        filter.ResetControl();
            }
        }
        #endregion


        #region Filter Properties

        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue("False")]
        private bool _canUserSelectDistinct = false;
        public bool CanUserSelectDistinct
        {
            get { return _canUserSelectDistinct; }
            set
            {
                if (_canUserSelectDistinct != value)
                {
                    _canUserSelectDistinct = value;
                    OnPropertyChanged("CanUserSelectDistinct");
                    foreach (var optionControl in Filters)
                        optionControl.CanUserSelectDistinct = _canUserSelectDistinct;
                }
            }
        }

        [Bindable(false)]
        [Category("Appearance")]
        [DefaultValue("True")]
        private bool _canUserFilter = true;
        public bool CanUserFilter
        {
            get { return _canUserFilter; }
            set
            {
                if (_canUserFilter != value)
                {
                    _canUserFilter = value;
                    OnPropertyChanged("CanUserFilter");
                    foreach (var optionControl in Filters)
                        optionControl.CanUserFilter = _canUserFilter;
                }
            }
        }
        #endregion Filter Properties
        public CH_DataGrid()
        {
            _filterHandler = Filter_PropertyChanged;
            InitializeComponent();
            Style = GetStyle("DataGridStyle");
            CellStyle = GetStyle("DataGridCellStyle");

            //in App.xaml in your application, you need to update the DataGridStyle and DataGridCellStyle styles
            //Jib.WPF.Testbed shows an example that conforms to the MahApps Teal light theme
        }

        static public Style? GetStyle(string keyName)
        {
            object resource = Application.Current.TryFindResource(keyName);
            if (resource != null && resource.GetType() == typeof(Style))
                return (Style)resource;
            else
                return null;
        }


        /// <summary>
        /// Whenever any registered OptionControl raises the FilterChanged property changed event, we need to rebuild
        /// the new predicate used to filter the CollectionView.  Since Multiple Columns can have predicate we need to
        /// iterate over all registered OptionControls and get each predicate.
        /// </summary>
        /// <param name="columnFilter">The property which has been changed</param>
        private void Filter_PropertyChanged(ColumnFilterControl columnFilter)
        {
            Predicate<object>? predicate = null;
            foreach (var filter in Filters)
                if (filter.HasPredicate)
                    if (predicate == null)
                        predicate = filter.GeneratePredicate();
                    else
                        predicate = predicate.And(filter.GeneratePredicate());
            bool canContinue = true;
            var args = new CancelableFilterChangedEventArgs(predicate);
            if (BeforeFilterChanged != null && !IsResetting)
            {
                BeforeFilterChanged(this, args);
                canContinue = !args.Cancel;
            }
            if (canContinue)
            {
                ListCollectionView? view = CollectionViewSource.GetDefaultView(this.ItemsSource) as ListCollectionView;
                if (view != null && view.IsEditingItem)
                    view.CommitEdit();
                if (view != null && view.IsAddingNew)
                    view.CommitNew();
                if (CollectionView != null)
                    CollectionView.Filter = predicate;
                if (AfterFilterChanged != null)
                    AfterFilterChanged(this, new FilterChangedEventArgs(predicate));
            }
            else
            {
                IsResetting = true;
                columnFilter.ResetControl();
                IsResetting = false;
            }
        }

        public void RegisterOptionControl(ColumnFilterControl ctrl)
        {
            if (!Filters.Contains(ctrl))
            {
                ctrl.FilterChanged += _filterHandler;
                Filters.Add(ctrl);
            }
        }

        public void FirePredicationGeneration()
        {
            Predicate<object> predicate = null;
            foreach (var filter in Filters)
                if (filter.HasPredicate)
                    if (predicate == null)
                        predicate = filter.GeneratePredicate();
                    else
                        predicate = predicate.And(filter.GeneratePredicate());
            bool canContinue = true;
            CancelableFilterChangedEventArgs args = new(predicate);
            if (BeforeFilterChanged != null && !IsResetting)
            {
                BeforeFilterChanged(this, args);
                canContinue = !args.Cancel;
            }
            if (canContinue)
            {
                ListCollectionView? view = CollectionViewSource.GetDefaultView(ItemsSource) as ListCollectionView;
                if (view != null && view.IsEditingItem)
                    view.CommitEdit();
                if (view != null && view.IsAddingNew)
                    view.CommitNew();
                if (CollectionView != null)
                    CollectionView.Filter = predicate;
                if (AfterFilterChanged != null)
                    AfterFilterChanged(this, new FilterChangedEventArgs(predicate));
            }
            else
            {
                IsResetting = true;
                IsResetting = false;
            }
        }

        #region Grouping

        public void AddGroup(string boundPropertyName)
        {
            if (!string.IsNullOrWhiteSpace(boundPropertyName) && CollectionView != null && CollectionView.GroupDescriptions != null)
            {
                foreach (var groupedCol in CollectionView.GroupDescriptions)
                {
                    PropertyGroupDescription? propertyGroup = groupedCol as PropertyGroupDescription;

                    if (propertyGroup != null && propertyGroup.PropertyName == boundPropertyName)
                        return;
                }

                CollectionView.GroupDescriptions.Add(new PropertyGroupDescription(boundPropertyName));
            }
        }

        public bool IsGrouped(string boundPropertyName)
        {
            if (CollectionView != null && CollectionView.Groups != null)
            {
                foreach (var g in CollectionView.GroupDescriptions)
                {
                    var pgd = g as PropertyGroupDescription;

                    if (pgd != null)
                        if (pgd.PropertyName == boundPropertyName)
                            return true;
                }
            }

            return false;
        }

        public void RemoveGroup(string boundPropertyName)
        {
            if (!string.IsNullOrWhiteSpace(boundPropertyName) && CollectionView != null && CollectionView.GroupDescriptions != null)
            {
                PropertyGroupDescription selectedGroup = null;

                foreach (var groupedCol in CollectionView.GroupDescriptions)
                {
                    var propertyGroup = groupedCol as PropertyGroupDescription;

                    if (propertyGroup != null && propertyGroup.PropertyName == boundPropertyName)
                    {
                        selectedGroup = propertyGroup;
                    }
                }

                if (selectedGroup != null)
                    CollectionView.GroupDescriptions.Remove(selectedGroup);

                //if (CollapseLastGroup && CollectionView.Groups != null)
                //foreach (CollectionViewGroup group in CollectionView.Groups)
                //    RecursiveCollapse(group);
            }
        }

        public void ClearGroups()
        {
            if (CollectionView != null && CollectionView.GroupDescriptions != null)
                CollectionView.GroupDescriptions.Clear();
        }
        #endregion Grouping

        #region Freezing

        public void FreezeColumn(DataGridColumn column)
        {
            if (Columns != null && Columns.Contains(column))
            {
                column.DisplayIndex = FrozenColumnCount;
                FrozenColumnCount++;
            }
        }
        public bool IsFrozenColumn(DataGridColumn column)
        {
            if (Columns != null && Columns.Contains(column))
            {
                return column.DisplayIndex < FrozenColumnCount;
            }
            else
            {
                return false;
            }
        }
        public void UnFreezeColumn(DataGridColumn column)
        {
            if (FrozenColumnCount > 0 && column.IsFrozen && Columns != null && Columns.Contains(column))
            {
                FrozenColumnCount--;
                column.DisplayIndex = FrozenColumnCount;
            }
        }

        public void UnFreezeAllColumns()
        {
            for (int i = Columns.Count - 1; i >= 0; i--)
                UnFreezeColumn(Columns[i]);
        }

        #endregion Freezing

        public void ShowFilter(DataGridColumn column, Visibility visibility)
        {
            var ctrl = Filters.Where(i => i.FilterColumnInfo.Column == column).FirstOrDefault();
            if (ctrl != null)
                ctrl.FilterVisibility = visibility;
        }

        public void ConfigureFilter(DataGridColumn column, bool canUserSelectDistinct, bool canUserGroup, bool canUserFreeze, bool canUserFilter)
        {
            column.SetValue(ColumnConfiguration.CanUserFilterProperty, canUserFilter);
            column.SetValue(ColumnConfiguration.CanUserFreezeProperty, canUserFreeze);
            column.SetValue(ColumnConfiguration.CanUserGroupProperty, canUserGroup);
            column.SetValue(ColumnConfiguration.CanUserSelectDistinctProperty, canUserSelectDistinct);

            var ctrl = Filters.Where(i => i.FilterColumnInfo.Column == column).FirstOrDefault();
            if (ctrl != null)
            {
                ctrl.CanUserSelectDistinct = canUserSelectDistinct;
                ctrl.CanUserFilter = canUserFilter;
            }
        }

        public void ResetDistinctLists()
        {
            foreach (var optionControl in Filters)
                optionControl.ResetDistinctList();
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}

