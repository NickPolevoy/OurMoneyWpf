using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurMoneyWpf.Common
{
    public enum EntityTypes
    {
        Categories = 1,
        Sources,
        Accounts,
        Debts,
        Markets,
        Loyalty
    }

    public enum RootParentId
    {
        Food = -1,
        Other = -2,
        Source = -3
    }

    public enum Operations
    {
        Outcome = 1,
        Income,
        Transfer
    }

    public enum MessageSenders
    {
        AccountsPage,
        DebtsPage,
        IncomingsPage,
        MarketsPage,
        OutcomingsPage,
        IncomingForm,
        IncomingJournal,
        OutcomingForm,
        OutcomingCategories,
        OutcomingJournal
    }

}
