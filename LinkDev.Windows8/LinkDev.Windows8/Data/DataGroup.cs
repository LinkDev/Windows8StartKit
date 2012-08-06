using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Media;

namespace LinkDev.Windows8.Data
{
    public class DataGroup:DataCommon
    {
        private ObservableCollection<DataItem> _items = new ObservableCollection<DataItem>();
        public ObservableCollection<DataItem> Items
        {
            get { return this._items; }
        }

        private ObservableCollection<DataItem> _itemsSummary = new ObservableCollection<DataItem>();
        public ObservableCollection<DataItem> ItemsSummary
        {
            get { return this._itemsSummary; }
        }

        private int _summaryItemsCount = 4;
        public int SummaryItemsCount
        {
            get { return _summaryItemsCount; }
            set { this.SetProperty(ref this._summaryItemsCount, value); }
        }

        private int _firstItemGridWidth = 1;
        public int FirstItemGridWidth
        {
            get { return _firstItemGridWidth; }
            set { this.SetProperty(ref this._firstItemGridWidth, value); }
        }

        private int _firstItemGridHeight = 1;
        public int FirstItemGridHeight
        {
            get { return _firstItemGridHeight;}
            set { this.SetProperty(ref this._firstItemGridHeight, value); }
        }

        public virtual void PrepareGroupForBinding()
        {
           // ItemsSummary.Clear();
            for (int i = 0; i < SummaryItemsCount && i<Items.Count; i++)
            {
                DataItem item = Items[i];
                if (i == 0)
                {
                    item.GridWidth = FirstItemGridWidth;
                    item.GridHeight = FirstItemGridHeight;
                }
                else
                {
                    item.GridWidth = 1;
                    item.GridHeight = 1;
                }

                ItemsSummary.Add(item);
            }
        }

        public void UnloadItemsImages()
        {
            foreach (var item in Items)
            {
                if (ItemsSummary.Contains(item) == false)
                {
                    item.UnloadImage();
                }
            }

            GC.Collect();
        }

        public void MergeItemsFromDataGroup(DataGroup sourceGroup)
        {
            for (int s = 0; s < sourceGroup.Items.Count; s++)
			{
			    var item = sourceGroup.Items[s];
		        bool isFound = false;
                for (int i = 0; i < Items.Count; i++)
                {
                    var targetItem = Items[i];

                    if (item.UniqueId == targetItem.UniqueId)
                    {
                        isFound = true;
                        Items.Remove(targetItem);
                        Items.Insert(s, item);
                        break;
                    }
                }

                if (isFound == false)
                {
                    Items.Add(item);
                }
            }
        }

        public static void MergeDataGroupsCollections(ObservableCollection<DataGroup> sourceGroups, ObservableCollection<DataGroup> targetGroups)
        {
            for (int s = 0; s < sourceGroups.Count; s++)
			{
			    var group = sourceGroups[s];
			    bool isFound=false;
                for (int i = 0; i < targetGroups.Count; i++)
                {
                    var oldGroup = targetGroups[i];
                    if (group.UniqueId == oldGroup.UniqueId)
                    {
                        isFound = true;
                        oldGroup.MergeItemsFromDataGroup(group);
                        break;
                    }
                }
                
                if (isFound == false)
                    targetGroups.Insert(s, group);
            }
        }
    }
}
