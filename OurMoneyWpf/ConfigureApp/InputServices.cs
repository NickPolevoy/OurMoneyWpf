using Caliburn.Micro;
using OurMoneyWpf.Modules.Input.Debt;
using OurMoneyWpf.Modules.Input.Incoming;
using OurMoneyWpf.Modules.Input.Outcoming;
using OurMoneyWpf.Modules.Input.Transfer;

namespace OurMoneyWpf.ConfigureApp
{
    public class InputServices
    {
        public InputServices(SimpleContainer container)
        {
            container.PerRequest<DebtViewModel>();
            container.PerRequest<IncomingViewModel>();
            container.PerRequest<OutcomingViewModel>();
            container.PerRequest<TransferViewModel>();
        }
    }
}
