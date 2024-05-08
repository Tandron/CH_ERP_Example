namespace CH_WpfControls.CH_DataGrid.Enums
{
    public class FilterEnum
    {
        public enum FilterOperation
        {
            Unknown,
            Contains,
            Equals,
            StartsWith,
            EndsWith,
            GreaterThanEqual,
            LessThanEqual,
            GreaterThan,
            LessThan,
            NotEquals,
            Today,
            Yesterday,
            LastXDays,
            LastXWeeks,
            LastXMonths
        }

        public enum ColumnOption
        {
            Unknown = 0,
            AddGrouping,
            RemoveGrouping,
            PinColumn,
            UnpinColumn
        }
    }
}
