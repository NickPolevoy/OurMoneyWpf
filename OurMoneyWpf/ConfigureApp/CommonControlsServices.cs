using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using OurMoneyWpf.Common.Journal;
using OurMoneyWpf.Modules.Input.Common.AccountBalances;
//using OurMoneyWpf.Modules.Input.Common.Journal;

namespace OurMoneyWpf.ConfigureApp
{
    public class CommonControlsServices
    {
        public CommonControlsServices(SimpleContainer container)
        {
 //           container.PerRequest<JournalViewModel>();
            container.PerRequest<AccountBalancesViewModel>();
        }
    }
}
