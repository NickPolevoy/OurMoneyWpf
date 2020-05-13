using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OurMoneyWpf.DataProvider.Entities;
using OurMoneyWpf.DataProvider.Repository;

namespace OurMoneyWpf.Common
{

    public class DataService : IDataService
    {
        private readonly IRepository<Dictionary> dictionaries;
        private readonly IRepository<Event> events;
        private readonly IRepository<Invoice> invoices;
        public DataService(IRepository<Dictionary> dictionaries, IRepository<Event> events, IRepository<Invoice> invoices)
        {
            this.dictionaries = dictionaries;
            this.events = events;
            this.invoices = invoices;
        }

        #region Получение справочников

        public IEnumerable<Dictionary> GetAccounts()
        {
            return dictionaries.GetMany((x) => x.EntityType == (int)EntityTypes.Accounts).OrderByDescending(x => x.Rate);
        }
        public IEnumerable<Dictionary> GetDebts()
        {
            return dictionaries.GetMany((x) => x.EntityType == (int)EntityTypes.Debts).OrderByDescending(x => x.Rate); ;
        }
        public IEnumerable<Dictionary> GetMarkets()
        {
            return dictionaries.GetMany((x) => x.EntityType == (int)EntityTypes.Markets).OrderByDescending(x => x.Rate); ;
        }
        public IEnumerable<Dictionary> GetSources()
        {
            return dictionaries.GetMany((x) => x.EntityType == (int)EntityTypes.Sources).OrderByDescending(x => x.Rate); ;
        }
        public IEnumerable<Dictionary> GetCategories()
        {
            return dictionaries.GetMany((x) => x.EntityType == (int)EntityTypes.Categories).OrderByDescending(x => x.Rate); ;
        }

        #endregion


        public Dictionary GetDictItem(int? id)
        {
            return dictionaries.GetById(id);
        }

        #region Обновление Справочников

        public void AddDictItem(Dictionary item)
        {
            dictionaries.Add(item);
        }
        public void UpdateDictItem(Dictionary item)
        {
            dictionaries.Update(item);
        }

        public void DeleteDictItem(Dictionary item)
        {
            dictionaries.Delete(item);
        }
        public void DeleteDictItem(int? id )
        {
            var item = dictionaries.GetById(id);
            dictionaries.Delete(item);
        }
        #endregion

        #region Получение событий

        public IEnumerable<Event> GetIncomingEvents(DateTime fromDate, DateTime toDate)
        {
            return events.GetMany((x) => x.OperationType == (int)Operations.Income
                                         && x.Date >= fromDate
                                         && x.Date <= toDate).OrderBy(x => x.Date);
        }
        public IEnumerable<Event> GetOutcomingEvents(DateTime fromDate, DateTime toDate)
        {
            return events.GetMany((x) => x.OperationType == (int)Operations.Outcome
                                         && x.Date >= fromDate
                                         && x.Date <= toDate).OrderBy(x => x.Date);
        }
        public Event GetEventById(int? id)
        {
            return events.GetById(id);
        }
        #endregion

        #region Обновление базы событий

        public void AddEvent(Event item)
        {
            events.Add(item);
        }
        public void UpdateEvent(Event item)
        {
            events.Update(item);
        }
        public void DeleteEvent(Event item)
        {
            events.Delete(item);
        }
        public void DeleteEvent(int? id)
        {
            var item = events.GetById(id);
            events.Delete(item);
        }

        #endregion
        #region Получение чеков

        public IEnumerable<Invoice> GetInvoices(DateTime fromDate, DateTime toDate)
        {
            return invoices.GetMany(x => x.Date >= fromDate && x.Date <= toDate).OrderBy(x => x.Date);

        }

        public Invoice GetInvoice(int id)
        {
            return invoices.GetById(id);
        }

        #endregion

        #region Обновление базы чеков

        public void AddInvoice(Invoice item)
        {
            invoices.Add(item);
        }

        public void UpdateInvoice(Invoice item)
        {
            invoices.Update(item);
        }

        public void DeleteInvoice(int id)
        {

            var item =invoices.GetById(id);
            invoices.Delete(item);
        }


        #endregion
    }
}
    
