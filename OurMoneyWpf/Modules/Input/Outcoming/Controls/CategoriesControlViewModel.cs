using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using OurMoneyWpf.Common;
using OurMoneyWpf.Common.Journal;
using OurMoneyWpf.Modules.Input.Common.AccountBalances;
using OurMoneyWpf.DataProvider.Entities;

namespace OurMoneyWpf.Modules.Input.Outcoming.Controls
{
    public class CategoriesControlViewModel : PropertyChangedBase, IHandle<MessageArg>
    {
        private readonly IDataService dataService;
        private readonly IEventAggregator eventAggregator;
        public BindableCollection<DataForList> ListFoodData { get; set; } = new BindableCollection<DataForList>();
        public BindableCollection<DataForList> ListOtherData { get; set; } = new BindableCollection<DataForList>();

        public CategoriesControlViewModel(IDataService dataService, IEventAggregator eventAggregator)
        {
            this.dataService = dataService;
            this.eventAggregator = eventAggregator;

            eventAggregator.Subscribe(this);

            GetCategories();
        }

        public void Handle(MessageArg msg)
        {
            if (msg.Sender == MessageSenders.OutcomingsPage)
               GetCategories();
                   //           if (msg.Sender == MessageSenders.OutcomingCategories)
//                eventAggregator.PublishOnUIThread(SelectedCategory);
        }
        private Dictionary selectedCategory;
        public Dictionary SelectedCategory
        {
            get => selectedCategory;
            set
            {
                Set(ref selectedCategory, value);
                NotifyOfPropertyChange(() => SelectedCategory);
            }
        }

        public void SourceSelected()
        {
            var message = new MessageArg
            {
                Sender = MessageSenders.IncomingForm,
                Message = SelectedCategory
            };
            eventAggregator.PublishOnUIThread(message);
        }



        private void GetCategories()
        {
            ListFoodData.Clear();
            ListOtherData.Clear();
            var categories = dataService.GetCategories();
            var foodItems = categories.Where(x => x.ParentId == (int)RootParentId.Food).ToList();
            var otherItems = categories.Where(x => x.ParentId == (int)RootParentId.Other).ToList();

            foreach (var f in foodItems)
            {
                var food = new DataForList();
                food.Header = f.Name;
                food.Categories = new BindableCollection<Dictionary>();
                foreach (var c in categories)
                {
                    if (c.ParentId == f.Id)
                    {
                        food.Categories.Add(c);
                    }
                }

                ListFoodData.Add(food);
            }

            foreach (var o in otherItems)
            {
                var other = new DataForList();
                other.Header = o.Name;
                other.Categories = new BindableCollection<Dictionary>();
                foreach (var c in categories)
                {
                    if (c.ParentId == o.Id)
                    {
                        other.Categories.Add(c);
                    }
                }

                ListOtherData.Add(other);
            }
        }
    }
    public class DataForList
    {
        public BindableCollection<Dictionary> Categories { get; set; }
        public string Header { get; set; }

    }
}

