using System.Windows;
using System.Windows.Controls;

namespace CH_WpfControls.CH_DataGrid.Views
{
    /// <summary>
    /// Interaktionslogik für CH_GridColumn.xaml
    /// </summary>
    public partial class CH_GridColumn : DataGridBoundColumn
    {
        public CH_GridColumn()
        {
            InitializeComponent();
        }

        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            throw new NotImplementedException();
        }

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            throw new NotImplementedException();
        }
    }
}
