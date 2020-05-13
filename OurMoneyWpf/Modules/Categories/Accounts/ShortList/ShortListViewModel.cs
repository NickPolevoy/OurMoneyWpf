using Caliburn.Micro;
using OurMoneyWpf.Common;
using OurMoneyWpf.DataProvider.Entities;
using OurMoneyWpf.DataProvider.Repository;
using System.Windows;
using System.Windows.Controls;

namespace OurMoneyWpf.Modules.Categories.Accounts.ShortList
{
    public class ShortListViewModel : PropertyChangedBase
    {
        private readonly IDataService accounts;
        private readonly IEventAggregator eventAggregator;
        public BindableCollection<Dictionary> Accounts { get; set; } = new BindableCollection<Dictionary>();

        public ShortListViewModel(IDataService accounts, IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.accounts = accounts;
            GetAllItems();
        }

        #region Комманды

        public void AddOrEdit(DataGridRowEditEndingEventArgs accountItem)
        {
            var account = (Dictionary)accountItem.Row.DataContext;
            if (account.Id == 0 && account.Name != null)
            {
                account.EntityType = (int)EntityTypes.Accounts;
                accounts.AddDictItem(account);
            }
            else
            {
                accounts.UpdateDictItem(account);
            }

            MessageArg message = new MessageArg()
            {
                Sender = MessageSenders.AccountsPage,
                Message = account
            };
            eventAggregator.PublishOnUIThread(message); // сообщение для форм ввода

        }

        public void Delete(Dictionary accountItem)
        {


            var result = MessageBox.Show("Кошелек " + accountItem.Name + " будет удален навсегда",
            "Вы уверены?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                accounts.DeleteDictItem(accountItem);
                Accounts.Remove(accountItem);
            }
            eventAggregator.PublishOnUIThread(accountItem); // сообщение для журнала

        }

        public bool CanDelete(Dictionary item)
        {
            return item != null;
        }

        public void RefreshGrid()
        {
            Accounts.Clear();
            GetAllItems();
        }

        #endregion Комманды

        private void GetAllItems()
        {
            var items = accounts.GetAccounts();
            foreach (var a in items)
            {
                Accounts.Add(a);
            }
        }
    }
}