using DoubanGroup.Core.Api.Entity;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Client.Models
{
    public class ChannelModel : BindableBase
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { this.SetProperty(ref _name, value); }
        }

        private string _nameCN;

        public string NameCN
        {
            get { return _nameCN; }
            set { this.SetProperty(ref _nameCN, value); }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { this.SetProperty(ref _description, value); }
        }

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set { this.SetProperty(ref _isSelected, value); }
        }

        public ChannelModel(Channel channel)
        {
            this.Name = channel.Name;
            this.NameCN = channel.NameCN;
            this.Description = channel.Description;            
        }
    }
}
