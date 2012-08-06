using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using System.Threading.Tasks;
using Windows.Web.Syndication;
using System.Text.RegularExpressions;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.Storage;
using System.Xml.Serialization;
using System.Net;
using System.IO;

// The data model defined by this file serves as a representative example of a strongly-typed
// model that supports notification when members are added, removed, or modified.  The property
// names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs.

namespace NewsApp.Data
{
    /// <summary>
    /// Base class for <see cref="RssDataItem"/> and <see cref="RssDataGroup"/> that
    /// defines properties common to both.
    /// </summary>
    [Windows.Foundation.Metadata.WebHostHidden]
    public abstract class RssDataCommon : NewsApp.Common.BindableBase
    {
        private static Uri _baseUri = new Uri("ms-appx:///");

        public RssDataCommon()
        {
        }

        public RssDataCommon(String uniqueId, String title, String subtitle, String imagePath, String description)
        {
            this._uniqueId = uniqueId;
            this._title = title;
            this._subtitle = subtitle;
            this._description = description;
            this._imagePath = imagePath;
        }

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
                    this._image = new BitmapImage(new Uri(RssDataCommon._baseUri, this._imagePath));
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
    }

    /// <summary>
    /// Generic item data model.
    /// </summary>
    public class RssDataItem : RssDataCommon
    {
        public RssDataItem(String uniqueId, String title, String subtitle, String imagePath, String description, String content, RssDataGroup group)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
            this._content = content;
            this._group = group;
        }

        public RssDataItem()
        {
            // TODO: Complete member initialization
        }

        private string _content = string.Empty;
        public string Content
        {
            get { return this._content; }
            set { this.SetProperty(ref this._content, value); }
        }

