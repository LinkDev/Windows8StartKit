using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LinkDev.Windows8.Data;
using Windows.Data.Xml.Dom;


namespace LinkDev.Windows8.RSS
{
    public class RssFeed : FeedBase
    {
        public override async Task GetFeedAsync()
        {
            try
            {
                this.State = FeedState.Loading;

                if (Windows.Networking.Connectivity.NetworkInformation.GetInternetConnectionProfile()!=null && Windows.Networking.Connectivity.NetworkInformation.GetInternetConnectionProfile().GetNetworkConnectivityLevel() == Windows.Networking.Connectivity.NetworkConnectivityLevel.InternetAccess)
                {

                    DataGroup group = null;
                    if (TargetData == null)
                    {
                        TargetData = new DataGroup();
                    }
                    group = TargetData as DataGroup;

                    XmlDocument doc = await XmlDocument.LoadFromUriAsync(new Uri(Url));

                    //Empty group items, in case we are refreshing the list
                    group.Items.Clear();

                    IXmlNode node = null;
                    node = doc.SelectSingleNode("rss/channel/title");

                    int count = 0;

                    foreach (var item in doc.SelectSingleNode("rss/channel").ChildNodes.Where(n => n.NodeName == "item"))
                    {
                        DataItem feedItem = new DataItem();
                        feedItem.Title = item.SelectSingleNode("title").InnerText;
                        feedItem.Group = group;

                        string itemXML = item.GetXml();

                        feedItem.Content = Regex.Replace(item.SelectSingleNode("description").InnerText, @"<(.|\n)*?>", string.Empty).Replace("&#160;", "").Replace("&nbsp;", " ");

                        if (feedItem.Content.Length > 100)
                            feedItem.Description = feedItem.Content.Substring(0, 100) + "...";
                        else
                            feedItem.Description = feedItem.Content;

                        feedItem.Url = feedItem.UniqueId = item.SelectSingleNode("link").InnerText;

                        //Try to find the image in non standard RSS feeds
                        //TODO: to be fixed per app
                        Match m = Regex.Match(itemXML.ToLower(), "<image>(.*.jpg)</image>");
                        if (m.Success)
                        {
                            string image = m.Groups[1].Value;
                            image = image.Replace("_s2.jpg", "_s4.jpg").Replace("media_thumbnail", "highslide_zoom").Replace("/small/", "/large/").Replace("sites/", "http://www.almasryalyoum.com/sites/");
                            feedItem.SetImage(image);
                            feedItem.HasImage = true;
                        }
                        else
                        {
                            Match m2 = Regex.Match(itemXML, "(src|url)=(\"|\')([^<]*\\.jpg)");
                            if (m2.Success)
                            {
                                string image = m2.Groups[3].Value;
                                image = image.Replace("_s2.jpg", "_s4.jpg").Replace("media_thumbnail", "highslide_zoom").Replace("/small/", "/large/").Replace("sites/", "http://www.almasryalyoum.com/sites/");
                                feedItem.SetImage(image);
                            }
                        }

                        group.Items.Add(feedItem);
                        count++;
                    }

                    group.PrepareGroupForBinding();
                    this.LastUpdateTime = DateTime.Now;

                    if (group.Items.Count == 0)
                    {
                        this.ErrorMessage = "There are no items in this list.";
                        this.State = FeedState.Empty;
                    }
                    else
                    {
                        this.ErrorMessage = string.Empty;
                        this.State = FeedState.Loaded;

                        if (ShowTileNotifications)
                            RefreshTileNotifications(group.Items);
                    }
                }
                else
                {
                    this.ErrorMessage = "لا يوجد اتصال بالانترنت.";
                    this.State = FeedState.Error;
                }
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
                this.State = FeedState.Error;
            }
        }
    }
}
