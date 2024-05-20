namespace CH_WpfControls.CH_DataGrid.Views
{
    public class CheckboxComboItem
    {
        private bool _isChecked = false;

        public bool IsChecked
        {
            get => _isChecked;
            set => _isChecked = value;
        }

        private string _description = "";

        public string Description
        {
            get => _description;
            set => _description = value;
        }

        public override string ToString()  => Description;
    }
}
