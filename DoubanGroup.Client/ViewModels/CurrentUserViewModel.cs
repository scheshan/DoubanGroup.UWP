using DoubanGroup.Core.Api;
using DoubanGroup.Core.Api.Entity;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Client.ViewModels
{
    public class CurrentUserViewModel : BindableBase, IAccessTokenProvider
    {
        private User _user;

        public User User
        {
            get { return _user; }
            set { this.SetProperty(ref _user, value); }
        }

        private string _accessToken;

        public string AccessToken
        {
            get { return _accessToken; }
            private set { this.SetProperty(ref _accessToken, value); }
        }

        public bool IsLogin
        {
            get
            {
                return this.AccessToken != null;
            }
        }
    }
}
