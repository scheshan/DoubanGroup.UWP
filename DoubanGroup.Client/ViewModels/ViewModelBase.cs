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
using Windows.UI.Xaml;
using Prism.Mvvm;

namespace DoubanGroup.Client.ViewModels
{
    public abstract class ViewModelBase : BindableBase
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
                if (value)
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
                return App.Container.Resolve<INavigationService>();
            }
        }

        private Lazy<ApiClient> _apiClient = new Lazy<ApiClient>(() =>
        {
            return App.Container.Resolve<ApiClient>();
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
                return App.Container.Resolve<CurrentUserViewModel>();
            }
        }

        /// <summary>
        /// 事件订阅服务
        /// </summary>
        public IEventAggregator EventAggretator
        {
            get
            {
                return App.Container.Resolve<IEventAggregator>();
            }
        }

        /// <summary>
        /// 缓存服务
        /// </summary>
        public Service.ICacheService CacheService
        {
            get
            {
                return new Service.FileCacheService();
            }
        }

        #endregion

        #region 构造器

        public ViewModelBase()
        {

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

        /// <summary>
        /// 异步执行方法，并返回结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task<T> RunTaskAsync<T>(Task<T> task)
        {
            T result = default(T);
            try
            {
                this.IsLoading = true;
                result = await task;
            }
            catch (Exception ex)
            {
                this.HandleException(ex);
            }
            finally
            {
                this.IsLoading = false;
            }

            return result;
        }

        /// <summary>
        /// 异步执行方法
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task<bool> RunTaskAsync(Task task)
        {
            try
            {
                this.IsLoading = true;
                await task;
                return true;
            }
            catch(Exception ex)
            {
                this.HandleException(ex);
                return false;
            }
            finally
            {
                this.IsLoading = false;
            }
        }

        protected virtual void HandleException(Exception ex)
        {
            string message;

            if (ex is ApiException)
            {
                message = ex.Message;
            }
            else
            {
                message = "系统发生错误";
            }

            this.ShowToast(message);
        }

        public void ShowToast(string message)
        {
            var shell = Window.Current.Content as Shell;
            shell?.ShowToast(message);
        }

        #endregion
    }

    public abstract class NavigationViewModelBase : ViewModelBase
    {
        public virtual void OnNavigatingFrom(NavigatingFromEventArgs e)
        {

        }

        public virtual void OnNavigatedTo(NavigatedToEventArgs e)
        {

        }
    }
}
