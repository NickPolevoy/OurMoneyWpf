using Caliburn.Micro;
using OurMoneyWpf.Modules.Categories.Accounts.DetailList;
using OurMoneyWpf.Modules.Categories.Accounts.ShortList;
using OurMoneyWpf.Modules.Categories.Accounts.Transfers;

namespace OurMoneyWpf.Modules.Categories.Accounts
{
    public class AccountsViewModel : PropertyChangedBase
    {
        public AccountsViewModel(ShortListViewModel shortListViewModel, DetailListViewModel detailListViewModel, TransfersViewModel transfersViewModel)
        {
            ShortListVm = shortListViewModel;
            DetailListVm = detailListViewModel;
            TransfersVm = transfersViewModel;
        }

        public ShortListViewModel ShortListVm { get; }
        public DetailListViewModel DetailListVm { get; }
        public TransfersViewModel TransfersVm { get; }
    }
}