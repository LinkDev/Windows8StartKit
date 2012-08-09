using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace LinkDev.Windows8.Helpers
{
    /// <summary>
    /// This helper has methods that makes it easy to bind from code
    /// </summary>
    public class BindingHelper
    {
        /// <summary>
        /// Sets a binding from code
        /// </summary>
        /// <param name="element"></param>
        /// <param name="property"></param>
        /// <param name="source"></param>
        /// <param name="path"></param>
        /// <param name="converter"></param>
        public static void SetBinding(FrameworkElement element, DependencyProperty property, object source, string path, IValueConverter converter = null)
        {
            Binding binding = new Binding();
            binding.Source = source;
            binding.Path = new PropertyPath(path);
            binding.Converter = converter;
            element.SetBinding(property, binding);
        }
    }
}
