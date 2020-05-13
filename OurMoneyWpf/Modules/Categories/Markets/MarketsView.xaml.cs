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

namespace OurMoneyWpf.Modules.Categories.Markets
{
    /// <summary>
    /// Логика взаимодействия для MarketsView.xaml
    /// </summary>
    public partial class MarketsView : UserControl
    {
        public MarketsView()
        {
            InitializeComponent();
        }

        private void Markets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = e;
        }
    }
}
