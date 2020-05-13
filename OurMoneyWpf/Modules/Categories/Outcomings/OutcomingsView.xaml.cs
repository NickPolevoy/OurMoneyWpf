using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OurMoneyWpf.Modules.Categories.Outcomings
{
    /// <summary>
    /// Логика взаимодействия для OutcomingsView.xaml
    /// </summary>
    public partial class OutcomingsView : UserControl
    {
        public OutcomingsView()
        {
            InitializeComponent();
        }

        private void FoodCategories_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var a = FoodCategories.SelectedCells;
        }
    }
}
