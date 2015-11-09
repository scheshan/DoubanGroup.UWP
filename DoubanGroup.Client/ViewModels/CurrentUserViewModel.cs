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
using Windows.Storage;
using System.Collections.ObjectModel;
using Prism.Events;

namespace DoubanGroup.Client.ViewModels
{
    public class CurrentUserViewModel : BindableBase, IAccessTokenProvider
    {
        private const string SESSION_KEY = "Cache_UserSession";

        private const string USER_KEY = "Cache_User";

        private User _user;

        public User User
        {
            get { return _user; }
            set { this.SetProperty(ref _user, value); }
        }

        private UserDetail _userDetail;

        public UserDetail UserDetail
        {
            get { return _userDetail; }
            set { this.SetProperty(ref _userDetail, value); }
        }

        public ObservableCollection<Group> JoinedGroupList { get; private set; }

        public ObservableCollection<Group> ManagedGroupList { get; private set; }

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

        private ApiClient ApiClient { get; set; }

        private IEventAggregator EventAggregator { get; set; }

        public CurrentUserViewModel()
        {
            this.ApiClient = new ApiClient(this);
            this.EventAggregator = App.Container.Resolve<IEventAggregator>();

            this.JoinedGroupList = new ObservableCollection<Group>();
            this.ManagedGroupList = new ObservableCollection<Group>();

            this.EventAggregator.GetEvent<Events.JoinGroupEvent>().Subscribe(this.OnJoinGroup);
            this.EventAggregator.GetEvent<Events.QuitGroupEvent>().Subscribe(this.OnQuitGroup);

            var session = ApplicationData.Current.LocalSettings.Get<Session>(SESSION_KEY);
            _session = session;

            if (this.IsLogin)
            {
                this.LoadUser();
            }
        }

        public void SetSession(Session session)
        {
            this.Session = session;

            if (this.IsLogin)
            {
                ApplicationData.Current.LocalSettings.Set(SESSION_KEY, session);
                this.LoadUser();
            }
            else
            {
                ApplicationData.Current.LocalSettings.Remove(SESSION_KEY);
            }
        }

        private async Task LoadUser()
        {
            var userDetail = await this.ApiClient.GetUserDetail(this.Session.DoubanUserID, 0);
            this.UserDetail = userDetail;
            this.User = userDetail.User;

            await this.LoadJoinedGroups();
            await this.LoadManagedGroups();
        }

        public async Task LoadJoinedGroups()
        {
            this.JoinedGroupList.Clear();

            var groupList = await this.ApiClient.GetUserJoinedGroups(this.User.ID, 0, this.UserDetail.JoinGroupCount);

            foreach (var group in groupList.Items)
            {
                this.JoinedGroupList.Add(group);
            }
        }

        public async Task LoadManagedGroups()
        {
            this.ManagedGroupList.Clear();

            var groupList = await this.ApiClient.GetUserManagedGroups(this.User.ID, 0, 200);

            foreach (var group in groupList.Items)
            {
                this.ManagedGroupList.Add(group);
            }
        }

        public bool IsGroupMember(long groupID)
        {
            return this.JoinedGroupList.Any(t => t.ID == groupID);
        }

        public void LogOff()
        {
            this.SetSession(null);
            this.JoinedGroupList.Clear();
            this.ManagedGroupList.Clear();
        }

        private void OnJoinGroup(Group group)
        {
            lock(this.JoinedGroupList)
            {
                this.JoinedGroupList.Add(group);
            }
        }

        private void OnQuitGroup(Group group)
        {
            lock(this.JoinedGroupList)
            {
                var list = this.JoinedGroupList.Where(t => t.ID == group.ID).ToList();

                foreach (var item in list)
                {
                    this.JoinedGroupList.Remove(item);
                }
            }
        }
    }
}
