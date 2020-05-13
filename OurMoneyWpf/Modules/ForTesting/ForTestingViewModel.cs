using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using OurMoneyWpf.Modules.ForTesting.EventAgregatorTest;
using OurMoneyWpf.Modules.ForTesting.ListBoxTest;

namespace OurMoneyWpf.Modules.ForTesting
{
    public class ForTestingViewModel : Screen
    {

        public EventAgregatorViewModel EventAgregatorViewModel { get; }
        public ListBoxBehindViewModel ListBoxBehindViewModel { get; set; }
        public ForTestingViewModel(ListBoxBehindViewModel listBoxBehindViewModel)
        {
            EventAgregatorViewModel = new EventAgregatorViewModel();
            ListBoxBehindViewModel =  listBoxBehindViewModel;
        }
    }
}
