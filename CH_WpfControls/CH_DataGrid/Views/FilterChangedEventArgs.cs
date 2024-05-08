namespace CH_WpfControls.CH_DataGrid.Views
{
    public class FilterChangedEventArgs : EventArgs
    {
        public Predicate<object> Filter { get; set; }

        public FilterChangedEventArgs(Predicate<object> p)
        {
            Filter = p;
        }
    }
}
