using Microsoft.Practices.Unity;
using MyToolkit.Paging;
using Prism.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace DoubanGroup.Client
{
    /// <summary>
    /// 集成Prism和MyToolkit的Application类
    /// </summary>
    public abstract class ExtendedMtApplication : MtApplication
    {
        #region 属性

        /// <summary>
        /// IOC容器
        /// </summary>
        public IUnityContainer Container { get; private set; }

        /// <summary>
        /// 导航服务
        /// </summary>
        protected INavigationService NavigationService { get; private set; }

        /// <summary>
        /// Shell
        /// </summary>
        protected UIElement Shell { get; private set; }

        #endregion

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {

        }

        protected virtual UIElement CreateShell(MtFrame rootFrame)
        {
            return null;
        }

        protected virtual MtFrame CreateFrame()
        {
            return new MtFrame();
        }

        protected virtual void COnfigureContainer()
        {
            this.Container = new UnityContainer();
            this.Container.RegisterInstance(this.Container);
        }
    }
}
