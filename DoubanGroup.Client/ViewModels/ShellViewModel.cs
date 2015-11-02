using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Client.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private bool _isPaneOpen;

        public bool IsPaneOpen
        {
            get { return _isPaneOpen; }
            set { this.SetProperty(ref _isPaneOpen, value); }
        }

        private DelegateCommand _togglePaneCommand;

        public DelegateCommand TogglePaneCommand
        {
            get
            {
                if (_togglePaneCommand == null)
                {
                    _togglePaneCommand = new DelegateCommand(this.TogglePane);
                }
                return _togglePaneCommand;
            }
        }

        private void TogglePane()
        {
            this.IsPaneOpen = !this.IsPaneOpen;
        }

        private DelegateCommand _loginCommand;

        public DelegateCommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new DelegateCommand(this.Login);
                }
                return _loginCommand;
            }
        }

        private void Login()
        {
            var vm = new LoginPageViewModel();
            vm.Show();
        }

        private DelegateCommand<string> _navigateCommand;

        public DelegateCommand<string> NavigateCommand
        {
            get
            {
                if (_navigateCommand == null)
                {
                    _navigateCommand = new DelegateCommand<string>(this.Navigate);
                }
                return _navigateCommand;
            }
        }

        private void Navigate(string parameter)
        {
            this.NavigationService.Navigate(parameter.ToString(), null);
        }
    }
}
