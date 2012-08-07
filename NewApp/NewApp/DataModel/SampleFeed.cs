using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.Windows8.Data;

namespace NewApp.DataModel
{
    public class SampleFeed:FeedBase
    {
        public override async Task GetFeedAsync()
        {
            try
            {
                this.State = FeedState.Loading;

                String ITEM_CONTENT = String.Format("Item Content: {0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}",
                            "Curabitur class aliquam vestibulum nam curae maecenas sed integer cras phasellus suspendisse quisque donec dis praesent accumsan bibendum pellentesque condimentum adipiscing etiam consequat vivamus dictumst aliquam duis convallis scelerisque est parturient ullamcorper aliquet fusce suspendisse nunc hac eleifend amet blandit facilisi condimentum commodo scelerisque faucibus aenean ullamcorper ante mauris dignissim consectetuer nullam lorem vestibulum habitant conubia elementum pellentesque morbi facilisis arcu sollicitudin diam cubilia aptent vestibulum auctor eget dapibus pellentesque inceptos leo egestas interdum nulla consectetuer suspendisse adipiscing pellentesque proin lobortis sollicitudin augue elit mus congue fermentum parturient fringilla euismod feugiat");

                AppData.Default.ItemGroups.Clear();

                var group1 = new DataGroup
                {
                    UniqueId = "Group-1",
                    Title = "Group Title: 1",
                    Subtitle = "Group Subtitle: 1",
                    ImagePath = "Assets/DarkGray.png",
                    Description = "Group Description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante"
                };
                group1.Items.Add(new DataItem
                {
                    UniqueId = "Group-1-Item-1",
                    Title = "Item Title: 1",
                    Subtitle = "Item Subtitle: 1",
                    Url = "http://preview.windows.com",
                    ImagePath = "Assets/MediumGray.png",
                    Description = "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    Content = ITEM_CONTENT,
                    Group = group1
                });
                group1.Items.Add(new DataItem
                {
                    UniqueId = "Group-1-Item-2",
                    Title = "Item Title: 2",
                    Subtitle = "Item Subtitle: 2",
                    Url = "http://preview.windows.com",
                    ImagePath = "Assets/MediumGray.png",
                    Description = "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    Content = ITEM_CONTENT,
                    Group = group1
                });
                group1.Items.Add(new DataItem
                {
                    UniqueId = "Group-1-Item-3",
                    Title = "Item Title: 3",
                    Subtitle = "Item Subtitle: 3",
                    Url = "http://preview.windows.com",
                    ImagePath = "Assets/MediumGray.png",
                    Description = "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    Content = ITEM_CONTENT,
                    Group = group1
                });
                group1.Items.Add(new DataItem
                {
                    UniqueId = "Group-1-Item-4",
                    Title = "Item Title: 4",
                    Subtitle = "Item Subtitle: 4",
                    Url = "http://preview.windows.com",
                    ImagePath = "Assets/MediumGray.png",
                    Description = "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    Content = ITEM_CONTENT,
                    Group = group1
                });
                group1.Items.Add(new DataItem
                {
                    UniqueId = "Group-1-Item-5",
                    Title = "Item Title: 5",
                    Subtitle = "Item Subtitle: 5",
                    Url = "http://preview.windows.com",
                    ImagePath = "Assets/MediumGray.png",
                    Description = "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    Content = ITEM_CONTENT,
                    Group = group1
                });
                AppData.Default.ItemGroups.Add(group1);
                group1.PrepareGroupForBinding();

                if (ShowTileNotifications)
                    RefreshTileNotifications(group1.Items);

                var group2 = new DataGroup
                {
                    UniqueId = "Group-2",
                    Title = "Group Title: 2",
                    Subtitle = "Group Subtitle: 2",
                    ImagePath = "Assets/DarkGray.png",
                    Description = "Group Description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante"
                };
                group2.Items.Add(new DataItem
                {
                    UniqueId = "Group-2-Item-1",
                    Title = "Item Title: 1",
                    Subtitle = "Item Subtitle: 1",
                    Url = "http://preview.windows.com",
                    ImagePath = "Assets/MediumGray.png",
                    Description = "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    Content = ITEM_CONTENT,
                    Group = group2
                });
                group2.Items.Add(new DataItem
                {
                    UniqueId = "Group-2-Item-2",
                    Title = "Item Title: 2",
                    Subtitle = "Item Subtitle: 2",
                    Url = "http://preview.windows.com",
                    ImagePath = "Assets/MediumGray.png",
                    Description = "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    Content = ITEM_CONTENT,
                    Group = group2
                });
                group2.Items.Add(new DataItem
                {
                    UniqueId = "Group-2-Item-3",
                    Title = "Item Title: 3",
                    Subtitle = "Item Subtitle: 3",
                    Url = "http://preview.windows.com",
                    ImagePath = "Assets/MediumGray.png",
                    Description = "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    Content = ITEM_CONTENT,
                    Group = group2
                });
                AppData.Default.ItemGroups.Add(group2);
                group2.PrepareGroupForBinding();

                if (ShowTileNotifications)
                    RefreshTileNotifications(group2.Items);

                var group3 = new DataGroup
                {
                    UniqueId = "Group-3",
                    Title = "Group Title: 3",
                    Subtitle = "Group Subtitle: 3",
                    ImagePath = "Assets/DarkGray.png",
                    Description = "Group Description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante"
                };
                group3.Items.Add(new DataItem
                {
                    UniqueId = "Group-3-Item-1",
                    Title = "Item Title: 1",
                    Subtitle = "Item Subtitle: 1",
                    Url = "http://preview.windows.com",
                    ImagePath = "Assets/MediumGray.png",
                    Description = "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    Content = ITEM_CONTENT,
                    Group = group3
                });
                group3.Items.Add(new DataItem
                {
                    UniqueId = "Group-3-Item-2",
                    Title = "Item Title: 2",
                    Subtitle = "Item Subtitle: 2",
                    Url = "http://preview.windows.com",
                    ImagePath = "Assets/MediumGray.png",
                    Description = "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    Content = ITEM_CONTENT,
                    Group = group3
                });
                group3.Items.Add(new DataItem
                {
                    UniqueId = "Group-3-Item-3",
                    Title = "Item Title: 3",
                    Subtitle = "Item Subtitle: 3",
                    Url = "http://preview.windows.com",
                    ImagePath = "Assets/MediumGray.png",
                    Description = "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    Content = ITEM_CONTENT,
                    Group = group3
                });
                group3.Items.Add(new DataItem
                {
                    UniqueId = "Group-3-Item-4",
                    Title = "Item Title: 4",
                    Subtitle = "Item Subtitle: 4",
                    Url = "http://preview.windows.com",
                    ImagePath = "Assets/MediumGray.png",
                    Description = "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    Content = ITEM_CONTENT,
                    Group = group3
                });
                group3.Items.Add(new DataItem
                {
                    UniqueId = "Group-3-Item-5",
                    Title = "Item Title: 5",
                    Subtitle = "Item Subtitle: 5",
                    Url = "http://preview.windows.com",
                    ImagePath = "Assets/MediumGray.png",
                    Description = "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    Content = ITEM_CONTENT,
                    Group = group3
                });
                group3.Items.Add(new DataItem
                {
                    UniqueId = "Group-3-Item-6",
                    Title = "Item Title: 6",
                    Subtitle = "Item Subtitle: 6",
                    Url = "http://preview.windows.com",
                    ImagePath = "Assets/MediumGray.png",
                    Description = "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    Content = ITEM_CONTENT,
                    Group = group3
                });
                group3.Items.Add(new DataItem
                {
                    UniqueId = "Group-3-Item-7",
                    Title = "Item Title: 7",
                    Subtitle = "Item Subtitle: 7",
                    Url = "http://preview.windows.com",
                    ImagePath = "Assets/MediumGray.png",
                    Description = "Item Description: Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.",
                    Content = ITEM_CONTENT,
                    Group = group3
                });
                AppData.Default.ItemGroups.Add(group3);
                group3.PrepareGroupForBinding();

                if (ShowTileNotifications)
                    RefreshTileNotifications(group3.Items);

                this.LastUpdateTime = DateTime.Now;
                this.ErrorMessage = string.Empty;
                this.State = FeedState.Loaded;

                await base.GetFeedAsync();
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
                this.State = FeedState.Error;
            }
        }
    }
}
