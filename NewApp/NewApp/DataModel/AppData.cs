using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.Windows8.Data;
using LinkDev.Windows8.RSS;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace NewApp.DataModel
{
    /// <summary>
    /// This is the main application data container class, add here any app custom data collections
    /// </summary>
    public class AppData: AppDataBase
    {
        private static AppData _default = null;
        public static AppData Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new AppData();
                }

                return _default;
            }
            set
            {
                _default = value;
            }
        }

        static AppData()
        {
            AppDataType = typeof(AppData);

            //TODO: Add here any custom types that need to be serialized with the app data
            SerializedTypes = new Type[] { typeof(RssFeed), typeof(SampleFeed) };
        }

        public AppData()
        {

        }

        /// <summary>
        /// Invoked on app initialization
        /// </summary>
        public override void Initialize()
        {
            _default = this;

            //TODO: Customize this to add your app custom feeds
            //Add the feed if it doesn't exist already, it can exist if the application data is loaded from storage
            if (this.GetFeedByUrl("This is not required by the sample feed") == null)
            {
                SampleFeed feed = new SampleFeed();
                feed.Url = "This is not required by the sample feed";
                feed.UpdateInterval = TimeSpan.FromMinutes(30);
                Feeds.Add(feed);
            }

            //This step insure that all items have their group set properly
            //This is required especially when loading data from storage as this property is not serializable
            foreach (var group in ItemGroups)
            {
                foreach (var item in group.Items)
                {
                    item.Group = group;
                }
            }

            //Show tile notifications for the first feed
            Feeds[0].ShowTileNotifications = true;

            //Get the feeds data
            GetFeedsDataAsync();

            base.Initialize();
        }

        public void AddFeed(string url, string title)
        {
            foreach (var g in ItemGroups)
            {
                if (g.Title == title)
                {
                    return;
                }
            }

            //Not found
            DataGroup group = new DataGroup();
            group.SummaryItemsCount = 5;
            group.FirstItemGridWidth = 2;
            group.FirstItemGridHeight = 2;
            group.UniqueId = title;
            group.Title = title;
            ItemGroups.Add(group);

            RssFeed feed = new RssFeed();
            feed.Url = url;
            feed.UpdateInterval = TimeSpan.FromMinutes(30);
            feed.TargetData = group;
            Feeds.Add(feed);
        }

        /// <summary>
        /// The list of data groups displayed in this application
        /// TODO: should be customized for your application and you can add multiple collection if needed
        /// </summary>
        public ObservableCollection<DataGroup> ItemGroups
        {
            get { return this._itemGroups; }
        }
        private ObservableCollection<DataGroup> _itemGroups = new ObservableCollection<DataGroup>();

        /// <summary>
        /// Gets a group by its UniqueId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataGroup GetGroup(string id)
        {
            foreach (var group in ItemGroups)
            {
                if (group.UniqueId == id)
                {
                    return group;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets an item by its UniqueId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataItem GetItem(string id)
        {
            foreach (var group in ItemGroups)
            {
                foreach (var item in group.Items)
                {
                    if (item.UniqueId == id)
                    {
                        return item;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Register application settings commands.
        /// </summary>
        public void RegisterAppSettings()
        {
            SettingsPane.GetForCurrentView().CommandsRequested += AppData_CommandsRequested;
        }

        /// <summary>
        /// SettingsPane was requested.
        /// </summary>
        private void AppData_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            UICommandInvokedHandler handler = new UICommandInvokedHandler(onSettingsCommand);

            SettingsCommand aboutCommand = new SettingsCommand("about", "About", handler);
            args.Request.ApplicationCommands.Add(aboutCommand);
        }

        /// <summary>
        /// Invoked when a setting command is activated
        /// </summary>
        /// <param name="command"></param>
        private void onSettingsCommand(IUICommand command)
        {
            SettingsCommand settingsCommand = (SettingsCommand)command;

            switch (settingsCommand.Id.ToString())
            {
                case "about":
                    Popup popup = new Popup() { IsLightDismissEnabled = true };

                    About about = new About();
                    popup.Child = about;
                    Canvas.SetLeft(popup, ((Frame)Window.Current.Content).ActualWidth);
                    about.Height = ((Frame)Window.Current.Content).ActualHeight;

                    popup.IsOpen = true;
                    break;
            }
        }

        /// <summary>
        /// A helper method that is used to search in items
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ObservableCollection<DataItem> Search(string query)
        {
            ObservableCollection<DataItem> results = new ObservableCollection<DataItem>();

            foreach (var group in this.ItemGroups)
            {
                foreach (var item in group.Items)
                {
                    if (item.Title.ToLower().Contains(query.ToLower()))
                    {
                        results.Add(item);
                    }
                }
            }

            return results;
        }
    }
}
