﻿

#pragma checksum "C:\Users\Bishoy\Documents\GitHub\Windows8StartKit\NewApp\NewApp\DataModel\About.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "269ED1C4311A3DC41778985D2B97930E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace Masrawy
{
    partial class About : Windows.UI.Xaml.Controls.UserControl
    {
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private Windows.UI.Xaml.Controls.Button backButton; 
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private bool _contentLoaded;

        [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;

            _contentLoaded = true;
            Application.LoadComponent(this, new System.Uri("ms-appx:///DataModel/About.xaml"), Windows.UI.Xaml.Controls.Primitives.ComponentResourceLocation.Application);
 
            backButton = (Windows.UI.Xaml.Controls.Button)this.FindName("backButton");
        }
    }
}



