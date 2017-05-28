using DatabaseService;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ViewModel
{
    public class HistogramLogic
    {
        public DataService DataService { get; set; }

        public HistogramLogic(DataService dataService)
        {
            DataService = dataService;
        }




        public List<HistogramModel> GetStatisticList()
        {
            List<HistogramModel> list = new List<HistogramModel>();


            var repairs = DataService.GetRepairs().Where(repair => repair.OrderDate > DateTime.Now.AddMonths(-6)).GroupBy(repair => repair.OrderDate.Month.ToString());
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

            return list;
        }

    }
}