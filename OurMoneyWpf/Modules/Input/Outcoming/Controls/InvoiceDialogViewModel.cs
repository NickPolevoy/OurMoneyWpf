using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using OurMoneyWpf.Common;
using OurMoneyWpf.DataProvider.Entities;

namespace OurMoneyWpf.Modules.Input.Outcoming.Controls
{
    public class InvoiceDialogViewModel : Screen
    {
        public Invoice Invoice { get; set; }
        private IDataService dataService;
        public BindableCollection<Dictionary> Accounts { get; set; } = new BindableCollection<Dictionary>();
        public BindableCollection<Dictionary> Markets { get; set; } = new BindableCollection<Dictionary>();

        public InvoiceDialogViewModel(Invoice invoice, IDataService dataService)
        {
            Invoice = invoice;
            this.dataService = dataService;
            if (invoice == null)
            {
                CurrentDate = DateTime.Today;
                Invoice = new Invoice();
            }
            GetAccounts();
            GetMarkets();
        }
        #region Свойства

        private DateTime currentDate;
        public DateTime CurrentDate
        {
            get => currentDate;
            set
            {
                Set(ref currentDate, value);
                NotifyOfPropertyChange(() => CurrentDate);
            }
        }

        private Dictionary selectedMarket;
        public Dictionary SelectedMarket
        {
            get => selectedMarket;
            set
            {
                Set(ref selectedMarket, value);
                NotifyOfPropertyChange(() => SelectedMarket);
            }
        }

        private Dictionary selectedAccount;
        public Dictionary SelectedAccount
        {
            get => selectedAccount;
            set
            {
                Set(ref selectedAccount, value);
                NotifyOfPropertyChange(() => SelectedAccount);
            }
        }

        private Decimal totalAmount;
        public Decimal TotalAmount
        {
            get => totalAmount;
            set
            {
                Set(ref totalAmount, value);
                NotifyOfPropertyChange(() => TotalAmount);
            }
        }

        private decimal totalDiscount;
        public decimal TotalDiscount
        {
            get => totalDiscount;
            set
            {
                Set(ref totalDiscount, value);
                NotifyOfPropertyChange(() => TotalDiscount);
            }
        }

        private decimal incomingPoints;
        public decimal IncomingPoints
        {
            get => incomingPoints;
            set
            {
                Set(ref incomingPoints, value);
                NotifyOfPropertyChange(() => IncomingPoints);
            }
        }

        private decimal outcomingPoints;
        public decimal OutcomingPoints
        {
            get => outcomingPoints;
            set
            {
                Set(ref outcomingPoints, value);
                NotifyOfPropertyChange(() => OutcomingPoints);
            }
        }

        #endregion
        private void GetMarkets()
        {
            Markets.Clear();
            var items = dataService.GetMarkets();
            foreach (var a in items)
            {
                Markets.Add(a);
            }
        }
        private void GetAccounts()
        {
            Accounts.Clear();
            var items = dataService.GetAccounts();
            foreach (var a in items)
            {
                Accounts.Add(a);
            }
        }

        public void Ok()
        {
            if (SelectedAccount != null && SelectedMarket != null && TotalAmount > 0)
            {
                Invoice.AccountId = SelectedAccount.Id;
                Invoice.MarketId = SelectedMarket.Id;
                Invoice.Amount = TotalAmount;
                Invoice.Discount = TotalDiscount;
                Invoice.Date = CurrentDate;

                if (Invoice.Id == 0)
                    dataService.AddInvoice(Invoice);
                else
                    dataService.UpdateInvoice(Invoice);

                TryClose(true);
            }
        }

        public void Cancel()
        {
            TryClose(false);
        }

    }
}
