using Prism.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Unity;

namespace DoubanGroup.Client.Commands
{
    public abstract class CommandBase : ICommand
    {
        private Lazy<INavigationService> _navigationService = new Lazy<INavigationService>(() =>
        {
            return App.Container.Resolve<INavigationService>();
        });

        protected INavigationService NavigationService
        {
            get
            {
                return _navigationService.Value;
            }
        }

        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public abstract void Execute(object parameter);
    }
}
