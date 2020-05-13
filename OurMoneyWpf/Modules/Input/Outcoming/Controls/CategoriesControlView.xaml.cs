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
using OurMoneyWpf.Common;

namespace OurMoneyWpf.Modules.Input.Outcoming.Controls
{
    public partial class CategoriesControlView : UserControl
    {
        private readonly IEventAggregator eventAggregator;

        public CategoriesControlView()
        {
            InitializeComponent();
 //           this.eventAggregator = eventAggregator;
            this.Loaded += ListBoxBehindView_Loaded;
            
        }
        private void ListBoxBehindView_Loaded(object sender, RoutedEventArgs e)
        {
            var foodCount = ((CategoriesControlViewModel) DataContext).ListFoodData.Count;
            var otherCount = ((CategoriesControlViewModel) DataContext).ListOtherData.Count;

            #region Построение подкатегорий "Еда, питье"

            for (int i = 0; i < foodCount; i++)
            {
                #region Подключаем вертикальную StacPanel
                StackPanel CategoriesPanel = new StackPanel();
                CategoriesPanel.Orientation = Orientation.Vertical;
                FoodPanel.Children.Add(CategoriesPanel);
                Message.AttachProperty.
                #endregion

                #region Строим элементы
                var header = new TextBlock
                {
                    FontSize = 16,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(4, 0, 4, 8)
                };
                var lbBox = new ListBox
                {
                    DisplayMemberPath = "Name",
                    FontSize = 14
                };
                

                lbBox.MouseDoubleClick += ListBox_MouseDoubleClick;
                #endregion

                #region Определяем привязки
                var headerBinding = new Binding("ListFoodData[" + i + "].Header");
                headerBinding.Source = DataContext;
                header.SetBinding(TextBlock.TextProperty, headerBinding);

                var itemsBinding = new Binding("ListFoodData[" + i + "].Categories");
                itemsBinding.Source = DataContext;
                lbBox.SetBinding(ListBox.ItemsSourceProperty, itemsBinding);
                //var selectedBinding = new Binding("SelectedCategory");
                //selectedBinding.Source = DataContext;
                //lbBox.SetBinding(ListBox.SelectedItemProperty, selectedBinding);
                #endregion

                CategoriesPanel.Children.Add(header);
                CategoriesPanel.Children.Add(lbBox);
            }
            #endregion
            #region Построение подкатегорий "Остальное"

            for (int i = 0; i < otherCount; i++)
            {
                #region Подключаем вертикальную StacPanel
                StackPanel CategoriesPanel = new StackPanel();
                CategoriesPanel.Orientation = Orientation.Vertical;
                OtherPanel.Children.Add(CategoriesPanel);
                #endregion

                #region Строим элементы
                var header = new TextBlock
                {
                    FontSize = 16,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(4, 0, 4, 8)
                };
                var lbBox = new ListBox
                {
                    DisplayMemberPath = "Name",
                    FontSize = 14
                };

                lbBox.MouseDoubleClick += ListBox_MouseDoubleClick;
                #endregion

                #region Определяем привязки
                var headerBinding = new Binding("ListOtherData[" + i + "].Header");
                headerBinding.Source = DataContext;
                header.SetBinding(TextBlock.TextProperty, headerBinding);

                var itemsBinding = new Binding("ListOtherData[" + i + "].Categories");
                itemsBinding.Source = DataContext;
                lbBox.SetBinding(ListBox.ItemsSourceProperty, itemsBinding);
                //var selectedBinding = new Binding("SelectedCategory");
                //selectedBinding.Source = DataContext;
                //lbBox.SetBinding(ListBox.SelectedItemProperty, selectedBinding);
                #endregion

                CategoriesPanel.Children.Add(header);
                CategoriesPanel.Children.Add(lbBox);
            }
            #endregion

        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var a = sender;
            //MessageArg message = new MessageArg
            //{
            //    Sender = MessageSenders.OutcomingCategories,
            //    Message = sender
            //};
//            eventAggregator.PublishOnUIThread(message);
        }
    }
}
