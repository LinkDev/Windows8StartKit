using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace LinkDev.Windows8.UI
{
    public class AutoHideGridView:GridView
    {
        public AutoHideGridView()
        {
            
        }

        protected override void OnItemsChanged(object e)
        {
            if (Items.Count > 0)
                this.Visibility = Windows.UI.Xaml.Visibility.Visible;
            else
                this.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            base.OnItemsChanged(e);
        }
    }
}
