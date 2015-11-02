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
    public abstract class DialogViewModelBase : ViewModelBase
    {
        protected ContentDialog DialogInstance { get; private set; }

        public DialogViewModelBase(Type dialogType)
        {
            this.DialogInstance = (ContentDialog)App.Current.Container.Resolve(dialogType);
            this.DialogInstance.DataContext = this;
        }

        public virtual async Task<ContentDialogResult> Show()
        {
            return await this.DialogInstance.ShowAsync();
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