        private RssDataGroup _group;
        public RssDataGroup Group
        {
            get { return this._group; }
            set { this.SetProperty(ref this._group, value); }
        }
    }

    /// <summary>
    /// Generic group data model.
    /// </summary>
    public class RssDataGroup : RssDataCommon
    {
        public RssDataGroup(String uniqueId, String title, String subtitle, String imagePath, String description)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
        }

        public RssDataGroup()
        {
            
        }

        private ObservableCollection<RssDataItem> _items = new ObservableCollection<RssDataItem>();
        public ObservableCollection<RssDataItem> Items
        {
            get { return this._items; }
        }

        private ObservableCollection<RssDataItem> _itemsSummary = new ObservableCollection<RssDataItem>();
        public ObservableCollection<RssDataItem> ItemsSummary
        {
            get { return this._itemsSummary; }
        }
    }

    /// <summary>
    /// Creates a collection of groups and items with hard-coded content.
    /// </summary>
    public sealed class RssDataSource
    {
        private ObservableCollection<RssDataGroup> _itemGroups = new ObservableCollection<RssDataGroup>();
        public ObservableCollection<RssDataGroup> ItemGroups
        {
            get { return this._itemGroups; }
        }

        private ObservableCollection<RssDataGroup> _tours = new ObservableCollection<RssDataGroup>();
        public ObservableCollection<RssDataGroup> Tours
        {
            get { return this._tours; }
        }

        public static int MaxItemsInGroup { get; set; }
        public static RssDataSource Default = null;

        public event EventHandler LoadingCompleted;
        public event EventHandler LoadingError;

        static RssDataSource()
        {
            MaxItemsInGroup = 50;
        }

        public RssDataSource(string[] feeds, string[] tours = null)
        {
            Default = this;
            initialize(feeds, tours);
        }

        private async Task initialize(string[] feeds, string[] tours)
        {
            //await GetFeedsFromCache();
            GetFeedsAsync(feeds, tours);
        }

        public async Task GetFeedsFromCache()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<RssDataGroup>));
                StorageFile feedsFile = await ApplicationData.Current.TemporaryFolder.GetFileAsync("Feeds.xml");
                string input = await FileIO.ReadTextAsync(feedsFile);
                System.IO.MemoryStream m = new System.IO.MemoryStream();
                System.IO.StreamWriter writer = new System.IO.StreamWriter(m);
                writer.Write(input);
                m.Seek(0, System.IO.SeekOrigin.Begin);
                ObservableCollection<RssDataGroup> items = (ObservableCollection<RssDataGroup>)serializer.Deserialize(m);
                foreach (var item in items)
                {
                    _itemGroups.Add(item);
                }
            }
            catch (Exception ex)
            {
                if (LoadingError != null)
                {
                    LoadingError(this, null);
                }
            }
        }

        public async Task GetFeedsAsync(string[] feeds, string[] tours)
        {
            //Request
            List<Task<RssDataGroup>> list = new List<Task<RssDataGroup>>();
            foreach (var feed in feeds)
            {
                list.Add(GetFeedAsync(feed));
            }

            List<Task<List<RssDataGroup>>> listOfTours = new List<Task<List<RssDataGroup>>>();
            if (tours != null)
            {
                foreach (var tour in tours)
                {
                    listOfTours.Add(GetMatchesFeedAsync(tour));
                }
            }
            
            //Waiting
            foreach (var item in list)
            {
                this.ItemGroups.Add(await item);
                if (item.Result!=null && LoadingCompleted != null)
                {
                    LoadingCompleted(this, null);
                }
            }

            if (tours != null)
            {
                foreach (var item in listOfTours)
                {
                    foreach (var node in await item)
                    {
                        this.Tours.Add(node);
                    }

                    if (item.Result != null && LoadingCompleted != null)
                    {
                        LoadingCompleted(this, null);
                    }
                }
            }

            //Store Cache: Didn't work
            /*XmlSerializer serializer=new XmlSerializer(typeof(ObservableCollection<RssDataGroup>));
            StorageFile feedsFile = await ApplicationData.Current.TemporaryFolder.CreateFileAsync("Feeds.xml", CreationCollisionOption.ReplaceExisting);
            System.IO.MemoryStream m = new System.IO.MemoryStream();
            serializer.Serialize(m, ItemGroups);
            m.Seek(0, System.IO.SeekOrigin.Begin);
            System.IO.StreamReader reader = new System.IO.StreamReader(m);
            string output=reader.ReadToEnd();
            await FileIO.WriteTextAsync(feedsFile, output);*/
            

            //Tile Notifications
            if (ItemGroups.Count > 0)
            {
                XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWideImageAndText01);

                TileUpdateManager.CreateTileUpdaterForApplication().EnableNotificationQueue(true);

                for (int i = 4; i >= 0; i--)
                {
                    if (i <= ItemGroups[0].Items.Count)
                    {
                        XmlNodeList tileTextAttributes = tileXml.GetElementsByTagName("text");

                        foreach (var tileTextAttr in tileTextAttributes)
                        {
                            tileTextAttr.InnerText = ItemGroups[0].Items[i].Title;
                        }

                        XmlNodeList tileImageAttributes = tileXml.GetElementsByTagName("image");
                        ((XmlElement)tileImageAttributes[0]).SetAttribute("src", ItemGroups[0].Items[i].ImagePath);
                        

                        TileNotification tileNotification = new TileNotification(tileXml);
                        TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
                    }
                }
            }
        }

        private async Task<List<RssDataGroup>> GetMatchesFeedAsync(string feedUriString)
        {
            try
            {
                XmlDocument doc = await XmlDocument.LoadFromUriAsync(new Uri(feedUriString));

                List<RssDataGroup> list=new List<RssDataGroup>();
                

                foreach (var node in doc.ChildNodes[1].ChildNodes[0].ChildNodes.Where(n=>n.NodeName=="Tour"))
                {
                    RssDataGroup feedData = new RssDataGroup();
                    feedData.Title = node.SelectSingleNode("TourName").InnerText;

                    foreach (var match in node.ChildNodes.Where(n => n.NodeName == "match"))
	                {
		                RssDataItem feedItem = new RssDataItem();
                        feedItem.UniqueId = match.SelectSingleNode("Matchid").InnerText;
                        feedItem.Title = match.SelectSingleNode("team1").InnerText + " - " + match.SelectSingleNode("team2").InnerText;
                        if (match.SelectSingleNode("Team1Score").InnerText == "-" && match.SelectSingleNode("Team2Score").InnerText == "-")
                            feedItem.Description = DateTime.Parse(match.SelectSingleNode("timestamp").InnerText).ToString("hh:mm tt");
                        else
                            feedItem.Description = match.SelectSingleNode("Team1Score").InnerText + " - " + match.SelectSingleNode("Team2Score").InnerText;
                        feedItem.Group = feedData;

                        feedData.Items.Add(feedItem);
                        feedData.ItemsSummary.Add(feedItem);
                    }

                    list.Add(feedData);
                }

                return list;
            }
            catch (Exception ex)
            {
                if (LoadingError != null)
                {
                    LoadingError(this, null);
                }
                return null;
            }
        }

        private async Task<RssDataGroup> GetFeedAsync(string feedUriString)
        {
            try
            {
                XmlDocument doc = await XmlDocument.LoadFromUriAsync(new Uri(feedUriString));

                // This code is executed after RetrieveFeedAsync returns the SyndicationFeed.
                // Process it and copy the data we want into our FeedData and FeedItem classes.
                RssDataGroup feedData = new RssDataGroup();
                
                IXmlNode node = null;
                node = doc.SelectSingleNode("rss/channel/title");

                if (node != null)
                    feedData.Title = Regex.Replace(node.InnerText, "[a-zA-Z0-9\\.]*", "").Trim();

                if (string.IsNullOrEmpty(feedData.Title))
                    feedData.Title = "أخبار";

                int count = 0;

                foreach (var item in doc.SelectSingleNode("rss/channel").ChildNodes.Where(n => n.NodeName == "item"))
                {
                    RssDataItem feedItem = new RssDataItem();
                    feedItem.Title = item.SelectSingleNode("title").InnerText;
                    feedItem.Group = feedData;

                    string itemXML = item.GetXml();

                    feedItem.Content = Regex.Replace(item.SelectSingleNode("description").InnerText, @"<(.|\n)*?>", string.Empty).Replace("&#160;", "");
                    
                    if (feedItem.Content.Length > 100)
                        feedItem.Description = feedItem.Content.Substring(0, 100) + "...";
                    else
                        feedItem.Description = feedItem.Content;

                    feedItem.Url = item.SelectSingleNode("link").InnerText;

                    //Try to find the image in non standard RSS feeds
                    Match m = Regex.Match(itemXML, "<image>(.*.jpg)</image>");
                    if (m.Success)
                    {
                        string image = m.Groups[1].Value;
                        image = image.Replace("_s2.jpg", "_s4.jpg").Replace("media_thumbnail", "highslide_zoom").Replace("/small/","/large/").Replace("sites/","http://www.almasryalyoum.com/sites/");
                        feedItem.SetImage(image);
                    }
                    else
                    {
                        Match m2 = Regex.Match(itemXML, "(src|url)=(\"|\')(.*.jpg)");
                        if (m2.Success)
                        {
                            string image = m2.Groups[3].Value;
                            image = image.Replace("_s2.jpg", "_s4.jpg").Replace("media_thumbnail", "highslide_zoom").Replace("/small/", "/large/").Replace("sites/", "http://www.almasryalyoum.com/sites/");
                            feedItem.SetImage(image);
                        }
                    }

                    feedData.Items.Add(feedItem);
                    if (count < MaxItemsInGroup)
                    {
                        feedData.ItemsSummary.Add(feedItem);
                    }
                    count++;
                }

                return feedData;
            }
            catch (Exception)
            {
                if (LoadingError != null)
                {
                    LoadingError(this, null);
                }
                return null;
            }
        }
    }
}
