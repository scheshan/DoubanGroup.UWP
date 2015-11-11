using DoubanGroup.Core.Api;
using DoubanGroup.Core.Api.Entity;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Client.ViewModels
{
    public class AddCommentPageViewModel : DialogViewModelBase<Comment>
    {
        public Topic Topic { get; private set; }

        public Comment ReplyTo { get; private set; }

        private string _content;

        public string Content
        {
            get { return _content; }
            set { this.SetProperty(ref _content, value); }
        }

        public AddCommentPageViewModel(Topic topic, Comment replyTo)
            : base(typeof(Views.AddCommentPage))
        {
            this.Topic = topic;
            this.ReplyTo = replyTo;
        }

        private DelegateCommand _submitCommand;

        public DelegateCommand SubmitCommand
        {
            get
            {
                if (_submitCommand == null)
                {
                    _submitCommand = new DelegateCommand(this.Submit);
                }
                return _submitCommand;
            }
        }

        private async void Submit()
        {
            if (string.IsNullOrWhiteSpace(this.Content))
            {
                this.Alert("评论内容不能为空!");
                return;
            }

            if (this.IsLoading)
            {
                return;
            }

            var comment = await this.RunTaskAsync(this.ApiClient.AddComment(this.Topic.ID, this.ReplyTo?.ID, this.Content));

            if (comment != null)
            {
                this.DialogResult = comment;
                this.Hide();
                this.ShowToast("评论发表成功");
            }
        }
    }
}
