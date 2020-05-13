using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using OurMoneyWpf.Common;
using OurMoneyWpf.DataProvider.Entities;
using OurMoneyWpf.DataProvider.Repository;

namespace OurMoneyWpf.Modules.Categories.Debts
{
    public class DebtsViewModel
    {
        private readonly IEventAggregator eventAggregator;
        private readonly IDataService debts;
        public BindableCollection<Dictionary> Debts { get; set; } = new BindableCollection<Dictionary>();

        public DebtsViewModel(IDataService debts, IEventAggregator eventAggregator)
        {
            this.debts = debts;
            this.eventAggregator = eventAggregator;
            GetAllItems();
        }

        #region Комманды

        public void AddOrEdit(DataGridRowEditEndingEventArgs debtItem)
        {
            var debt = (Dictionary)debtItem.Row.DataContext;
            if (debt.Name != null)
            {
                if (debt.Id == 0)
                {
                    debt.EntityType = (int)EntityTypes.Debts;
                    debts.AddDictItem(debt);
                }
                else
                {
                    debts.UpdateDictItem(debt);
                }
                MessageArg message = new MessageArg
                {
                    Sender = MessageSenders.DebtsPage,
                    Message = debt
                };

                eventAggregator.PublishOnUIThread(message); // сообщение для формы
            }
        }

        public void Delete(Dictionary debtItem)
        {
            if (debtItem == null) return;
            var result = MessageBox.Show("Долговое место " + debtItem.Name + " будет удалено навсегда",
                "Вы уверены?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                debts.DeleteDictItem(debtItem);
                Debts.Remove(debtItem);
                MessageArg message = new MessageArg
                {
                    Sender = MessageSenders.DebtsPage,
                    Message = debtItem
                };

                eventAggregator.PublishOnUIThread(message); // сообщение для формы
            }
        }
        public bool CanDelete(Dictionary item)
        {
            return item != null;
        }

        public void RefreshGrid()
        {
            Debts.Clear();
            GetAllItems();
        }

        #endregion Комманды

        private void GetAllItems()
        {
            var items = debts.GetDebts();
            foreach (var a in items)
            {
                Debts.Add(a);
            }
        }
    }
}
