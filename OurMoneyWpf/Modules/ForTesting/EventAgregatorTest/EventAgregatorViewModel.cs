using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace OurMoneyWpf.Modules.ForTesting.EventAgregatorTest
{
    public class EventAgregatorViewModel
    {
        public EventAgregatorViewModel()
        {
            IEventAggregator events = new EventAggregator();

            ButtonsVM = new ButtonsViewModel(events);
            TextVM = new TextViewModel(events);
        }
        public ButtonsViewModel ButtonsVM
        {
            get;
            set;
        }

        public TextViewModel TextVM
        {
            get;
            set;
        }

    }
}
