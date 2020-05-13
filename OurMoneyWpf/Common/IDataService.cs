using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OurMoneyWpf.DataProvider.Entities;

namespace OurMoneyWpf.Common
{
    public interface IDataService
    {
        #region Получение справочников

        IEnumerable<Dictionary> GetAccounts();
        IEnumerable<Dictionary> GetDebts();
        IEnumerable<Dictionary> GetMarkets();
        IEnumerable<Dictionary> GetSources();
        IEnumerable<Dictionary> GetCategories();

        #endregion

        #region Получение одного элемента справочника

        Dictionary GetDictItem(int? id);
        #endregion

        #region Обновление Справочников

        void AddDictItem(Dictionary item);
        void UpdateDictItem(Dictionary item);

        void DeleteDictItem(Dictionary item);
        void DeleteDictItem(int? id);
        #endregion

        #region Получение событий

        IEnumerable<Event> GetIncomingEvents(DateTime fromDate, DateTime toDate);
        IEnumerable<Event> GetOutcomingEvents(DateTime fromDate, DateTime toDate);
        Event GetEventById(int? id);
        #endregion

        #region Обновление базы событий

        void AddEvent(Event item);
        void UpdateEvent(Event item);
        void DeleteEvent(Event item);
        void DeleteEvent(int? id);

        #endregion

        #region Получение чеков

        IEnumerable<Invoice> GetInvoices(DateTime fromDate, DateTime toDate);
        Invoice GetInvoice(int id);

        #endregion

        #region Обновление базы чеков

        void AddInvoice(Invoice item);
        void UpdateInvoice(Invoice item);
        void DeleteInvoice(int id);


        #endregion
    }
}
