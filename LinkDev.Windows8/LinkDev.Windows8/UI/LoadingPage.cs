using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.Windows8.Data;
using Windows.UI.Popups;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Data;
using LinkDev.Windows8.Helpers;

namespace LinkDev.Windows8.UI
{
    /// <summary>
    /// A page that has a progress bar when any app feed is loading
    /// </summary>
    public class LoadingPage: Page
    {
        private FeedsCollection feeds = null;
        public FeedsCollection Feeds
        {
            get { return feeds; }
            set
            {
                feeds = value;

                BindingHelper.SetBinding(progress, ProgressBar.VisibilityProperty, feeds, "IsLoading", new BooleanToVisibilityConverter());
                BindingHelper.SetBinding(error, ProgressBar.VisibilityProperty, feeds, "IsError", new BooleanToVisibilityConverter());
                BindingHelper.SetBinding(offlineError, ProgressBar.VisibilityProperty, feeds, "IsOffline", new BooleanToVisibilityConverter());
            }
        }

        protected ProgressBar progress;
        protected Error error;
        protected OfflineError offlineError;

        public event EventHandler LoadingCompleted;

        public LoadingPage()
        {
            this.Loaded += (sender, e) =>
            {
                //ProgressBar
                progress = new ProgressBar();
                progress.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                progress.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                progress.IsIndeterminate = true;
                progress.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                progress.Width = 400;
                ((Grid)this.Content).Children.Add(progress);

                //Error
                error = new Error();
                error.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                error.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Right;
                error.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                error.Margin = new Thickness(0, 0, 30, 0);
                ((Grid)this.Content).Children.Add(error);

                //OfflineError
                offlineError = new OfflineError();
                offlineError.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                offlineError.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Right;
                offlineError.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                offlineError.Margin = new Thickness(0, 0, 130, 0);
                ((Grid)this.Content).Children.Add(offlineError);
            };
        }
    }
}
