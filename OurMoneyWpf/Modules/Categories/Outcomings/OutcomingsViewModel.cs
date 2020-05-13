using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using OurMoneyWpf.Common;
using OurMoneyWpf.DataProvider.Entities;
using OurMoneyWpf.DataProvider.Repository;

namespace OurMoneyWpf.Modules.Categories.Outcomings
{
    public class OutcomingsViewModel : Screen
    {
        private int currentParentId;
        private readonly IDataService outcomings;
        private readonly IEventAggregator eventAggregator;
        public BindableCollection<Dictionary> FoodCategories { get; set; } = new BindableCollection<Dictionary>();
        public BindableCollection<Dictionary> SubCategories { get; set; } = new BindableCollection<Dictionary>();
        public BindableCollection<Dictionary> OtherCategories { get; set; } = new BindableCollection<Dictionary>();

        public OutcomingsViewModel(IDataService outcomings, IEventAggregator eventAggregator)
        {
            this.outcomings = outcomings;
            this.eventAggregator = eventAggregator;

            GetCategories();
            if (FoodCategories.Count > 0)
            {
                SelectedCategory = FoodCategories[0];
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
        public void AddOrEditFood(DataGridRowEditEndingEventArgs outcomingItem)
        {

            AddOrEditCategory((Dictionary)outcomingItem.Row.DataContext, (int)RootParentId.Food);
        }
        public void AddOrEditOther(DataGridRowEditEndingEventArgs outcomingItem)
        {

            AddOrEditCategory((Dictionary)outcomingItem.Row.DataContext, (int)RootParentId.Other);
        }
        private void AddOrEditCategory(Dictionary outcoming, int parentId)
        {
            if (outcoming.Name != null)
            {
                if (outcoming.Id == 0)
                {
                    outcoming.EntityType = (int)EntityTypes.Categories;
                    outcoming.ParentId = parentId;
                    outcomings.AddDictItem(outcoming);
                    RefreshGrid();
                }
                else
                {
                    outcomings.UpdateDictItem(outcoming);
                }
                MessageArg message = new MessageArg
                {
                    Sender = MessageSenders.OutcomingsPage,
                    Message = outcoming
                };

                eventAggregator.PublishOnUIThread(message); // сообщение для формы

            }
        }
        public void AddOrEditSubcategory(DataGridRowEditEndingEventArgs outcomingItem)
        {
            var outcoming = (Dictionary)outcomingItem.Row.DataContext;
            if (outcoming.Name != null && currentParentId > 0)
            {
                if (outcoming.Id == 0)
                {
                    outcoming.EntityType = (int)EntityTypes.Categories;
                    outcoming.ParentId = currentParentId;
                    outcomings.AddDictItem(outcoming);
                    RefreshGrid();
                    GetSubCategories(currentParentId);
                }
                else
                {
                    outcomings.UpdateDictItem(outcoming);
                }
                MessageArg message = new MessageArg
                {
                    Sender = MessageSenders.OutcomingsPage,
                    Message = outcoming
                };

                eventAggregator.PublishOnUIThread(message); // сообщение для формы
            }
        }

        public void DeleteFood(Dictionary outcomingItem)
        {
            if (outcomingItem == null) return;
            var result = MessageBox.Show("Категория расхода " + outcomingItem.Name + " будет удалена навсегда",
                "Вы уверены?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                outcomings.DeleteDictItem(outcomingItem);
                //                FoodCategories.Remove(outcomingItem);
                RefreshGrid();
                MessageArg message = new MessageArg
                {
                    Sender = MessageSenders.OutcomingsPage,
                    Message = outcomingItem
                };

                eventAggregator.PublishOnUIThread(message); // сообщение для формы
            }
        }
        public void DeleteOther(Dictionary outcomingItem)
        {
            if (outcomingItem == null) return;
            var result = MessageBox.Show("Категория расхода " + outcomingItem.Name + " будет удалена навсегда",
                "Вы уверены?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                outcomings.DeleteDictItem(outcomingItem);
                //                OtherCategories.Remove(outcomingItem);
                RefreshGrid();
                MessageArg message = new MessageArg
                {
                    Sender = MessageSenders.OutcomingsPage,
                    Message = outcomingItem
                };

                eventAggregator.PublishOnUIThread(message); // сообщение для формы
            }
        }
        public void DeleteSubcategory(Dictionary outcomingItem)
        {
            if (outcomingItem == null) return;
            var result = MessageBox.Show("Вид расхода " + outcomingItem.Name + " будет удален навсегда",
                "Вы уверены?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                outcomings.DeleteDictItem(outcomingItem);
                SubCategories.Remove(outcomingItem);
                RefreshGrid();
                MessageArg message = new MessageArg
                {
                    Sender = MessageSenders.OutcomingsPage,
                    Message = outcomingItem
                };

                eventAggregator.PublishOnUIThread(message); // сообщение для формы
            }
        }
        public bool CanDeleteFood(Dictionary item)
        {
            return CanDelete(item);
        }
        public bool CanDeleteOther(Dictionary item)
        {
            return CanDelete(item);
        }

        private bool CanDelete(Dictionary item)
        {

            if (item == null) return false;
            var items = outcomings.GetCategories().Where(x => x.ParentId == item.Id);
            return items.ToList().Count <= 0;
        }
        public bool CanDeleteSubcategory(Dictionary item)
        {
            return item != null;
        }

        public void RefreshGrid()
        {
            FoodCategories.Clear();
            OtherCategories.Clear();
            GetCategories();
        }

        #endregion Комманды

        private void GetCategories()
        {
            var items = outcomings.GetCategories();

            foreach (var item in items)
            {
                if (item.ParentId == (int)RootParentId.Food) FoodCategories.Add(item);
                if (item.ParentId == (int)RootParentId.Other) OtherCategories.Add(item);
            }

        }

        private void GetSubCategories(int parentId)
        {
            SubCategories.Clear();
            var items = outcomings.GetCategories().Where(x => x.ParentId == parentId);
            foreach (var item in items)
            {
                SubCategories.Add(item);
            }
        }

    }
}
