using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubanGroup.Client.Models
{
    public class ImageItem : BindableBase
    {
        private string _source;

        public string Source
        {
            get { return _source; }
            set { this.SetProperty(ref _source, value); }
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { this.SetProperty(ref _title, value); }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { this.SetProperty(ref _description, value); }
        }

        private double _width;

        public double Width
        {
            get { return _width; }
            set { this.SetProperty(ref _width, value); }
        }

        private double _height;

        public double Height
        {
            get { return _height; }
            set { this.SetProperty(ref _height, value); }
        }

        private object _sourceObject;

        public object SourceObject
        {
            get { return _sourceObject; }
            set { this.SetProperty(ref _sourceObject, value); }
        }
    }
}
