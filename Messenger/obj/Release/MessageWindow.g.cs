﻿#pragma checksum "..\..\MessageWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5F4496EE77394BD31B0DDC34E46017D4"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using Messenger;
using Messenger.Converters;
using Messenger.Utils;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Messenger {
    
    
    /// <summary>
    /// MessageWindow
    /// </summary>
    public partial class MessageWindow : MahApps.Metro.Controls.MetroWindow, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 11 "..\..\MessageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Messenger.MessageWindow messageWindow;
        
        #line default
        #line hidden
        
        
        #line 283 "..\..\MessageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl ItemsControlMessage;
        
        #line default
        #line hidden
        
        
        #line 297 "..\..\MessageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grid2;
        
        #line default
        #line hidden
        
        
        #line 326 "..\..\MessageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel attachmentPanel;
        
        #line default
        #line hidden
        
        
        #line 331 "..\..\MessageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listBoxAttachmentList;
        
        #line default
        #line hidden
        
        
        #line 415 "..\..\MessageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MahApps.Metro.Controls.DropDownButton DownButton1;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Messenger;component/messagewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MessageWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.messageWindow = ((Messenger.MessageWindow)(target));
            
            #line 16 "..\..\MessageWindow.xaml"
            this.messageWindow.Closing += new System.ComponentModel.CancelEventHandler(this.OnClosing);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 29 "..\..\MessageWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.buttonAttach_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 30 "..\..\MessageWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItemAttachFolder_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 31 "..\..\MessageWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItemClear_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ItemsControlMessage = ((System.Windows.Controls.ItemsControl)(target));
            return;
            case 7:
            this.grid2 = ((System.Windows.Controls.Grid)(target));
            return;
            case 8:
            this.attachmentPanel = ((System.Windows.Controls.DockPanel)(target));
            return;
            case 9:
            this.listBoxAttachmentList = ((System.Windows.Controls.ListBox)(target));
            
            #line 334 "..\..\MessageWindow.xaml"
            this.listBoxAttachmentList.Drop += new System.Windows.DragEventHandler(this.listBoxAttachmentList_Drop);
            
            #line default
            #line hidden
            
            #line 337 "..\..\MessageWindow.xaml"
            this.listBoxAttachmentList.SizeChanged += new System.Windows.SizeChangedEventHandler(this.listBoxAttachmentList_SizeChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 355 "..\..\MessageWindow.xaml"
            ((System.Windows.Controls.TextBox)(target)).ContextMenuOpening += new System.Windows.Controls.ContextMenuEventHandler(this.OnContextMenuOpenning);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 364 "..\..\MessageWindow.xaml"
            ((System.Windows.Controls.ContextMenu)(target)).ContextMenuOpening += new System.Windows.Controls.ContextMenuEventHandler(this.OnContextMenuOpenning);
            
            #line default
            #line hidden
            return;
            case 12:
            this.DownButton1 = ((MahApps.Metro.Controls.DropDownButton)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 5:
            
            #line 163 "..\..\MessageWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.buttonSave_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

