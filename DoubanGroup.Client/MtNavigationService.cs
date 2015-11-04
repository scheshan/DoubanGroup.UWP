using DoubanGroup.Client.ViewModels;
using MyToolkit.Paging;
using Prism.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace DoubanGroup.Client
{
    public sealed class MtNavigationService : INavigationService
    {
        private MtFrame RootFrame { get; set; }

        private object _lastPageContent;

        /// <summary>
        /// Gets the type of the page based on a page token.
        /// </summary>
        /// <param name="pageToken">The page token.</param>
        /// <returns>The type of the page which corresponds to the specified token.</returns>
        private Type GetPageType(string pageToken)
        {
            var assemblyQualifiedAppType = this.GetType().AssemblyQualifiedName;

            var pageNameWithParameter = assemblyQualifiedAppType.Replace(this.GetType().FullName, this.GetType().Namespace + ".Views.{0}Page");

            var viewFullName = string.Format(CultureInfo.InvariantCulture, pageNameWithParameter, pageToken);
            var viewType = Type.GetType(viewFullName);

            if (viewType == null)
            {
                throw new ArgumentException(
                    "Invalid page type",
                    "pageToken");
            }

            return viewType;
        }

        public MtNavigationService(MtFrame rootFrame)
        {
            this.RootFrame = rootFrame;

            this.RootFrame.Navigated += RootFrame_Navigated;

            this.RootFrame.Navigating += RootFrame_Navigating;
        }

        private void RootFrame_Navigating(object sender, MtNavigatingCancelEventArgs e)
        {
            _lastPageContent = this.RootFrame.Content;

            var ui = _lastPageContent as FrameworkElement;
            if (ui == null)
            {
                return;
            }
            var vm = ui.DataContext as ViewModelBase;
            if (vm == null)
            {
                return;
            }

            var args = new NavigatingFromEventArgs
            {
                Cancel = e.Cancel,
                NavigationMode = e.NavigationMode,
                Parameter = e.Parameter
            };

            vm.OnNavigatingFrom(args, null, false);            
        }

        private void RootFrame_Navigated(object sender, MtNavigationEventArgs e)
        {
            var ui = e.Content as FrameworkElement;
            if (ui == null)
            {
                return;
            }
            var vm = ui.DataContext as ViewModelBase;
            if (vm == null)
            {
                return;
            }

            var args = new NavigatedToEventArgs
            {
                NavigationMode = e.NavigationMode,
                Parameter = e.Parameter
            };

            vm.OnNavigatedTo(args, null);

            if (ui is MtPage)
            {
                var page = (MtPage)ui;

                if (page.BottomAppBar != null)
                {
                    page.BottomAppBar.DataContext = vm;
                }
                if (page.TopAppBar != null)
                {
                    page.TopAppBar.DataContext = vm;
                }
            }
        }

        public bool CanGoBack()
        {
            return this.RootFrame.CanGoBack;
        }

        public bool CanGoForward()
        {
            return this.RootFrame.CanGoForward;
        }

        public void ClearHistory()
        {
            throw new NotImplementedException();
        }

        public async void GoBack()
        {
            await this.RootFrame.GoBackAsync();
        }

        public async void GoForward()
        {
            await this.RootFrame.GoForwardAsync();
        }

        public bool Navigate(string pageToken, object parameter)
        {
            var pageType = this.GetPageType(pageToken);

            this.RootFrame.NavigateAsync(pageType, parameter);

            return true;
        }

        public void RemoveAllPages(string pageToken = null, object parameter = null)
        {
            throw new NotImplementedException();
        }

        public void RemoveFirstPage(string pageToken = null, object parameter = null)
        {
            throw new NotImplementedException();
        }

        public void RemoveLastPage(string pageToken = null, object parameter = null)
        {
            throw new NotImplementedException();
        }

        public void RestoreSavedNavigation()
        {
            throw new NotImplementedException();
        }

        public void Suspending()
        {
            throw new NotImplementedException();
        }
    }
}
