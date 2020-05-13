using Caliburn.Micro;
using OurMoneyWpf.Modules.Categories.Accounts;
using OurMoneyWpf.Modules.Categories.Accounts.DetailList;
using OurMoneyWpf.Modules.Categories.Accounts.ShortList;
using OurMoneyWpf.Modules.Categories.Accounts.Transfers;
using OurMoneyWpf.Modules.Categories.Debts;
using OurMoneyWpf.Modules.Categories.Incomings;
using OurMoneyWpf.Modules.Categories.Markets;
using OurMoneyWpf.Modules.Categories.Outcomings;

namespace OurMoneyWpf.ConfigureApp
{
    public class DictServices
    {
        public DictServices(SimpleContainer container)
        {
            container.PerRequest<AccountsViewModel>();
            container.PerRequest<ShortListViewModel>();
            container.PerRequest<DetailListViewModel>();
            container.PerRequest<TransfersViewModel>();
            container.PerRequest<MarketsViewModel>();
            container.PerRequest<IncomingsViewModel>();
            container.PerRequest<OutcomingsViewModel>();
            container.PerRequest<DebtsViewModel>();
        }
    }
}
