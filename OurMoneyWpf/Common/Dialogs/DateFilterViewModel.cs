using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Caliburn.Micro;
using Syncfusion.Windows.Controls;

namespace OurMoneyWpf.Common.Dialogs
{
    public class DateFilterViewModel :   Screen
    {
        private DateTime currentDate;
        public BindableCollection<FilterList> RangeList { get; set; } = new BindableCollection<FilterList>
       {
           new FilterList
           {
               Id = 1,
               Range = "Этот день"
           },
           new FilterList
           {
               Id = 2,
               Range = "Эта неделя"
           },
           new FilterList
           {
               Id = 3,
               Range = "Этот месяц"
           },
           new FilterList
           {
               Id = 4,
               Range = "Последние 7 дней"
           },
           new FilterList
           {
               Id = 5,
               Range = "Последние 30 дней"
           },
           new FilterList
           {
               Id = 100,
               Range = "Без фильтра"
           }
       };

        public DateFilterViewModel(DateTime currentDate)
        {
            this.currentDate = currentDate;
            FromDate = currentDate;
            ToDate = currentDate;
        }

        #region Свойства


        private DateTime fromDate;
        public DateTime FromDate
        {
            get => fromDate;
            set
            {
                Set(ref fromDate, value);
                NotifyOfPropertyChange(() => FromDate);
            }
        }

        private DateTime toDate;
        public DateTime ToDate
        {
            get => toDate;
            set
            {
                Set(ref toDate, value);
                NotifyOfPropertyChange(() => ToDate);
            }
        }


        #endregion

        public void Ok()
        {
            TryClose(true);
        }

        public void Cancel()
        {
            TryClose(false);
        }

        public void RangeSelected(SelectionChangedEventArgs selectedRange)
        {
            var rangeSelected = (FilterList)selectedRange.AddedItems[0];

            ToDate = DateTime.Today;
            switch (rangeSelected.Id)
            {
                case 1:
                    FromDate = currentDate;
                    ToDate = currentDate;
                    break;
                case 2:
                    FromDate = ToDate.AddDays(-Weekday(ToDate));
                    break;
                case 3:
                    FromDate = ToDate.AddDays(-ToDate.Day + 1);
                    break;
                case 4:
                    FromDate = ToDate.AddDays(-7);
                    break;
                case 5:
                    FromDate = ToDate.AddDays(-30);
                    break;
                case 100:
                    FromDate = DateTime.MinValue;
                    break;
            }
        }

        int Weekday(DateTime date)
        {
            var year = date.Year;
            var month = date.Month;
            var day = date.Day - 1;

            if (month < 3)
            {
                --year;
                month += 10;
            }
            else
                month -= 2;
            return (day + 31 * month / 12 + year + year / 4 - year / 100 + year / 400) % 7;
        }

        public class FilterList
        {
            public int Id { get; set; }
            public string Range { get; set; }
        }
    }
}
