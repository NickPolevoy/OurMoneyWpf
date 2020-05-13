using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using OurMoneyWpf.Modules.Input.Incoming.Controls;
using OurMoneyWpf.Modules.Input.Outcoming.Controls;

namespace OurMoneyWpf.ConfigureApp
{
    public class LocalControlsServices
    {
        public LocalControlsServices(SimpleContainer container)
        {
            container.PerRequest<IncomingFormViewModel>();
            container.PerRequest<SourcesControlViewModel>();
            container.PerRequest<OutcomingFormViewModel>();
            container.PerRequest<CategoriesControlViewModel>();
            container.PerRequest<IncomingJournalViewModel>();
            container.PerRequest<OutcomingJournalViewModel>();

        }
    }
}
