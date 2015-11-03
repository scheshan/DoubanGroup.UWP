using DoubanGroup.Core.Api;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace DoubanGroup.Client.ViewModels
{
    public class LoginPageViewModel : DialogViewModelBase<bool>
    {
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { this.SetProperty(ref _userName, value); }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { this.SetProperty(ref _password, value); }
        }

        public LoginPageViewModel()
            : base(typeof(Views.LoginPage))
        {
            this.DialogResult = false;
        }

        private DelegateCommand _loginCommand;

        public DelegateCommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new DelegateCommand(Login);
                }
                return _loginCommand;
            }
        }

        private async void Login()
        {
            if (string.IsNullOrWhiteSpace(this.UserName))
            {
                this.Alert("请输入用户名");
                return;
            }
            if (string.IsNullOrWhiteSpace(this.Password))
            {
                this.Alert("请输入密码");
                return;
            }

            if (this.IsLoading)
            {
                return;
            }

            this.IsLoading = true;

            try
            {
                var session = await this.ApiClient.Login(this.UserName, this.Password);
                this.CurrentUser.SetSession(session);

                this.DialogResult = true;

                this.Hide();
            }
            catch (ApiException)
            {
                this.Alert("用户名或密码错误");
            }
            catch (Exception ex)
            {
                this.Alert("登录发生错误");
            }
            finally
            {
                this.IsLoading = false;
            }
        }
    }
}
