using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using Caliburn.Micro;
using OurMoneyWpf.Common;
using OurMoneyWpf.Common.Dialogs;
using OurMoneyWpf.DataProvider.Entities;

namespace OurMoneyWpf.Modules.Input.Incoming.Controls
{
    public class IncomingJournalViewModel : PropertyChangedBase, IHandle<MessageArg>
    {
        private readonly IDataService dataService;
        private IEventAggregator eventAggregator;
        private IWindowManager windowManager;
        private DateFilterViewModel filter;

        public BindableCollection<DataForView> GridData { get; set; } = new BindableCollection<DataForView>();
        public DateTime CurrentDate { get; set; } = DateTime.Today;

        public IncomingJournalViewModel(IDataService dataService, IEventAggregator eventAggregator,
            IWindowManager windowManager)
        {
            this.dataService = dataService;
            this.eventAggregator = eventAggregator;
            this.windowManager = windowManager;
            this.eventAggregator.Subscribe(this);
            GetEvents(DateTime.Today, DateTime.Today);
        }

        public void Handle(MessageArg msg)
        {
            var inputEvent = msg.Message as Event;
            if (msg.Sender == MessageSenders.IncomingForm && inputEvent != null)
            {
                if (filter != null)
                    GetEvents(filter.FromDate, filter.ToDate);
                else
                {
                    GetEvents(inputEvent.Date, inputEvent.Date);
                    CurrentDate = inputEvent.Date;
                }
            }
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
        private DataForView selectedEvent;
        public DataForView SelectedEvent
        {
            get => selectedEvent;
            set
            {
                Set(ref selectedEvent, value);
                NotifyOfPropertyChange(() => SelectedEvent);
            }
        }


        #region Комманды

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridEventItem"></param>


        public void Delete(DataForView incomingEventItem)
        {
            if (incomingEventItem == null) return;
            var result = MessageBox.Show("Этот доход " + incomingEventItem.Amount + " будет удален навсегда",
                "Вы уверены?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                dataService.DeleteEvent(incomingEventItem.Id);
                GridData.Remove(incomingEventItem);
            }
        }

        public bool CanDelete(Event item)
        {
            return item != null;
        }


        public void FilterReplay()
        {

            filter = new DateFilterViewModel(CurrentDate);

            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settings.WindowStyle = WindowStyle.None;

            if (windowManager.ShowDialog(filter, null, settings) == true)
            {
                GetEvents(filter.FromDate, filter.ToDate);
            }
        }

        public void SelectEvent()
        {
            if (SelectedEvent == null) return;

            MessageArg message = new MessageArg
            {
                Message = SelectedEvent.Id,
                Sender = MessageSenders.IncomingJournal
            };
            eventAggregator.PublishOnUIThread(message); // сообщение в форму
        }

        #endregion Комманды


        private void GetEvents(DateTime fromDate, DateTime toDate)
        {
            GridData.Clear();
            TotalSum = 0;
            var items = dataService.GetIncomingEvents(fromDate, toDate);
            foreach (var a in items)
            {
                var item = new DataForView
                {
                    Id = a.Id,
                    Date = a.Date,
                    Amount = a.Amount,
                    AccountName = dataService.GetDictItem(a.AccountId).Name,
                    SourceName = dataService.GetDictItem(a.CategoryId).Name,
                    Comment = a.Comment
                };
                GridData.Add(item);
                TotalSum += a.Amount;
            }
        }

        public class DataForView
        {
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public string SourceName { get; set; }
            public string AccountName { get; set; }
            public decimal Amount { get; set; }
            public string Comment { get; set; }
        }
    }
}
