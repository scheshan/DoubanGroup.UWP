using DoubanGroup.Client.Models;
using DoubanGroup.Core.Api.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Client.ViewModels
{
    public class CommentListViewModel : ViewModelBase
    {
        private int _page;

        public int Page
        {
            get { return _page; }
            set { this.SetProperty(ref _page, value); }
        }

        private int _pageSize;

        public int PageSize
        {
            get { return _pageSize; }
            set { this.SetProperty(ref _pageSize, value); }
        }

        private long _topicID;

        public long TopicID
        {
            get { return _topicID; }
            set { this.SetProperty(ref _topicID, value); }
        }

        public ObservableCollection<Comment> CommentList { get; private set; }

        public TopicDetailPageViewModel Topic { get; set; }
        
        public bool ShowTopic
        {
            get
            {
                return this.Page == 1;
            }
        }

        public bool ViewAuthor { get; private set; }

        public CommentListViewModel(long topicID, int page, int pageSize, bool viewAuthor)
        {
            this.Page = page;
            this.TopicID = topicID;
            this.PageSize = pageSize;
            this.ViewAuthor = viewAuthor;

            this.CommentList = new ObservableCollection<Comment>();
        }

        private bool _isLoaded;

        public bool IsLoaded
        {
            get { return _isLoaded; }
            set { this.SetProperty(ref _isLoaded, value); }
        }

        public async Task LoadData()
        {
            if(this.IsLoaded)
            {
                return;
            }

            this.IsLoaded = true;

            int start = (this.Page - 1) * this.PageSize;

            CommentList commentList;

            if (this.ViewAuthor)
            {
                commentList = await this.ApiClient.GetOpCommentList(this.TopicID, start, this.PageSize);
            }
            else
            {
                commentList = await this.ApiClient.GetCommentList(this.TopicID, start, this.PageSize);
            }

            foreach (var comment in commentList.Comments)
            {
                this.CommentList.Add(comment);
            }
        }
    }
}
