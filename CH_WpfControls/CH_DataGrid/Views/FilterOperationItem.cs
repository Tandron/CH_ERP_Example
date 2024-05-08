using CH_WpfControls.CH_DataGrid.Enums;

namespace CH_WpfControls.CH_DataGrid.Views
{
    public class FilterOperationItem
    {
        public FilterEnum.FilterOperation FilterOption { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public bool NeedsFilterValue { get; set; }

        public FilterOperationItem(FilterEnum.FilterOperation operation, string description, string imagePath, bool needsFilterValue = true)
        {
            FilterOption = operation;
            Description = description;
            ImagePath = imagePath;
            NeedsFilterValue = needsFilterValue;
        }
        public override string ToString()
        {
            return Description;
        }
    }
}
