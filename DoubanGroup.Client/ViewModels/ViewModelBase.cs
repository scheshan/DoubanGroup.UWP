using Prism.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using DoubanGroup.Core.Api;
using Windows.UI.Popups;
using Prism.Events;

namespace DoubanGroup.Client.ViewModels
{
    public abstract class ViewModelBase : Prism.Windows.Mvvm.ViewModelBase
    {
        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { this.SetProperty(ref _isLoading, value); }
        }

        protected INavigationService NavigationService
        {
            get
            {
                return App.Current.Container.Resolve<INavigationService>();
            }
        }

        private static ApiClient CreateApiClient()
        {
            return App.Current.Container.Resolve<ApiClient>();
        }

        private Lazy<ApiClient> _apiClient = new Lazy<ApiClient>(CreateApiClient);

        protected ApiClient ApiClient
        {
            get
            {
                return _apiClient.Value;
            }
        }

        public CurrentUserViewModel CurrentUser
        {
            get
            {
                return App.Current.Container.Resolve<CurrentUserViewModel>();
            }
        }

        public IEventAggregator EventAggretator
        {
            get
            {
                return App.Current.Container.Resolve<IEventAggregator>();
            }
        }

        public virtual void Alert(string content, string title = null)
        {
            MessageDialog dialog = this.CreateMessageDialog(content, title);
            dialog.ShowAsync();
        }

        public virtual async Task<bool> Confirm(string content, string title = null)
        {
            MessageDialog dialog = this.CreateMessageDialog(content, title);

            dialog.Commands.Add(new UICommand("确认") { Id = 1 });
            dialog.Commands.Add(new UICommand("取消") { Id = 2 });

            var cmd = await dialog.ShowAsync();

            var id = Convert.ToInt32(cmd.Id);

            return id == 1;
        }

        /// <summary>
        /// 创建默认的消息框
        /// </summary>
        /// <param name="content"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        private MessageDialog CreateMessageDialog(string content, string title)
        {
            MessageDialog dialog;

            if (title == null)
            {
                dialog = new MessageDialog(content);
            }
            else
            {
                dialog = new MessageDialog(content, title);
            }

            return dialog;
        }

        protected async Task<bool> RequireLogin()
        {
            if (this.CurrentUser.IsLogin)
            {
                return true;
            }

            var vm = new LoginPageViewModel();
            return await vm.Show();
        }
    }
}
