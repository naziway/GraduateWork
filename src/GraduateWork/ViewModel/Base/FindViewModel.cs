using PropertyChanged;
using Shared;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel.Base
{
    [ImplementPropertyChanged]
    public class FindViewModel
    {
        private string findtext;
        public List<string> FindingParametersList { get; set; }

        public ICommand FindCommand => new CommandHandler(Finding);
        public string SelectedParam { get; set; }
        public string FindText
        {
            get { return findtext; }
            set
            {
                findtext = value;
                Task.Factory.StartNew(() => Finding(findtext));
            }
        }

        public void Finding()
        {
            Finding(findtext);
        }
        protected virtual void Finding(string text)
        {

        }


    }
}