using Caliburn.Micro;
using OurMoneyWpf.Modules.Categories.Accounts;
using OurMoneyWpf.Modules.Categories.Debts;
using OurMoneyWpf.Modules.Categories.Incomings;
using OurMoneyWpf.Modules.Categories.Markets;
using OurMoneyWpf.Modules.Categories.Outcomings;

namespace OurMoneyWpf.Modules.Categories
{
    public class CategoriesViewModel : Screen
    {
        public CategoriesViewModel(
                                   AccountsViewModel accountsViewModel,
                                   MarketsViewModel marketsViewModel,
                                   IncomingsViewModel incomingsViewModel,
                                   OutcomingsViewModel outcomingsViewModel,
                                   DebtsViewModel debtsViewModel)
        {
            AccountsVm = accountsViewModel;
            MarketsVm = marketsViewModel;
            IncomingsVm = incomingsViewModel;
            OutcomingsVm = outcomingsViewModel;
            DebtsVm = debtsViewModel;
        }

        public AccountsViewModel AccountsVm { get; }
        public MarketsViewModel MarketsVm { get; }
        public IncomingsViewModel IncomingsVm { get; }
        public OutcomingsViewModel OutcomingsVm { get; }
        public DebtsViewModel DebtsVm { get; }

 
    }
}