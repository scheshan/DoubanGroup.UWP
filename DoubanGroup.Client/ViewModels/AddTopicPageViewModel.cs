using DoubanGroup.Core.Api.Entity;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Media.Imaging;
using Prism.Windows.Navigation;

namespace DoubanGroup.Client.ViewModels
{
    public class AddTopicPageViewModel : NavigationViewModelBase
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            set { this.SetProperty(ref _title, value); }
        }

        private string _content;

        public string Content
        {
            get { return _content; }
            set { this.SetProperty(ref _content, value); }
        }

        private long _groupID;

        public long GroupID
        {
            get { return _groupID; }
            set { this.SetProperty(ref _groupID, value); }
        }

        private StorageFile _imageFile;

        public StorageFile ImageFile
        {
            get { return _imageFile; }
            set
            {
                if (this.SetProperty(ref _imageFile, value))
                {
                    this.SetImageSource();

                    if (!this.Content.Contains("<图片1>"))
                    {
                        this.Content = this.Content + "\r\n" + "<图片1>";
                    }
                }
            }
        }

        private BitmapImage _imageSource;

        public BitmapImage ImageSource
        {
            get { return _imageSource; }
            set { this.SetProperty(ref _imageSource, value); }
        }

        public AddTopicPageViewModel()
        {

        }

        public override void OnNavigatedTo(NavigatedToEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.GroupID = Convert.ToInt64(e.Parameter);
        }

        private DelegateCommand _chooseImageCommand;

        public DelegateCommand ChooseImageCommand
        {
            get
            {
                if (_chooseImageCommand == null)
                {
                    _chooseImageCommand = new DelegateCommand(ChooseImage);
                }
                return _chooseImageCommand;
            }
        }        

        private async void ChooseImage()
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            var file = await picker.PickSingleFileAsync();

            if (file != null)
            {
                this.ImageFile = file;
            }
        }

        private async Task SetImageSource()
        {
            if (this.ImageFile == null)
            {
                this.ImageSource = null;
            }
            else
            {
                using (var stream = await this.ImageFile.OpenReadAsync())
                {
                    this.ImageSource = new BitmapImage();
                    this.ImageSource.SetSource(stream);
                }
            }
        }

        private DelegateCommand _submitCommand;

        public DelegateCommand SubmitCommand
        {
            get
            {
                if (_submitCommand == null)
                {
                    _submitCommand = new DelegateCommand(Submit);
                }
                return _submitCommand;
            }
        }

        private async void Submit()
        {
            if (string.IsNullOrWhiteSpace(this.Title))
            {
                this.Alert("请填写标题");
                return;
            }
            if (string.IsNullOrWhiteSpace(this.Content))
            {
                this.Alert("请填写标题");
                return;
            }

            if (this.IsLoading)
            {
                return;
            }

            Topic topic;

            if (this.ImageFile != null)
            {
                topic = await this.RunTaskAsync(this.ApiClient.AddTopic(this.GroupID, this.Title, this.Content, this.ImageFile));
            }
            else
            {
                topic = await this.RunTaskAsync(this.ApiClient.AddTopic(this.GroupID, this.Title, this.Content));
            }

            if (topic != null)
            {
                this.NavigationService.GoBack();
                this.ShowToast("发表成功");
            }
        }
    }
}
