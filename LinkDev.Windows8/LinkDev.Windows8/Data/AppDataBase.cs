using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;
using Windows.UI.Xaml;

namespace LinkDev.Windows8.Data
{
    /// <summary>
    /// The base class for application data container. Every app should inherit from this one and add its own data collections to it.
    /// </summary>
    public class AppDataBase : FeedsCollection
    {
        public static Type AppDataType { get; set; }
        public static Type[] SerializedTypes { get; set; }
        
        [XmlIgnore]
        public DispatcherTimer timer = new DispatcherTimer();

        public virtual void Initialize()
        {
            timer.Interval = TimeSpan.FromMinutes(1);
            timer.Start();
            timer.Tick += timer_Tick;
        }

        protected async void timer_Tick(object sender, object e)
        {
            await GetFeedsDataAsync();
        }

        public async Task SaveAsync()
        {
            try
            {
                StorageFolder folder = ApplicationData.Current.LocalFolder;

                using (Stream s = await folder.OpenStreamForWriteAsync("AppData.xml", CreationCollisionOption.ReplaceExisting))
                {
                    XmlSerializer serializer = new XmlSerializer(AppDataType, SerializedTypes);
                    serializer.Serialize(s, this);

                    s.Flush();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<AppDataBase> LoadAsync()
        {
            try
            {
                StorageFolder folder = ApplicationData.Current.LocalFolder;
                using (Stream s = await folder.OpenStreamForReadAsync("AppData.xml"))
                {
                    XmlSerializer serializer = new XmlSerializer(AppDataType, SerializedTypes);
                    AppDataBase data = (AppDataBase)serializer.Deserialize(s);

                    data.Initialize();

                    return data;
                }
            }
            catch (Exception ex)
            {
                AppDataBase appData = (AppDataBase)Activator.CreateInstance(AppDataType);
                appData.Initialize();

                return appData;
            }
        }
    }
}
