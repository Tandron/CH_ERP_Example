namespace CH_WpfControls.CH_DataGrid.Views
{
    public class CancelableFilterChangedEventArgs : EventArgs
    {
        public bool Cancel { get; set; }
        public Predicate<object> NewFilter { get; set; }
        public CancelableFilterChangedEventArgs(Predicate<object> p)
        {
            NewFilter = p;
        }
    }
}
