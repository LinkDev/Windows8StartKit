using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Windows8.Data
{
    /// <summary>
    /// A class representing a collection of feeds
    /// </summary>
    public class FeedsCollection: BindableBase
    {
        private ObservableCollection<FeedBase> _feeds = new ObservableCollection<FeedBase>();
        public ObservableCollection<FeedBase> Feeds
        {
            get { return this._feeds; }
        }

        private bool _isLoading = false;
        public bool IsLoading
        {
            get { return this._isLoading; }
            set
            {
                if (_isLoading != value)
                    this.SetProperty(ref this._isLoading, value);
            }
        }

        private bool _isError = false;
        public bool IsError
        {
            get { return this._isError; }
            set
            {
                if (_isError != value)
                    this.SetProperty(ref this._isError, value);
            }
        }

        private DateTime _lastUpdateTime = DateTime.MinValue;
        public DateTime LastUpdateTime
        {
            get { return this._lastUpdateTime; }
            set { this.SetProperty(ref this._lastUpdateTime, value); }
        }

        public FeedsCollection()
        {
            _feeds.CollectionChanged += _feeds_CollectionChanged;
        }

        private void _feeds_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (FeedBase feed in e.NewItems)
                {
                    feed.PropertyChanged += feed_PropertyChanged;
                }
            }

            if (e.OldItems != null)
            {
                foreach (FeedBase oldFeed in e.OldItems)
                {
                    oldFeed.PropertyChanged -= feed_PropertyChanged;
                }
            }
        }

        private void feed_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "State")
            {
                bool oneLoading = false;
                bool oneError = false;
                foreach (var feed in _feeds)
                {
                    if (feed.State == FeedState.Loading)
                    {
                        oneLoading = true;
                    }

                    if (feed.State == FeedState.Error)
                    {
                        oneError = true;
                    }
                }

                if (oneLoading == false)
                    IsLoading = false;
                else
                    IsLoading = true;

                if (oneError == false)
                    IsError = false;
                else
                    IsError = true;
            }
        }

        /// <summary>
        /// Gets the data for all feeds that are pending update
        /// </summary>
        /// <returns></returns>
        public async Task GetFeedsDataAsync()
        {
            List<Task> requests = new List<Task>();

            //Requesting pending feeds
            foreach (var feed in Feeds)
            {
                if ((feed.State == FeedState.Pending) || (feed.State == FeedState.Loaded && feed.UpdateInterval != TimeSpan.MaxValue && feed.LastUpdateTime + feed.UpdateInterval < DateTime.Now))
                    requests.Add(feed.GetFeedAsync());
            }

            //Waiting
            foreach (var request in requests)
            {
                await request;
            }

            //Update Last Updated Time for the collection
            foreach (var feed in Feeds)
            {
                if (feed.LastUpdateTime > LastUpdateTime)
                    LastUpdateTime = feed.LastUpdateTime;
            }
        }

        /// <summary>
        /// Gets a feed by url, returns null if not found
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public FeedBase GetFeedByUrl(string url)
        {
            foreach (var feed in Feeds)
            {
                if (feed.Url == url)
                    return feed;
            }

            return null;
        }
    }
}
