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

namespace LinkDev.Windows8.UI
{
    public class LoadingPage: Page
    {
        private FeedsCollection feeds = null;
        public FeedsCollection Feeds
        {
            get { return feeds; }
            set
            {
                feeds = value;
                feeds.PropertyChanged += feeds_PropertyChanged;

                feeds_PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("IsLoading"));
                feeds_PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("IsError"));
            }
        }

        protected ProgressBar progress;
        protected bool isError = false;

        public event EventHandler LoadingCompleted;

        public LoadingPage()
        {
            this.Loaded += (sender, e) =>
            {
                progress = new ProgressBar();
                progress.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                progress.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                progress.Width = 400;

                ((Grid)this.Content).Children.Add(progress);
            };
        }

        private void feeds_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsLoading":
                    progress.IsIndeterminate = feeds.IsLoading;
                    if (feeds.IsLoading == true)
                        progress.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    else
                    {
                        progress.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        if (LoadingCompleted != null)
                            LoadingCompleted(this, null);
                    }
                    break;

                case "IsError":
                    if (feeds.IsError != this.isError)
                    {
                        this.isError = feeds.IsError;
                        if (this.isError == true)
                        {
                            ShowErrorAsync();
                        }
                    }
                    break;

                default:
                    break;
            }
        }

        private async void ShowErrorAsync()
        {
            try
            {
                MessageDialog msg = new MessageDialog("لقد حدث خطأ فى التحميل. من فضلك تأكد من اتصالك بالانترنت ثم حاول مرة اخرى.");
                msg.Title = "حدث خطأ";
                msg.Commands.Add(new UICommand("موافق"));

                await msg.ShowAsync();
                feeds.IsError = false;
            }
            catch { }
        }
    }
}
