using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using OurMoneyWpf.Common;
using OurMoneyWpf.DataProvider.Entities;
using OurMoneyWpf.DataProvider.Repository;

namespace OurMoneyWpf.Modules.Categories.Markets
{
    public class MarketsViewModel : PropertyChangedBase
    {
        private readonly IDataService markets;
        private readonly IEventAggregator eventAggregator;
        public BindableCollection<Dictionary> Markets { get; set; }

        public MarketsViewModel(IDataService markets, IEventAggregator eventAggregator)
        {
            Markets = new BindableCollection<Dictionary>();
            this.markets = markets;
            this.eventAggregator = eventAggregator;
            GetAllItems();
        }

        #region Комманды

        public void AddOrEdit(DataGridRowEditEndingEventArgs marketItem)
        {
            var market = (Dictionary)marketItem.Row.DataContext;
            if (market.Name != null)
            {
                if (market.Id == 0)
                {
                    market.EntityType = (int) EntityTypes.Markets;
                    markets.AddDictItem(market);
                }
                else
                {
                    markets.UpdateDictItem(market);
                }
                MessageArg message = new MessageArg
                {
                    Sender = MessageSenders.MarketsPage,
                    Message = market
                };

                eventAggregator.PublishOnUIThread(message); // сообщение для формы
            }
        }

        public void Delete(Dictionary marketItem)
        {
            if (marketItem == null) return;
            var result = MessageBox.Show("Точка продаж " + marketItem.Name + " будет удалена навсегда",
                "Вы уверены?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                markets.DeleteDictItem(marketItem);
                Markets.Remove(marketItem);
                MessageArg message = new MessageArg
                {
                    Sender = MessageSenders.MarketsPage,
                    Message = marketItem
                };

                eventAggregator.PublishOnUIThread(message); // сообщение для формы
            }
        }
        public bool CanDelete(Dictionary item)
        {
            return item != null;
        }

        public void RefreshGrid()
        {
            Markets.Clear();
            GetAllItems();
        }

        #endregion Комманды

        private void GetAllItems()
        {
            var items = markets.GetMarkets();
            foreach (var a in items)
            {
                Markets.Add(a);
            }
        }
    }
}
