﻿using DatabaseService;
using Model;
using System;
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
            var reviews = DataService.GetReviews().Where(review => review.OrderDate > DateTime.Now.AddMonths(-6)).GroupBy(review => review.OrderDate.Month.ToString());
            foreach (var review in reviews)
            {
                var value = review.Count() * 50;

                var bar = list.FirstOrDefault(model => model.Argument == review.Key);
                if (bar == null)
                    list.Add(new HistogramModel { Argument = review.Key, ReviewValue = value });
                else
                    bar.ReviewValue = value;
            }

            var sellings = DataService.GetSellings().Where(review => review.OrderDate > DateTime.Now.AddMonths(-6)).GroupBy(review => review.OrderDate.Month.ToString());
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

    }
}