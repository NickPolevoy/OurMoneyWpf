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
using Caliburn.Micro;


namespace OurMoneyWpf.Modules.ForTesting.ListBoxTest
{
    /// <summary>
    /// Логика взаимодействия для ListBoxBehindView.xaml
    /// </summary>
    public partial class ListBoxBehindView : UserControl
    {
        public ListBoxBehindView()
        {
            InitializeComponent();
            var a = DataContext;
            this.Loaded += ListBoxBehindView_Loaded;
        }

        private void ListBoxBehindView_Loaded(object sender, RoutedEventArgs e)
        {

            Panel1.Orientation = Orientation.Horizontal;
            for (int i = 0; i < 3; i++)
            {
                ListBox lbBox = new ListBox();
                lbBox.MouseDoubleClick += ListBox_MouseDoubleClick;
                lbBox.DisplayMemberPath = "Name";
                var binding = new Binding("ListData[" + i + "].Sources");
                binding.Source = DataContext;
                lbBox.SetBinding(ListBox.ItemsSourceProperty, binding);
                Panel1.Children.Add(lbBox);
                TextBlock tbm = new TextBlock {Text = "test" + i};
                Panel1.Children.Add(tbm);
            }

        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var a = sender;
        }
    }

    public class Items
    {
        public IList<string> ItemsCollection { get; set; } = new List<string>();
        public Items()
        {
            for (int i = 0; i < 5; i++)
            {
                ItemsCollection.Add("Item" + i);
            }
        }

        public IList<string> GetItems()
        {
            return ItemsCollection;
        }
    }
}
