using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace LinkDev.Windows8.Data
{
    /// <summary>
    /// The base class for any app data class
    /// </summary>
    [Windows.Foundation.Metadata.WebHostHidden]
    public class DataCommon:BindableBase
    {
        protected static Uri _baseUri = new Uri("ms-appx:///");

        private string _uniqueId = string.Empty;
        public string UniqueId
        {
            get { return this._uniqueId; }
            set { this.SetProperty(ref this._uniqueId, value); }
        }

        private string _title = string.Empty;
        public string Title
        {
            get { return this._title; }
            set { this.SetProperty(ref this._title, value); }
        }

        private string _subtitle = string.Empty;
        public string Subtitle
        {
            get { return this._subtitle; }
            set { this.SetProperty(ref this._subtitle, value); }
        }

        private string _description = string.Empty;
        public string Description
        {
            get { return this._description; }
            set { this.SetProperty(ref this._description, value); }
        }

        private string _url = string.Empty;
        public string Url
        {
            get { return this._url; }
            set { this.SetProperty(ref this._url, value); }
        }

        [XmlIgnore]
        private ImageSource _image = null;
        private String _imagePath = null;

        public String ImagePath
        {
            get
            {
                return _imagePath;
            }
            set
            {
                SetImage(value);
            }
        }

        [XmlIgnore]
        public ImageSource Image
        {
            get
            {
                if (this._image == null && this._imagePath != null)
                {
                    this._image = new BitmapImage(new Uri(DataCommon._baseUri, this._imagePath));
                }
                return this._image;
            }

            set
            {
                this._imagePath = null;
                this.SetProperty(ref this._image, value);
            }
        }

        public void SetImage(String path)
        {
            this._image = null;
            this._imagePath = path;
            this.OnPropertyChanged("Image");
        }

        public DataCommon()
        {
            _uniqueId = Guid.NewGuid().ToString();
        }

        public virtual void UnloadImage()
        {
            if (_image != null)
            {
                ((BitmapImage)_image).UriSource = null;
                _image = null;
            }
        }

        public virtual void EnableShare()
        {
            try
            {
                DataTransferManager.GetForCurrentView().DataRequested -= DataRequested;
                DataTransferManager.GetForCurrentView().DataRequested += new TypedEventHandler<DataTransferManager, DataRequestedEventArgs>(this.DataRequested);
            }
            catch { }
        }

        public virtual void DisableShare()
        {
            DataTransferManager.GetForCurrentView().DataRequested -= DataRequested;
        }

        private void DataRequested(DataTransferManager sender, DataRequestedEventArgs e)
        {
            e.Request.Data.Properties.Title = this.Title;
            e.Request.Data.SetUri(new Uri(this.Url));
        }
    }
}
