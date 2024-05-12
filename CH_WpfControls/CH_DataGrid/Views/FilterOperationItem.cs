using CH_WpfControls.CH_DataGrid.Enums;

namespace CH_WpfControls.CH_DataGrid.Views
{
    public class FilterOperationItem(FilterEnum.FilterOperation operation, string description, string imagePath, bool needsFilterValue = true)
    {
        public FilterEnum.FilterOperation FilterOption { get; set; } = operation;
        public string ImagePath { get; set; } = imagePath;
        public string Description { get; set; } = description;
        public bool NeedsFilterValue { get; set; } = needsFilterValue;

        public override string ToString()
        {
            return Description;
        }
    }
}
