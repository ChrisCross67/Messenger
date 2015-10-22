using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Messenger.Utils;
using Messenger.Protocol;
using Messenger.Properties;
using Microsoft.Win32;
using System.IO;
using System.Collections.Specialized;
using System.Windows.Media;
using System.Diagnostics;

namespace Messenger
{
    /// <summary>
    /// MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow
    {
        public MessageWindow()
        {
            InitializeComponent();
        }

        private bool fileReceived = false;


        public MessagerModel MessageContext
        {
            get { return (MessagerModel)GetValue(MessageContextProperty); }
            set { SetValue(MessageContextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Sender.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageContextProperty =
            DependencyProperty.Register("MessageContext", typeof(MessagerModel), typeof(MessageWindow), new PropertyMetadata(null,OnContextChanged));

        private static void OnContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var messageWindow = d as MessageWindow;
            if (messageWindow == null)
                return;

            if(e.NewValue != null)
            {
                var context = e.NewValue as MessagerModel;
                if (context == null)
                    return;
                context.Messages.CollectionChanged += messageWindow.OnMessageAppened;
            }
            if (e.OldValue != null)
            {
                var context = e.OldValue as MessagerModel;
                if (context == null)
                    return;
                context.Messages.CollectionChanged -= messageWindow.OnMessageAppened;
            }
        }

        private void OnMessageAppened(object sender, NotifyCollectionChangedEventArgs e)
        {

        }
        private void CloseWindow()
        {
            foreach (var message in MessageContext.Messages.Where(m=>m.HasAttachment))
            {
                if(!fileReceived)
                    Messager.DropFiles(message);
            }
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
                    MessagerViewModel.Instance.AttachFile(MessageContext,file);
                }
                AutoAdjustHeight();
            }
        }
        private void listBoxAttachmentList_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double changed_height = e.NewSize.Height - e.PreviousSize.Height;
            Height += changed_height;
        }

        private void MenuItemAttachFolder_Click(object sender, RoutedEventArgs e)
        {
            FolderDialog dialog = new FolderDialog();
            dialog.Folder = Settings.Default.LastSendDirectory;
            if (dialog.ShowDialog(this) == true)
            {
                Settings.Default.LastSendDirectory = dialog.Folder;
                MessagerViewModel.Instance.AttachDirectory(MessageContext,dialog.Folder);
                AutoAdjustHeight();
            }

        }

        private void MenuItemClear_Click(object sender, RoutedEventArgs e)
        {
            MessageContext.Attachments.Clear();
        }
        private void AutoAdjustHeight()
        {
            grid2.RowDefinitions[0].Height = new GridLength(
                23 * listBoxAttachmentList.Items.Count + 23);
        }
        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
                return;

            var attachments = ((button.Tag) as ListBox).ItemsSource as List<Checkable<Attachment>>;
            if (attachments == null)
                return;

            var message = button.DataContext as Message;
            if (message == null)
                return;
            var selected = (from Checkable<Attachment> file in attachments
                           where file.Checked
                           select file.Item)
                              .ToArray();

            if (selected.Length > 0)
            {
                FolderDialog dialog = new FolderDialog();
                dialog.Folder = Settings.Default.LastReceivedDirectory;
                if (dialog.ShowDialog(this) == true)
                {
                    Settings.Default.LastReceivedDirectory = dialog.Folder;
                    System.Threading.Thread thread = new System.Threading.Thread(delegate ()
                    {
                        Messager.ReceiveFiles(message, selected, dialog.Folder, startProgress, updateProgressBar, endProgress);
                        fileReceived = true;
                    });
                    thread.Start();
                }
            }
        }

        void startProgress(string name)
        {
            //Dispatcher.Invoke(new Action(delegate()
            //{
            //    popupReceive.IsOpen = true;
            //    labelReciveFile.Text = "Receiving " + name;
            //}));
        }

        void endProgress()
        {
        //    Dispatcher.Invoke(new Action(delegate()
        //    {
        //        popupReceive.IsOpen = false;
        //    }));
        }
        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)listBoxAttachmentList.ContainerFromElement((DependencyObject)sender);
            if (item != null)
            {
                MessageContext.Attachments.Remove((Attachment)item.DataContext);
                AutoAdjustHeight();
            }
            else
            {
                //item = (ListBoxItem)listViewFiles.ContainerFromElement((DependencyObject)sender);
                //Messager.RemoveFromFileMap((Attachment)item.DataContext);
            }
        }
        void updateProgressBar(double progress, double speed)
        {
            //Dispatcher.Invoke(new Action(delegate()
            //{
            //    recivedProgress.Value = progress * 100;
            //    textBlockTransferSpeed.Text = string.Format("{0}, {1:P}",
            //        Network.Network.PresentTransferSpeed(speed), progress);
            //}));
        }

        private void btnReplay_Click(object sender, RoutedEventArgs e)
        {
            //CloseWindow();
            //MainWindow window = (MainWindow)Application.Current.MainWindow;
            //window.WindowState = WindowState.Normal;
            //window.Activate();
            //window.Select(Sender);
        }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CloseWindow();
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
                        MessagerViewModel.Instance.AttachFile(MessageContext,path);
                    }
                    else if (Directory.Exists(path))
                    {
                        MessagerViewModel.Instance.AttachDirectory(MessageContext,path);
                    }
                }
                AutoAdjustHeight();
            }
        }
    }
}
