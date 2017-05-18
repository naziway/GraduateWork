using DatabaseService;
using Model;
using PropertyChanged;
using Shared;
using Shared.Enum;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    [ImplementPropertyChanged]
    public class ReviewsViewModel
    {
        #region Action
        public Action<OpenWindow> OpenWindowAction { get; set; }
        public Action<OpenWindow, object> OpenWindowByDataAction { get; set; }
        public Action<OpenWindow, TransferData> OpenWindowWithEventResultAction { get; set; }

        public void SetAction(Action<OpenWindow, TransferData> action)
        {
            OpenWindowWithEventResultAction = action;
        }
        public void SetAction(Action<OpenWindow> action)
        {
            OpenWindowAction = action;
        }
        public void SetAction(Action<OpenWindow, object> action)
        {
            OpenWindowByDataAction = action;
        }
        #endregion

        public DataService DataService { get; set; }


        public ObservableCollection<Review> Reviews { get; set; }

        public ReviewsViewModel(DataService dataService)
        {
            DataService = dataService;
            var command = new CommandWithParameters((async o =>
            {
                await Reviewing(o);
            }));
            var list = DataService.GetReviews();
            Command = command;
            list.ForEach((order) => { order.Command = command; });
            Reviews = new ObservableCollection<Review>(list);
        }

        public ICommand Command { get; set; }
        private async Task Reviewing(object obj)
        {
            var review = obj as Review;
            if (review == null)
                return;
            await DataService.ChangeReviewStatusById(review.Id, ReviewStatus.Reviewing);
            OpenWindowByDataAction(OpenWindow.ReviewToOrder, review);
        }
    }
}