using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using OurMoneyWpf.Common;
using OurMoneyWpf.DataProvider.Entities;
using OurMoneyWpf.DataProvider.Repository;

namespace OurMoneyWpf.ConfigureApp
{
    public class RepoServices
    {
        public RepoServices(SimpleContainer container)
        {
            container.Singleton<OurMoneyDbContext>();
            container.Singleton<IRepository<Event>, Repository<Event>>();
            container.Singleton<IRepository<Dictionary>, Repository<Dictionary>>();
            container.Singleton<IRepository<Invoice>, Repository<Invoice>>();
            container.Singleton<IDataService, DataService>();
        }
    }
 }
