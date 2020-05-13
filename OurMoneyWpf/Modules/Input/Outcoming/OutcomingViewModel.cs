using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using OurMoneyWpf.Modules.Input.Common.AccountBalances;
//using OurMoneyWpf.Modules.Input.Common.Journal;
using OurMoneyWpf.Modules.Input.Outcoming.Controls;

namespace OurMoneyWpf.Modules.Input.Outcoming
{
    public class OutcomingViewModel : Screen
    {
        public OutcomingViewModel(
                OutcomingFormViewModel outcomingFormViewModel,
                CategoriesControlViewModel categoriesControlViewModel,
                OutcomingJournalViewModel journalViewModel,
                AccountBalancesViewModel accountBalancesViewModel)
        {
            OutcomingFormVM = outcomingFormViewModel;
            CategoriesControlVM = categoriesControlViewModel;
            JournalVM = journalViewModel;
            AccountBalancesVM = accountBalancesViewModel;
        }
        public OutcomingFormViewModel OutcomingFormVM { get; set; }
        public CategoriesControlViewModel CategoriesControlVM { get; set; }
        public OutcomingJournalViewModel JournalVM { get; set; }
        public AccountBalancesViewModel AccountBalancesVM { get; set; }

    }
}
