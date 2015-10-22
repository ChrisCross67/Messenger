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


        public MemberList Members
        {
            get { return (MemberList)GetValue(MembersProperty); }
            set { SetValue(MembersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Members.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MembersProperty =
            DependencyProperty.Register("Members", typeof(MemberList), typeof(MainWindow), new PropertyMetadata(new MemberList()));



        public List<System.Net.IPAddress> Networks
        {
            get { return (List<System.Net.IPAddress>)GetValue(NetworksProperty); }
            set { SetValue(NetworksProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Networks.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NetworksProperty =
            DependencyProperty.Register("Networks", typeof(List<System.Net.IPAddress>), typeof(MainWindow), new PropertyMetadata(new List<System.Net.IPAddress>()));


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSettings();
            Messager.Members.CollectionChanged += OnMembersChanged;
            Networks = Network.Network.GetIPv4NetworkInterfaces();
        }

        private void OnMembersChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.NewItems != null && e.NewItems[0] != null)
            {
                foreach (Member newMember in e.NewItems)
                {
                    if (newMember.UserName.Equals(Messager.Self.UserName))
                        continue;
                    Members.AddMember(newMember);
                }
            }
            if (e.OldItems != null && e.OldItems[0] != null)
            {
                foreach (Member oldMember in e.NewItems)
                {
                    if (oldMember.UserName.Equals(Messager.Self.UserName))
                        continue;
                    Members.RemoveMember(oldMember.IPAddress);
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
                Messager.TextEncoding = Encoding.GetEncoding(Settings.Default.TextEncoding);
            }
            catch { }
        }

        private void OnComboBoxDropDownOpened(object sender, EventArgs e)
        {
            Networks = Network.Network.GetIPv4NetworkInterfaces();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Messager.BroadcastExit();
            Messager.StopListening();
            Settings.Default.Save();
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

        private void buttonRefresh_Click(object sender, RoutedEventArgs e)
        {
            Messager.RefreshMembers();
        }

        private void buttonOption_Click(object sender, RoutedEventArgs e)
        {
            OptionWindow option = new OptionWindow();
            if (option.ShowDialog() == true)
            {
                LoadSettings();
                Settings.Default.Save();
            }
        }


        private void buttonAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow window = new AboutWindow();
            window.Owner = this;
            window.ShowDialog();
        }

        private void OnThemeSettingsClicked(object sender, RoutedEventArgs e)
        {
            //ThemeSelectionViewModel.Initialize();
            flyoutsControl.IsOpen = true;
        }

        private void OnUserGridClicked(object sender, RoutedEventArgs e)
        {
            Member member = (sender as Button).DataContext as Member;
            if (member == null)
                return;

            Application.Current.Dispatcher.Invoke(new Action(delegate
            {
                var receivedMessage = Messager.GetWindowOpen<MessageWindow>(member.UserName);
                if (receivedMessage == null)
                {
                    var messager = new MessagerModel
                    {
                        Messages = new ObservableCollection<Message>(),
                        Member = member
                    };
                    MessagerViewModel.Instance.Messagers.Add(messager);
                    receivedMessage = new MessageWindow();
                    receivedMessage.DataContext = messager;
                    receivedMessage.Tag = member.UserName;
                    receivedMessage.MessageContext = messager;
                    receivedMessage.Show();
                    if (Settings.Default.ActivateComingMessage)
                    {
                        receivedMessage.Activate();
                    }
                }
                else
                    receivedMessage.Activate();
            }));
        }

        private void OnUserGridDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox == null)
                return;
            //Ensure a ListBoxItem is clicked
            var row = this.TryFindFromPoint<ListBoxItem>(e.GetPosition(this));
            if (row == null)
                return;
            if (!this.TryFindFromPoint<ListBoxItem>(e.GetPosition(this)).GetType().Equals(typeof(ListBoxItem)))
                return;
            //Gets the current member
            Member member = listBox.SelectedItem as Member;
            if (member == null)
                return;

            Application.Current.Dispatcher.Invoke(new Action(delegate
            {
                //Open the message window
                var receivedMessage = Messager.GetWindowOpen<MessageWindow>(member.UserName);
                if (receivedMessage == null)
                {
                    var messager = new MessagerModel
                    {
                        Messages = new ObservableCollection<Message>(),
                        Member = member
                    };
                    MessagerViewModel.Instance.Messagers.Add(messager);
                    receivedMessage = new MessageWindow();
                    receivedMessage.DataContext = messager;
                    receivedMessage.Name = member.UserName;
                    receivedMessage.MessageContext = messager;
                    receivedMessage.Show();
                    if (Settings.Default.ActivateComingMessage)
                    {
                        receivedMessage.Activate();
                    }
                }
                else
                    receivedMessage.Activate();
            }));
        }
    }
}
