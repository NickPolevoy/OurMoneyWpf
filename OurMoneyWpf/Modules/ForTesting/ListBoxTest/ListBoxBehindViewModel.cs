using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using OurMoneyWpf.Common;
using OurMoneyWpf.DataProvider.Entities;

namespace OurMoneyWpf.Modules.ForTesting.ListBoxTest
{
    public class ListBoxBehindViewModel : Screen
    {
        private readonly IDataService dataService;
        public BindableCollection<Dictionary> IncomingSources { get; set; } = new BindableCollection<Dictionary>();
        //       public DataForList[] ListData { get; set; } = new DataForList[4];
        public BindableCollection<Input.Incoming.Controls.DataForList> ListData { get; set; } = new BindableCollection<Input.Incoming.Controls.DataForList>();


        public ListBoxBehindViewModel(IDataService dataService)
        {
            this.dataService = dataService;
 
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
                var item = new Input.Incoming.Controls.DataForList();
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
