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
        #region 属性

        private int _loadingCounters = 0;

        /// <summary>
        /// 标记是否处于忙碌状态
        /// </summary>
        public bool IsLoading
        {
            get
            {
                return _loadingCounters > 0;
            }
            set
            {
                if(value)
                {
                    _loadingCounters++;
                }
                else
                {
                    _loadingCounters--;
                }

                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// 导航服务
        /// </summary>
        protected INavigationService NavigationService
        {
            get
            {
                return App.Current.Container.Resolve<INavigationService>();
            }
        }

        private Lazy<ApiClient> _apiClient = new Lazy<ApiClient>(() =>
        {
            return App.Current.Container.Resolve<ApiClient>();
        });

        /// <summary>
        /// API代理
        /// </summary>
        protected ApiClient ApiClient
        {
            get
            {
                return _apiClient.Value;
            }
        }

        /// <summary>
        /// 表示当前用户的视图模型对象
        /// </summary>
        public CurrentUserViewModel CurrentUser
        {
            get
            {
                return App.Current.Container.Resolve<CurrentUserViewModel>();
            }
        }

        /// <summary>
        /// 事件订阅服务
        /// </summary>
        public IEventAggregator EventAggretator
        {
            get
            {
                return App.Current.Container.Resolve<IEventAggregator>();
            }
        }

        /// <summary>
        /// 缓存服务
        /// </summary>
        public Service.ICacheService Cache { get; private set; }

        #endregion

        #region 构造器

        public ViewModelBase()
        {
            this.Cache = new Service.FileCacheService();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 弹出警告提示框
        /// </summary>
        /// <param name="content"></param>
        /// <param name="title"></param>
        public virtual async Task Alert(string content, string title = null)
        {
            MessageDialog dialog = this.CreateMessageDialog(content, title);
            await dialog.ShowAsync();
        }

        /// <summary>
        /// 弹出询问提示框
        /// </summary>
        /// <param name="content"></param>
        /// <param name="title"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 判断登录状态
        /// 如果未登录，则弹出登录界面
        /// </summary>
        /// <returns></returns>
        protected async Task<bool> RequireLogin()
        {
            if (this.CurrentUser.IsLogin)
            {
                return true;
            }

            var vm = new LoginPageViewModel();
            return await vm.Show();
        }

        #endregion
    }
}
