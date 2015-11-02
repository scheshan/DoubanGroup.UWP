using DoubanGroup.Core.Api;
using DoubanGroup.Core.Api.Entity;
using Prism.Mvvm;
using Prism.Windows.AppModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using DoubanGroup.Client.Extensions;

namespace DoubanGroup.Client.ViewModels
{
    public class CurrentUserViewModel : BindableBase, IAccessTokenProvider
    {
        private const string SESSION_KEY = "UserSession";

        private User _user;

        public User User
        {
            get { return _user; }
            set { this.SetProperty(ref _user, value); }
        }

        private Session _session;

        public Session Session
        {
            get { return _session; }
            set
            {
                if (this.SetProperty(ref _session, value))
                {
                    this.OnPropertyChanged(() => this.IsLogin);
                }
            }
        }

        public string AccessToken
        {
            get
            {
                return this.Session?.AccessToken;
            }
        }

        public bool IsLogin
        {
            get
            {
                return this.Session != null;
            }
        }

        private Lazy<ApiClient> _apiClient = new Lazy<ApiClient>(() =>
        {
            return App.Current.Container.Resolve<ApiClient>();
        });

        private ApiClient ApiClient
        {
            get
            {
                return _apiClient.Value;
            }
        }

        private ISessionStateService SessionStateService { get; set; }

        public CurrentUserViewModel(ISessionStateService sessionStateService)
        {
            this.SessionStateService = sessionStateService;

            var session = sessionStateService.Get<Session>(SESSION_KEY);
            this.SetSession(session);
        }

        public void SetSession(Session session)
        {
            this.Session = session;

            if (this.IsLogin)
            {
                this.SessionStateService.Set(SESSION_KEY, this.Session);
                this.LoadUser();
            }
            else
            {
                this.SessionStateService.Remove(SESSION_KEY);
            }
        }

        private async Task LoadUser()
        {
            var userDetail = await this.ApiClient.GetUserDetail(this.Session.DoubanUserID, 0);
            this.User = userDetail.User;
        }
    }
}
