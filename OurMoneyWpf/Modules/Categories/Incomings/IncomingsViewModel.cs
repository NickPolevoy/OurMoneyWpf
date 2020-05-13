using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using OurMoneyWpf.Common;
using OurMoneyWpf.DataProvider.Entities;
using OurMoneyWpf.DataProvider.Repository;

namespace OurMoneyWpf.Modules.Categories.Incomings
{
    public class IncomingsViewModel : Screen
    {
        private readonly IDataService incomings;
        private readonly IEventAggregator eventAggregator;
        private int currentParentId;
        public BindableCollection<Dictionary> SourceCategories { get; set; } = new BindableCollection<Dictionary>();
        public BindableCollection<Dictionary> SubCategories { get; set; } = new BindableCollection<Dictionary>();


        public IncomingsViewModel(IDataService incomings, IEventAggregator eventAggregator)
        {
            this.incomings = incomings;
            this.eventAggregator = eventAggregator;
            GetCategories();
            if (SourceCategories.Count > 0)
            {
                SelectedCategory = SourceCategories[0];
                OpenSubItems();
            }
        }

        #region Свойства

        private Dictionary selectedCategory;

        public Dictionary SelectedCategory
        {
            get => selectedCategory;
            set
            {
                Set(ref selectedCategory, value);
                NotifyOfPropertyChange(() => SelectedCategory);
            }
        }

        #endregion

        #region Комманды

        public void OpenSubItems()
        {
            if (SelectedCategory == null) return;
            if (SelectedCategory.Name == null) return;

            GetSubCategories(SelectedCategory.Id);
            currentParentId = SelectedCategory.Id;
        }

        public void AddOrEditCategory(DataGridRowEditEndingEventArgs incomingItem)
        {

            var incoming = (Dictionary)incomingItem.Row.DataContext;

            if (incoming.Name != null)
            {
                if (incoming.Id == 0)
                {
                    incoming.EntityType = (int)EntityTypes.Sources;
                    incoming.ParentId = (int)RootParentId.Source;
                    incomings.AddDictItem(incoming);

                                           RefreshGrid();
                    //                       SourceCategories.Add(new Dictionary());
                }
                else
                {
                    incomings.UpdateDictItem(incoming);
                }
                MessageArg message = new MessageArg
                {
                    Sender = MessageSenders.IncomingsPage,
                    Message = incoming
                };

                eventAggregator.PublishOnUIThread(message); // сообщение для формы
            }
        }

        public void AddOrEditSubcategory(DataGridRowEditEndingEventArgs incomingItem)
        {
            var incoming = (Dictionary)incomingItem.Row.DataContext;
            if (incoming.Name != null && currentParentId > 0)
            {
                if (incoming.Id == 0)
                {
                    incoming.EntityType = (int)EntityTypes.Sources;
                    incoming.ParentId = currentParentId;
                    incomings.AddDictItem(incoming);
                    //                      SubCategories.Add(incoming);

                                            RefreshGrid();
                                            GetSubCategories(currentParentId);
                }
                else
                {
                    incomings.UpdateDictItem(incoming);
                }
                MessageArg message = new MessageArg
                {
                    Sender = MessageSenders.IncomingsPage,
                    Message = incoming
                };

                eventAggregator.PublishOnUIThread(message); // сообщение для формы
            }
        }

        public void DeleteCategory(Dictionary incomingItem)
        {
            if (incomingItem == null) return;
            var result = MessageBox.Show("Категория расхода " + incomingItem.Name + " будет удалена навсегда",
                "Вы уверены?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                incomings.DeleteDictItem(incomingItem);
                //SourceCategories.Remove(incomingItem);
                RefreshGrid();
                MessageArg message = new MessageArg
                {
                    Sender = MessageSenders.IncomingsPage,
                    Message = incomingItem
                };

                eventAggregator.PublishOnUIThread(message); // сообщение для формы
            }
        }

        public void DeleteSubcategory(Dictionary incomingItem)
        {
            if (incomingItem == null) return;
            var result = MessageBox.Show("Вид расхода " + incomingItem.Name + " будет удален навсегда",
                "Вы уверены?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                incomings.DeleteDictItem(incomingItem);
                SubCategories.Remove(incomingItem);
                RefreshGrid();
                MessageArg message = new MessageArg
                {
                    Sender = MessageSenders.IncomingsPage,
                    Message = incomingItem
                };

                eventAggregator.PublishOnUIThread(message); // сообщение для формы
            }
        }

        public bool CanDeleteCategory(Dictionary item)
        {

            if (item == null) return false;
            var items = incomings.GetSources().Where(x => x.ParentId == item.Id);
            return items.ToList().Count <= 0;
        }

        public bool CanDeleteSubcategory(Dictionary item)
        {
            return item != null;
        }

        public void RefreshGrid()
        {
            SourceCategories.Clear();
            GetCategories();
        }

        #endregion Комманды

        private void GetCategories()
        {
            var items = incomings.GetSources();

            foreach (var item in items)
            {
                if (item.ParentId == (int)RootParentId.Source) SourceCategories.Add(item);
            }

        }

        private void GetSubCategories(int parentId)
        {
            SubCategories.Clear();
            var items = incomings.GetSources().Where(x => x.ParentId == parentId);
            foreach (var item in items)
            {
                SubCategories.Add(item);
            }

            //           RefreshGrid();
        }
    }
}
