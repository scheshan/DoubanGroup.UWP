using DoubanGroup.Core.Api.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Client.ViewModels
{
    public class AddCommentPageViewModel : DialogViewModelBase<bool>
    {
        public Topic Topic { get; private set; }

        public Comment ReplyTo { get; private set; }

        public AddCommentPageViewModel(Topic topic, Comment replyTo)
            : base(typeof(Views.AddCommentPage))
        {
            this.Topic = topic;
            this.ReplyTo = replyTo;
        }
    }
}
