using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace DoubanGroup.Client.ViewModels
{
    public abstract class DialogViewModelBase<T> : ViewModelBase
    {
        private T _dialogResult;

        public T DialogResult
        {
            get { return _dialogResult; }
            protected set { this.SetProperty(ref _dialogResult, value); }
        }

        protected ContentDialog DialogInstance { get; private set; }

        public DialogViewModelBase(Type dialogType)
        {
            this.DialogInstance = (ContentDialog)App.Container.Resolve(dialogType);
            this.DialogInstance.DataContext = this;
        }


        public virtual async Task<T> Show()
        {
            await this.DialogInstance.ShowAsync();

            return this.DialogResult;
        }

        private DelegateCommand _hideCommand;

        public DelegateCommand HideCommand
        {
            get
            {
                if (_hideCommand == null)
                {
                    _hideCommand = new DelegateCommand(Hide);
                }
                return _hideCommand;
            }
        }

        public virtual void Hide()
        {
            this.DialogInstance.Hide();
        }
    }
}
