using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Messenger.Utils.WPF;
using Messenger.Properties;

using Messenger.Protocol;
using System.ComponentModel;
using System.Windows.Input;
using Messenger.Extensions;
using System.Collections.Specialized;
using System.Collections.Generic;
using Messenger.Notification;
using System.Drawing;

namespace Messenger
{
    /// <summary>
    /// MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            Messager.Logger = new MessageLogger();

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSettings();
            Messager.Members.CollectionChanged += OnMembersChanged;
            MainViewModel.Instance.Networks = Network.Network.GetIPv4NetworkInterfaces();
        }

        private void OnMembersChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null && e.OldItems[0] != null)
            {
                foreach (Member oldMember in e.OldItems)
                {
                    if (oldMember.MachineName.Equals(Messager.Self.MachineName))
                        continue;
                    MainViewModel.Instance.Members.RemoveMember(oldMember.IPAddress);
                }
            }
            if (e.NewItems != null && e.NewItems[0] != null)
            {
                foreach (Member newMember in e.NewItems)
                {
                    if (newMember.MachineName.Equals(Messager.Self.MachineName))
                        continue;
                    MainViewModel.Instance.Members.AddMember(newMember);
                }
            }
        }

        private static void LoadSettings()
        {
            if (!string.IsNullOrEmpty(Settings.Default.UserName))
            {
                Messager.UserName = Settings.Default.UserName;
            }
            if (Settings.Default.PortNumber > 0)
            {
                Messager.PortNumber = Settings.Default.PortNumber;
            }
            //Messager.Self.Icon = App.LoadIcon(Settings.Default.IconIndex);
            try
            {
                if(!string.IsNullOrEmpty(Settings.Default.TextEncoding))
                    Messager.TextEncoding = Encoding.GetEncoding(Settings.Default.TextEncoding);
            }
            catch { }
        }

        private void OnComboBoxDropDownOpened(object sender, EventArgs e)
        {
            MainViewModel.Instance.Networks = Network.Network.GetIPv4NetworkInterfaces();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Messager.BroadcastExit();
            Messager.StopListening();
            Settings.Default.Save();
            tb.Visibility = Visibility.Collapsed;
            tb.Dispose();
        }

        private void OnComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox == null)
                return;
            if (comboBox.SelectedItem != null)
            {
                Messager.StartListening((System.Net.IPAddress)comboBox.SelectedItem,
                    Dispatcher);
                Messager.RefreshMembers();
            }
        }

        private void OnThemeSettingsClicked(object sender, RoutedEventArgs e)
        {
            flyoutsControl.IsOpen = true;
        }
    }
}
