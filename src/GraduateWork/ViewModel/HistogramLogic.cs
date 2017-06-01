using DatabaseService;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace ViewModel
{
    public class HistogramLogic
    {
        private Dictionary<string, string> MonthByKey { get; set; } = new Dictionary<string, string>
        {
            {"1","Січень"},
            {"2","Лютий"},
            {"3","Березень"},
            {"4","Квітень"},
            {"5","Травень"},
            {"6","Червень"},
            {"7","Липень"},
            {"8","Серпень"},
            {"9","Вересень"},
            {"10","Жовтень"},
            {"11","Листопад"},
            {"12","Грудень"},
        };
        public DataService DataService { get; set; }



        public HistogramLogic(DataService dataService)
        {
            DataService = dataService;
        }

        public List<HistogramModel> GetStatisticList()
        {
            var list = new List<HistogramModel>();

            var repairs = DataService.GetRepairs().GroupBy(repair => repair.OrderDate.Month.ToString());
            foreach (var repair in repairs)
            {
                var value = 0.0;
                foreach (var item in repair)
                {
                    if (item.Part != null)
                        value += item.Part.Price;
                    value += item.Work.Price;
                }
                list.Add(new HistogramModel { Argument = repair.Key, RepairValue = value });
            }
            var reviews = DataService.GetReviews().GroupBy(review => review.OrderDate.Month.ToString());
            foreach (var review in reviews)
            {
                var value = review.Count() * 50;

                var bar = list.FirstOrDefault(model => model.Argument == review.Key);
                if (bar == null)
                    list.Add(new HistogramModel { Argument = review.Key, ReviewValue = value });
                else
                    bar.ReviewValue = value;
            }

            var sellings = DataService.GetSellings().GroupBy(review => review.OrderDate.Month.ToString());
            foreach (var selling in sellings)
            {
                var value = selling.Where(item => item.Part != null).Sum(item => item.Part.Price * item.Part.Count);
                var bar = list.FirstOrDefault(model => model.Argument == selling.Key);
                if (bar == null)
                    list.Add(new HistogramModel { Argument = selling.Key, SellingValue = value });
                else
                    bar.SellingValue = value;
            }

            list = list.OrderBy(model => model.Argument).ThenBy(model => model.Argument).ToList();
            list.ForEach(model => model.Argument = MonthByKey[model.Argument]);
            return list;
        }

        public List<SalaryHistogramModel> GetSalaryStatisticByUser(User user)
        {
            var salaryList =
                DataService.GetPaids().Where(paid => paid.UserId == user.Id);


            var returnList = new List<SalaryHistogramModel>();


            foreach (var item in salaryList)
            {
                returnList.Insert(0, new SalaryHistogramModel { Argument = MonthByKey[item.DatePaid.Month.ToString()], Value = item.Salary });
            }
            return returnList;
        }

        public List<CircleDiagramItem> GetIncomeCosts()
        {
            var list = new List<CircleDiagramItem>();
            double reviews = DataService.GetReviews().Count * 50;
            double repairs = 0.0;
            foreach (var repair in DataService.GetRepairs())
            {
                if (repair.Part != null)
                    repairs += repair.Part.Price;
                repairs += repair.Work.Price;
            }
            double selings = DataService.GetSellings().Sum(selling => selling.Part.Price);
            double salary = DataService.GetPaids().Sum(paid => paid.Salary);

            list.Add(new CircleDiagramItem
            {
                Argument = "Обстеження",
                Value = reviews
            });
            list.Add(new CircleDiagramItem
            {
                Argument = "Ремонт",
                Value = repairs
            });
            list.Add(new CircleDiagramItem
            {
                Argument = "Продаж",
                Value = selings
            });
            list.Add(new CircleDiagramItem
            {
                Argument = "Зарплата",
                Value = salary
            });


            return list;
        }
    }
}