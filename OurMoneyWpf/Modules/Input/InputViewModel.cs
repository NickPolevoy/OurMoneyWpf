using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using OurMoneyWpf.Modules.Input.Debt;
using OurMoneyWpf.Modules.Input.Incoming;
using OurMoneyWpf.Modules.Input.Outcoming;
using OurMoneyWpf.Modules.Input.Transfer;

namespace OurMoneyWpf.Modules.Input
{
    public class InputViewModel : Screen
    {
        public InputViewModel(DebtViewModel debtViewModel,
                              IncomingViewModel incomingViewModel,
                              OutcomingViewModel outcomingViewModel,
                              TransferViewModel transferViewModel)
        {
            DebtVm = debtViewModel;
            IncomingVm = incomingViewModel;
                            OutcomingVm = outcomingViewModel;
            TransferVm = transferViewModel;
        }

        public DebtViewModel DebtVm { get; set; }
        public IncomingViewModel IncomingVm { get; set; }
        public OutcomingViewModel OutcomingVm { get; set; }
        public TransferViewModel TransferVm { get; set; }

    }
}
