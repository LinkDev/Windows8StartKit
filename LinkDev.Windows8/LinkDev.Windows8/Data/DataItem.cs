using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace LinkDev.Windows8.Data
{
    public class DataItem: DataCommon
    {
        private string _content = string.Empty;
        public string Content
        {
            get { return this._content; }
            set { this.SetProperty(ref this._content, value); }
        }

        private int _gridWidth = 1;
        public int GridWidth
        {
            get { return this._gridWidth; }
            set { this.SetProperty(ref this._gridWidth, value); }
        }

        private int _gridHeight = 1;
        public int GridHeight
        {
            get { return this._gridHeight; }
            set { this.SetProperty(ref this._gridHeight, value); }
        }

        public int Width
        {
            get { return this._gridWidth * 250; }
        }

        public int Height
        {
            get { return this._gridHeight * 150; }
        }

        private bool _hasImage = false;
        public bool HasImage
        {
            get { return this._hasImage; }
            set { this.SetProperty(ref this._hasImage, value); }
        }

        private bool _isMore = false;
        public bool IsMore
        {
            get { return this._isMore; }
            set { this.SetProperty(ref this._isMore, value); }
        }
        

        private DataGroup _group;
        [XmlIgnore]
        public DataGroup Group
        {
            get { return this._group; }
            set { this.SetProperty(ref this._group, value); }
        }
    }
}
