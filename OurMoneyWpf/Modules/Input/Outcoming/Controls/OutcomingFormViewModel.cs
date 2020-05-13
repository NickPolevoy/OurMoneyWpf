using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using OurMoneyWpf.Common;
using OurMoneyWpf.Common.Dialogs;

namespace OurMoneyWpf.Modules.Input.Outcoming.Controls
{
    public class OutcomingFormViewModel : PropertyChangedBase
    {
        private readonly IDataService dataService;
        private IEventAggregator eventAggregator;
        private readonly IWindowManager windowManager;
        private InvoiceDialogViewModel invoice;
        public OutcomingFormViewModel(IDataService dataService,
            IEventAggregator eventAggregator,
            IWindowManager windowManager)
        {
            this.dataService = dataService;
            this.eventAggregator = eventAggregator;
            this.windowManager = windowManager;

        }

        #region Свойства

        private string marketName;
        public string MarketName
        {
            get => marketName;
            set
            {
                Set(ref marketName, value);
                NotifyOfPropertyChange(() => MarketName);
            }
        }

        private string accountName;
        public string AccountName
        {
            get => accountName;
            set
            {
                Set(ref accountName, value);
                NotifyOfPropertyChange(() => AccountName);
            }
        }

        private string categoryName;
        public string CategoryName
        {
            get => categoryName;
            set
            {
                Set(ref categoryName, value);
                NotifyOfPropertyChange(() => CategoryName);
            }
        }

        private string actualDate;
        public string ActualDate
        {
            get => actualDate;
            set
            {
                Set(ref actualDate, value);
                NotifyOfPropertyChange(() => ActualDate);
            }
        }

        #endregion
        #region Комманды
        public void OpenInvoice()
        {
            var invoiceDlg = new InvoiceDialogViewModel(null, dataService);

            // Установка параметров окна диалога
            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settings.WindowStyle = WindowStyle.None;

            if (windowManager.ShowDialog(invoiceDlg, null, settings) == true)
            {
                MarketName = dataService.GetDictItem(invoiceDlg.Invoice.MarketId).Name;
                AccountName = dataService.GetDictItem(invoiceDlg.Invoice.AccountId).Name;
                ActualDate = GetStringDate(invoiceDlg.CurrentDate);
            }
        }
        #endregion

        #region Вспомогательные методы

        private string GetStringDate(DateTime date)
        {
            if (date == DateTime.Today) return "cегоня";
            if (date == DateTime.Today.AddDays(-1)) return "вчера";
            if (date == DateTime.Today.AddDays(-2)) return "позавчера";
            return date.ToShortDateString();
        }

        #endregion
    }

}
