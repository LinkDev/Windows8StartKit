using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace LinkDev.Windows8.Data
{
    public class FeedBase: BindableBase
    {
        private string _url = string.Empty;
        public string Url
        {
            get { return this._url; }
            set { this.SetProperty(ref this._url, value); }
        }

        private FeedState _state = FeedState.Pending;
        [XmlIgnore]
        public FeedState State
        {
            get { return this._state; }
            set { this.SetProperty(ref this._state, value); }
        }

        private object _targetData = null;
        public object TargetData
        {
            get { return this._targetData; }
            set { this.SetProperty(ref this._targetData, value); }
        }

        private string _errorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return this._errorMessage; }
            set { this.SetProperty(ref this._errorMessage, value); }
        }

        private bool _showTileNotifications = false;
        public bool ShowTileNotifications
        {
            get { return this._showTileNotifications; }
            set { this.SetProperty(ref this._showTileNotifications, value); }
        }

        private DateTime _lastUpdateTime = DateTime.MinValue;
        public DateTime LastUpdateTime
        {
            get { return this._lastUpdateTime; }
            set { this.SetProperty(ref this._lastUpdateTime, value); }
        }

        private TimeSpan _updateInterval = TimeSpan.MaxValue;
        public TimeSpan UpdateInterval
        {
            get { return this._updateInterval; }
            set { this.SetProperty(ref this._updateInterval, value); }
        }

        public virtual async Task GetFeedAsync()
        {
        }

        public virtual void RefreshTileNotifications(Collection<DataItem> items)
        {
            XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWideImageAndText01);

            TileUpdateManager.CreateTileUpdaterForApplication().EnableNotificationQueue(true);

            for (int i = 4; i >= 0; i--)
            {
                if (i <= items.Count)
                {
                    XmlNodeList tileTextAttributes = tileXml.GetElementsByTagName("text");

                    foreach (var tileTextAttr in tileTextAttributes)
                    {
                        tileTextAttr.InnerText = items[i].Title;
                    }

                    if (items[i].ImagePath != null)
                    {
                        XmlNodeList tileImageAttributes = tileXml.GetElementsByTagName("image");
                        ((XmlElement)tileImageAttributes[0]).SetAttribute("src", items[i].ImagePath);
                    }

                    TileNotification tileNotification = new TileNotification(tileXml);
                    TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
                }
            }
        }
    }
}
