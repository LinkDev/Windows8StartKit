using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkDev.Windows8.Data;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LinkDev.Windows8.UI
{
    public class VariableSizedGridView:GridView
    {
        protected override void PrepareContainerForItemOverride(Windows.UI.Xaml.DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            
            DataItem dataItem = item as DataItem;

            VariableSizedWrapGrid.SetRowSpan(element as UIElement, dataItem.GridWidth);
            VariableSizedWrapGrid.SetColumnSpan(element as UIElement, dataItem.GridHeight);
        }
    }
}
