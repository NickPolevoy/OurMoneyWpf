using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using OurMoneyWpf.Common;
using OurMoneyWpf.DataProvider.Entities;

namespace OurMoneyWpf.Modules.Input.Incoming.Controls
{
    public class IncomingFormViewModel : PropertyChangedBase, IHandle<MessageArg>
    {
        private readonly IDataService dataService;
        private readonly IEventAggregator eventAggregator;
        public BindableCollection<Dictionary> Accounts { get; set; } = new BindableCollection<Dictionary>();

        public IncomingFormViewModel(IDataService dataService, IEventAggregator eventAggregator)
        {
            this.dataService = dataService;
            this.eventAggregator = eventAggregator;
            eventAggregator.Subscribe(this);

            CurrentEvent = new Event();
            // SourceName = "Категория";
            CurrentDate = DateTime.Today;
            GetAccounts();
        }


        public void Handle(MessageArg msg)
        {
            switch (msg.Message)
            {
                case Dictionary message:
                {
                    var inputEvent = message;
                    switch (msg.Sender)
                    {
                        case MessageSenders.IncomingForm:
                        {
                            if (inputEvent.EntityType == (int) EntityTypes.Sources)
                            {
                                SelectedSource = inputEvent;
                                SourceName = FullSourceName(inputEvent);
                            }

                            break;
                        }
                        case MessageSenders.AccountsPage:
                            GetAccounts();
                            break;
                    }

                    break;
                }
                default:
                {
                    if (msg.Sender == MessageSenders.IncomingJournal && msg.Message is int id)
                    {
                        var item = dataService.GetEventById(id);
                        if (item.OperationType != (int)Operations.Income) return;

                        CurrentEvent = item;
                        CurrentDate = item.Date;
                        SourceName = dataService.GetDictItem(item.CategoryId).Name;
                        CurrentEventAmount = item.Amount;
                        SelectedAccount = dataService.GetDictItem(item.AccountId);
                    }

                    break;
                }
            }
        }

        private string FullSourceName(Dictionary source)
        {
            var item = dataService.GetDictItem(source.ParentId);
            return item.Name + " / " + source.Name;
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

        private Event currentEvent;
        public Event CurrentEvent
        {
            get => currentEvent;
            set
            {
                Set(ref currentEvent, value);
                NotifyOfPropertyChange(() => CurrentEvent);
            }
        }


        private decimal currentEventAmount;
        public decimal CurrentEventAmount
        {
            get => currentEventAmount;
            set
            {
                Set(ref currentEventAmount, value);
                NotifyOfPropertyChange(() => CurrentEventAmount);
                NotifyOfPropertyChange(() => CanSaveEvent);
            }
        }

        private string sourceName;
        public string SourceName
        {
            get => sourceName;
            set
            {
                Set(ref sourceName, value);
                NotifyOfPropertyChange(() => SourceName);
                NotifyOfPropertyChange(() => CanSaveEvent);
            }
        }

        private Dictionary selectedSource;
        public Dictionary SelectedSource
        {
            get => selectedSource;
            set
            {
                Set(ref selectedSource, value);
                NotifyOfPropertyChange(() => SelectedSource);
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
                NotifyOfPropertyChange(() => CanSaveEvent);
            }
        }
        #endregion

        #region Команды
        public void SaveEvent()
        {
            if (SelectedSource != null && SelectedAccount != null && CurrentEventAmount > 0)
            {
                CurrentEvent.Date = CurrentDate;
                CurrentEvent.Amount = CurrentEventAmount;
                CurrentEvent.AccountId = SelectedAccount.Id;
                CurrentEvent.CategoryId = SelectedSource.Id;
                CurrentEvent.OperationType = (int)Operations.Income;
                SelectedSource.Amount += CurrentEvent.Amount;
                SelectedAccount.Amount += CurrentEvent.Amount;

                if (CurrentEvent.Id == 0)
                    dataService.AddEvent(CurrentEvent);
                else
                    dataService.UpdateEvent(CurrentEvent);

                dataService.UpdateDictItem(SelectedSource);
                dataService.UpdateDictItem(SelectedAccount);


                MessageArg message = new MessageArg
                {
                    Sender = MessageSenders.IncomingForm,
                    Message = CurrentEvent
                };

                eventAggregator.PublishOnUIThread(message); // сообщение для журнала

                eventAggregator.PublishOnUIThread(true); // сообщение для показа остатков

                CurrentEvent = new Event
                {
                    CategoryId = SelectedSource.Id,
                    AccountId = SelectedAccount.Id
                };



                CurrentEventAmount = 0;
            }
            else
            {
                MessageBox.Show("Нулевой доход. Шутите?");
            }
        }

        public bool CanSaveEvent
        {
            get
            {
                return SourceName != null
                       && SelectedAccount != null
                       && CurrentEventAmount > 0;
            }
        }

        public void Cancel()
        {
            CurrentDate = DateTime.Today;
            SourceName = "";
            CurrentEventAmount = 0;
            SelectedAccount = null;
            SelectedSource = null;
            CurrentEvent = new Event();
        }

        #endregion
        private void GetAccounts()
        {
            Accounts.Clear();
            var items = dataService.GetAccounts();
            foreach (var a in items)
            {
                Accounts.Add(a);
            }
        }

    }
}
