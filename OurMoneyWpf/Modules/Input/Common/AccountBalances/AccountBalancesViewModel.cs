using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using OurMoneyWpf.Common;
using OurMoneyWpf.DataProvider.Entities;

namespace OurMoneyWpf.Modules.Input.Common.AccountBalances
{
    public class AccountBalancesViewModel : PropertyChangedBase, IHandle<bool>
    {
        private readonly IDataService accounts;
        private IEventAggregator eventAggregator;
        public BindableCollection<Dictionary> AccountsData { get; set; } = new BindableCollection<Dictionary>();
        public AccountBalancesViewModel(IDataService accounts, IEventAggregator eventAggregator)
        {
            this.accounts = accounts;
            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);
            RefreshGrid();
        }

        private decimal totalSum;
        public decimal TotalSum
        {
            get => totalSum;
            set
            {
                Set(ref totalSum, value);
                NotifyOfPropertyChange(() => TotalSum);
            }
        }
        public void Handle(bool yes)
        {
            RefreshGrid();
        }
        public void RefreshGrid()
        {
            AccountsData.Clear();
            TotalSum = 0;
            GetAllItems();
        }

        private void GetAllItems()
        {
            var items = accounts.GetAccounts();
            foreach (var a in items)
            {
                AccountsData.Add(a);
                TotalSum += a.Amount;
            }
        }

    }
}
