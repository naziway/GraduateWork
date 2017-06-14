using DatabaseService;
using Model;
using PropertyChanged;
using Shared;
using Shared.Enum;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel.Lists
{
    [ImplementPropertyChanged]
    public class SellingListViewModel
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

        public SellingListViewModel(DataService dataService)
        {
            DataService = dataService;
            var command = new CommandWithParameters((async o =>
            {
                await OpenViewReviewToRepair(o);
            }));
            var viewcommand = new CommandWithParameters(OpenRepairByReview);
            var list = DataService.GetReviews();
            ViewCommand = viewcommand;
            Command = command;
            list.ForEach((review) =>
            {
                review.Command = command;
                review.ViewCommand = ViewCommand;
            });
            Reviews = new ObservableCollection<Review>(list);
        }

        public ICommand Command { get; set; }
        public ICommand ViewCommand { get; set; }
        private async Task OpenViewReviewToRepair(object obj)
        {
            var review = obj as Review;
            if (review == null)
                return;
            await DataService.ChangeReviewStatusById(review.Id, ReviewStatus.Reviewing);
            OpenWindowByDataAction(OpenWindow.ReviewToOrder, review);
        }

        private void OpenRepairByReview(object obj)
        {
            var review = obj as Review;
            if (review == null)
                return;
            var repair = DataService.GetRepairsByKod(review.Repair.Kod);
            OpenWindowByDataAction(OpenWindow.RepairInfo, repair);
        }
    }
}