using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using Messenger.Utils;
using Messenger.Utils.WPF;
using Messenger.Properties;

using Messenger.Protocol;
using System.ComponentModel;
using MahApps.Metro;
using Messenger.Themes;

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

            MainViewModel.Instance.Attachments = new ObservableCollection<Attachment>();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSettings();
            listBoxAttachmentList.ItemsSource = MainViewModel.Instance.Attachments;
            listViewMembers.ItemsSource = Messager.Members;
            //listViewFiles.ItemsSource = Messager.Files;
            comboBoxNetwork.ItemsSource = Network.Network.GetIPv4NetworkInterfaces();
            if (comboBoxNetwork.Items.Count > 0)
            {
                comboBoxNetwork.SelectedIndex = 0;
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
            Messager.Self.Icon = App.LoadIcon(Settings.Default.IconIndex);
            try
            {
                Messager.TextEncoding = Encoding.GetEncoding(Settings.Default.TextEncoding);
            }
            catch { }
        }

        private void comboBoxNetwork_DropDownOpened(object sender, EventArgs e)
        {
            comboBoxNetwork.ItemsSource = Network.Network.GetIPv4NetworkInterfaces();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Messager.BroadcastExit();
            Messager.StopListening();
            Settings.Default.Save();
        }

        private void comboBoxNetwork_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxNetwork.SelectedItem != null)
            {
                Messager.StartListening((System.Net.IPAddress)comboBoxNetwork.SelectedItem,
                    Dispatcher);
                Messager.RefreshMembers();
            }
        }

        private void buttonRefresh_Click(object sender, RoutedEventArgs e)
        {
            Messager.RefreshMembers();
        }

        private void buttonAttach_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Settings.Default.LastSendDirectory;
            dialog.Multiselect = true;
            if (dialog.ShowDialog() == true)
            {
                Settings.Default.LastSendDirectory = System.IO.Path.GetDirectoryName(dialog.FileName);
                foreach (string file in dialog.FileNames)
                {
                    AttachFile(file);
                }
                AutoAdjustHeight();
            }
        }

        private void MenuItemAttachFolder_Click(object sender, RoutedEventArgs e)
        {
            FolderDialog dialog = new FolderDialog();
            dialog.Folder = Settings.Default.LastSendDirectory;
            if (dialog.ShowDialog(this) == true)
            {
                Settings.Default.LastSendDirectory = dialog.Folder;
                AttachDirectory(dialog.Folder);
                AutoAdjustHeight();
            }

        }

        private void MenuItemClear_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.Attachments.Clear();
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

        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)listBoxAttachmentList.ContainerFromElement((DependencyObject)sender);
            if (item != null)
            {
                MainViewModel.Instance.Attachments.Remove((Attachment)item.DataContext);
                AutoAdjustHeight();
            }
            else
            {
                //item = (ListBoxItem)listViewFiles.ContainerFromElement((DependencyObject)sender);
                //Messager.RemoveFromFileMap((Attachment)item.DataContext);
            }
        }

        private void AutoAdjustHeight()
        {
            grid2.RowDefinitions[0].Height = new GridLength(
                23 * listBoxAttachmentList.Items.Count + 23);
        }

        private void listBoxAttachmentList_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double changed_height = e.NewSize.Height - e.PreviousSize.Height;
            Height += changed_height;
        }

        private void listBoxAttachmentList_Drop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null)
            {
                foreach (string path in files)
                {
                    if (File.Exists(path))
                    {
                        AttachFile(path);
                    }
                    else if (Directory.Exists(path))
                    {
                        AttachDirectory(path);
                    }
                }
                AutoAdjustHeight();
            }
        }

        private void AttachFile(string file)
        {
            FileInfo info = new FileInfo(file);
            Attachment attachment = new Attachment();
            attachment.Name = info.Name;
            attachment.Size = info.Length;
            attachment.Time = info.LastWriteTime;
            attachment.IsFile = true;
            attachment.FullPath = info.FullName;
            if (!MainViewModel.Instance.Attachments.Contains(attachment))
            {
                MainViewModel.Instance.Attachments.Add(attachment);
            }
        }

        private void AttachDirectory(string folderPath)
        {
            DirectoryInfo info = new DirectoryInfo(folderPath);
            Attachment attachment = new Attachment();
            attachment.Name = info.Name;
            attachment.Time = info.LastWriteTime;
            attachment.IsDirectory = true;
            attachment.FullPath = info.FullName;
            if (!MainViewModel.Instance.Attachments.Contains(attachment))
            {
                MainViewModel.Instance.Attachments.Add(attachment);
            }
        }

        public void Select(Member member)
        {
            //if (tabs.SelectedIndex != 0)
            //{
            //    tabs.SelectedIndex = 0;
            //    Dispatcher.Invoke(
            //        System.Windows.Threading.DispatcherPriority.ContextIdle,
            //        new Action(delegate { }));
            //}
            listViewMembers.ScrollIntoView(member);
            listViewMembers.SelectedItem = member;
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
    }
}
