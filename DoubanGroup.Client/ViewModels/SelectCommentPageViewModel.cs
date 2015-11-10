using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Client.ViewModels
{
    public class SelectCommentPageViewModel : DialogViewModelBase<int>
    {
        public ObservableCollection<int> PageList { get; private set; }

        private int _currentPage;

        public int CurrentPage
        {
            get { return _currentPage; }
            set { this.SetProperty(ref _currentPage, value); }
        }

        public SelectCommentPageViewModel(int pageCount, int currentPage)
            : base(typeof(Views.SelectCommentPage))
        {
            CurrentPage = currentPage;
            this.DialogResult = currentPage;

            this.PageList = new ObservableCollection<int>();

            for (var i = 1; i <= pageCount; i++)
            {
                this.PageList.Add(i);
            }

            this.CurrentPage = this.PageList[currentPage - 1];
        }

        private DelegateCommand<object> _choosePageCommand;

        public DelegateCommand<object> ChoosePageCommand
        {
            get
            {
                if (_choosePageCommand == null)
                {
                    _choosePageCommand = new DelegateCommand<object>(ChoosePage);
                }
                return _choosePageCommand;
            }
        }

        private void ChoosePage(object parameter)
        {
            int page = Convert.ToInt32(parameter);

            this.ChoosePageAndHide(page);
        }

        private DelegateCommand _firstPageCommand;

        public DelegateCommand FirstPageCommand
        {
            get
            {
                if (_firstPageCommand == null)
                {
                    _firstPageCommand = new DelegateCommand(FirstPage);
                }
                return _firstPageCommand;
            }
        }

        private void FirstPage()
        {
            this.ChoosePageAndHide(1);
        }

        private DelegateCommand _lastPageCommand;

        public DelegateCommand LastPageCommand
        {
            get
            {
                if (_lastPageCommand == null)
                {
                    _lastPageCommand = new DelegateCommand(LastPage);
                }
                return _lastPageCommand;
            }
        }

        private void LastPage()
        {
            this.ChoosePageAndHide(this.PageList.LastOrDefault());
        }

        private void ChoosePageAndHide(int page)
        {
            this.DialogResult = page;
            this.Hide();
        }
    }
}
