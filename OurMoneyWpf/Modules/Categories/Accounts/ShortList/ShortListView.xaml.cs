using System.Windows.Controls;

namespace OurMoneyWpf.Modules.Categories.Accounts.ShortList
{
    /// <summary>
    /// Логика взаимодействия для ShortListView.xaml
    /// </summary>
    public partial class ShortListView : UserControl
    {
        public ShortListView()
        {
            InitializeComponent();
        }

        private void Accounts_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
        }

        private void Accounts_Sorting(object sender, DataGridSortingEventArgs e)
        {
            var a = e;
        }
    }
}