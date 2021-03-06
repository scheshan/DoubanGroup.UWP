﻿using DoubanGroup.Core.Api.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Windows.Navigation;
using DoubanGroup.Client.Models;
using DoubanGroup.Core.Api;
using Prism.Commands;
using Windows.UI.Xaml.Navigation;
using DoubanGroup.Client.CacheItem;
using NotificationsExtensions.Tiles;
using Windows.UI.Notifications;
using Windows.UI.StartScreen;

namespace DoubanGroup.Client.ViewModels
{
    public class GroupDetailPageViewModel : NavigationViewModelBase
    {
        private long _groupID;

        public long GroupID
        {
            get { return _groupID; }
            set { this.SetProperty(ref _groupID, value); }
        }

        private Group _group;

        public Group Group
        {
            get { return _group; }
            set { this.SetProperty(ref _group, value); }
        }

        public bool IsGroupMember
        {
            get
            {
                return this.CurrentUser.IsGroupMember(this.GroupID);
            }
        }

        public RefreshableViewModel<Topic> GroupTopicsViewModel { get; private set; }

        public RefreshableViewModel<User> GroupUsersViewModel { get; private set; }

        public GroupDetailPageViewModel()
        {

        }

        public override async void OnNavigatedTo(NavigatedToEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.GroupID = (long)e.Parameter;

            if (e.NavigationMode == NavigationMode.New)
            {
                this.LoadGroup();
                this.GroupTopicsViewModel = new RefreshableViewModel<Topic>(this.LoadTopics, 30);
                this.GroupUsersViewModel = new RefreshableViewModel<User>(this.LoadUsers, 100);
            }
        }

        private async Task<IEnumerable<Topic>> LoadTopics(int start, int count)
        {
            var topicList = await this.ApiClient.GetGroupTopics(this.GroupID, start, count);
            return topicList.Items;
        }

        private async Task<IEnumerable<User>> LoadUsers(int start, int count)
        {
            var userList = await this.ApiClient.GetGroupMembers(this.GroupID, start, count);
            return userList.Items;
        }

        private async Task LoadGroup()
        {
            var group = await this.RunTaskAsync(this.ApiClient.GetGroup(this.GroupID));

            if (group == null)
            {
                this.ShowToast("小组加载失败");
                this.NavigationService.GoBack();
                return;
            }

            this.Group = group;
            this.OnPropertyChanged(() => this.IsGroupMember);
        }

        private DelegateCommand _joinGroupCommand;

        public DelegateCommand JoinGroupCommand
        {
            get
            {
                if (_joinGroupCommand == null)
                {
                    _joinGroupCommand = new DelegateCommand(this.JoinGroup);
                }
                return _joinGroupCommand;
            }
        }

        private async void JoinGroup()
        {
            if (!await this.RequireLogin())
            {
                return;
            }

            if (this.IsLoading)
            {
                return;
            }

            if (this.Group.JoinType == "A")
            {
                if (await this.RunTaskAsync(this.ApiClient.JoinGroup(this.GroupID)))
                {
                    this.ShowToast("加入小组成功");
                    this.EventAggretator.GetEvent<Events.JoinGroupEvent>().Publish(this.Group);
                    this.OnPropertyChanged(() => this.IsGroupMember);
                }
            }
            else
            {

            }
        }

        private DelegateCommand _quitGroupCommand;

        public DelegateCommand QuitGroupCommand
        {
            get
            {
                if (_quitGroupCommand == null)
                {
                    _quitGroupCommand = new DelegateCommand(QuitGroup);
                }
                return _quitGroupCommand;
            }
        }

        private async void QuitGroup()
        {
            if (!await this.RequireLogin())
            {
                return;
            }

            if (this.IsLoading)
            {
                return;
            }

            if (!await this.Confirm("确认退出该小组?"))
            {
                return;
            }

            if (await this.RunTaskAsync(this.ApiClient.QuitGroup(this.GroupID)))
            {
                this.ShowToast("退出小组成功");
                this.EventAggretator.GetEvent<Events.QuitGroupEvent>().Publish(this.Group);
                this.OnPropertyChanged(() => this.IsGroupMember);
            }
        }

        private DelegateCommand _pinCommand;

        public DelegateCommand PinCommand
        {
            get
            {
                if (_pinCommand == null)
                {
                    _pinCommand = new DelegateCommand(this.Pin);
                }
                return _pinCommand;
            }
        }

        private async void Pin()
        {
            if (this.Group == null)
            {
                return;
            }

            string tileID = $"Tile_Group_{this.GroupID}";
            string argument = $"GroupDetail-{this.GroupID}";

            SecondaryTile tile = new SecondaryTile(tileID, this.Group.Name, argument, new Uri("ms-appx:///Assets/logo-50.png"), TileSize.Square150x150);
            await tile.RequestCreateAsync();

            TileBindingContentAdaptive bindingContent = new TileBindingContentAdaptive()
            {
                PeekImage = new TilePeekImage()
                {
                    Source = new TileImageSource(this.Group.LargeAvatar)                    
                }                
            };
            bindingContent.Children.Add(new TileText
            {
                Text = this.Group.Name,
                Wrap = true,
                Style = TileTextStyle.Body
            });

            TileBinding binding = new TileBinding()
            {
                Branding = TileBranding.NameAndLogo,
                DisplayName = this.Group.Name,
                Content = bindingContent                
            };            

            TileContent content = new TileContent()
            {
                Visual = new TileVisual()
                {
                    TileMedium = binding,
                    TileWide = binding,
                    TileLarge = binding                    
                }
            };

            var notification = new TileNotification(content.GetXml());

            var updater = TileUpdateManager.CreateTileUpdaterForSecondaryTile(tileID);
            updater.Update(notification);

            bool result = await tile.UpdateAsync();
        }

        private DelegateCommand _addTopicCommand;

        public DelegateCommand AddTopicCommand
        {
            get
            {
                if (_addTopicCommand == null)
                {
                    _addTopicCommand = new DelegateCommand(AddTopic);
                }
                return _addTopicCommand;
            }
        }

        private void AddTopic()
        {
            this.NavigationService.Navigate("AddTopic", this.GroupID);
        }
    }
}
