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
    public class SourcesControlViewModel : PropertyChangedBase, IHandle<MessageArg>
    {
        private readonly IDataService dataService;
        private IEventAggregator eventAggregator;
        public BindableCollection<DataForList> ListData { get; set; } = new BindableCollection<DataForList>();


        public SourcesControlViewModel(IDataService dataService, IEventAggregator eventAggregator)
        {
            this.dataService = dataService;
            this.eventAggregator = eventAggregator;
            eventAggregator.Subscribe(this);

            GetSources();
        }

        public void Handle(MessageArg msg)
        {
            if (msg.Sender == MessageSenders.IncomingsPage)
                GetSources();
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

        public void SourceSelected()
        {
            var message = new MessageArg
            {
                Sender = MessageSenders.IncomingForm,
                Message = SelectedSource
            };
            eventAggregator.PublishOnUIThread(message);
        }


        private void GetSources()
        {
            ListData.Clear();
            var items = dataService.GetSources().ToList();

            if (items.Count == 0) return;

            var sources = new List<Dictionary>();

            foreach (var s in items)
            {
                if (s.ParentId == (int)RootParentId.Source)
                {
                    sources.Add(s);
                }
            }
            foreach (var s in items)
            {
                if (s.ParentId > 0)
                {
                    sources.Add(s);
                }
            }
            for (int i = 0; i < 3; i++)
            {
                var item = new DataForList();
                if (sources[i] != null)
                {
                    if (sources[i].ParentId == (int)RootParentId.Source)
                    {
                        item.Header = sources[i].Name;
                        item.YesNo = Visibility.Visible;
                        item.Sources = new BindableCollection<Dictionary>();
                        foreach (var source in sources)
                        {
                            if (source.ParentId == sources[i].Id)
                            {
                                item.Sources.Add(source);
                            }
                        }
                    }
                }
                else
                {
                    item.YesNo = Visibility.Hidden;
                }

                ListData.Add(item);
            }

        }
    }
    public class DataForList
    {
        public int Id { get; set; }
        public Visibility YesNo { get; set; }
        public BindableCollection<Dictionary> Sources { get; set; }
        public string Header { get; set; }

    }
}
