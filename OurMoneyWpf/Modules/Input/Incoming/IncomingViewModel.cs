using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;
using Caliburn.Micro;
using OurMoneyWpf.Common;
using OurMoneyWpf.Common.Dialogs;
using OurMoneyWpf.Common.Journal;
using OurMoneyWpf.DataProvider.Entities;
using OurMoneyWpf.DataProvider.Repository;
using OurMoneyWpf.Modules.Input.Common.AccountBalances;
using OurMoneyWpf.Modules.Input.Common.Journa;
//using OurMoneyWpf.Modules.Input.Common.Journal;
using OurMoneyWpf.Modules.Input.Incoming.Controls;

namespace OurMoneyWpf.Modules.Input.Incoming
{
    public class IncomingViewModel : Screen
    {
        public IncomingViewModel(
            IncomingFormViewModel incomingFormViewModel,
            SourcesControlViewModel sourcesControlViewModel,
            IncomingJournalViewModel journalViewModel,
            AccountBalancesViewModel accountBalancesViewModel)
        {
            IncomingFormVM = incomingFormViewModel;
            SourcesControlVM = sourcesControlViewModel;
            IncomingJournalVM = journalViewModel;
            AccountBalancesVM = accountBalancesViewModel;
        }

        public IncomingFormViewModel IncomingFormVM { get; set; }
        public SourcesControlViewModel  SourcesControlVM { get; set; }
        public IncomingJournalViewModel IncomingJournalVM { get; set; }
        public AccountBalancesViewModel AccountBalancesVM { get; set; }
    }
}