using System;
using System.Windows;
using Caliburn.Micro;
using OurMoneyWpf.Common;
using OurMoneyWpf.Common.Dialogs;
using OurMoneyWpf.DataProvider.Entities;

namespace OurMoneyWpf.Modules.Input.Common.Journa
{
    public class JournalViewModel : PropertyChangedBase, IHandle<Event>
    {
        private readonly IDataService dataService;
        private IEventAggregator eventAggregator;
        private IWindowManager windowManager;
        private DateFilterViewModel filter;

        public BindableCollection<DataForView> GridData { get; set; } = new BindableCollection<DataForView>();
        public DateTime CurrentDate { get; set; } = DateTime.Today;

        public JournalViewModel(IDataService dataService, IEventAggregator eventAggregator, IWindowManager windowManager)
        {
            this.dataService = dataService;
            this.eventAggregator = eventAggregator;
            this.windowManager = windowManager;
            this.eventAggregator.Subscribe(this);
            GetEvents(DateTime.Today, DateTime.Today); ;

        }

        public void Handle(Event msg)
        {
            if (filter != null)
                GetEvents(filter.FromDate, filter.ToDate);
            else
            {
                GetEvents(msg.Date, msg.Date);
                CurrentDate = msg.Date;
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

        public void RefreshGrid()
        {
            GridData.Clear();
            //           GetEvents(DateTime.Today, DateTime.Today); ;
        }

        public void FilterReplay()
        {

             filter = new DateFilterViewModel(CurrentDate);

            if (windowManager.ShowDialog(filter, null, null) == true)
            {
                GetEvents(filter.FromDate, filter.ToDate);
            }
        }

        public void SelectEvent()
        {
            if (SelectedEvent != null)
              eventAggregator.PublishOnUIThread(SelectedEvent.Id); // сообщение в форму
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
