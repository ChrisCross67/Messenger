using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Messenger.Utils;
using System.Collections.ObjectModel;

using Messenger.Protocol;
using Messenger.Properties;

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


        public Member Sender
        {
            get { return (Member)GetValue(SenderProperty); }
            set { SetValue(SenderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Sender.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SenderProperty =
            DependencyProperty.Register("Sender", typeof(Member), typeof(MessageWindow), new PropertyMetadata(null));



        public ObservableCollection<Message> SentMessages
        {
            get { return (ObservableCollection<Message>)GetValue(SentMessagesProperty); }
            set { SetValue(SentMessagesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SentMessages.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SentMessagesProperty =
            DependencyProperty.Register("SentMessages", typeof(ObservableCollection<Message>), typeof(MessageWindow), new PropertyMetadata(new ObservableCollection<Message>()));


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        private void CloseWindow()
        {
            foreach (var message in SentMessages.Where(m=>m.HasAttachment))
            {
                if(!fileReceived)
                    Messager.DropFiles(message);
            }

            Close();
        }

        private void messageWindow_LocationChanged(object sender, EventArgs e)
        {
            if (popupReceive.IsOpen)
            {
                popupReceive.Placement = System.Windows.Controls.Primitives.PlacementMode.AbsolutePoint;
                popupReceive.PlacementRectangle = new Rect(
                    Left + (Width - popupReceive.Width) / 2,
                    Top + (Height - popupReceive.Height) / 2,
                    popupReceive.Width, popupReceive.Height);
            }
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
            Dispatcher.Invoke(new Action(delegate()
            {
                popupReceive.IsOpen = true;
                labelReciveFile.Text = "Receiving " + name;
            }));
        }

        void endProgress()
        {
            Dispatcher.Invoke(new Action(delegate()
            {
                popupReceive.IsOpen = false;
            }));
        }

        void updateProgressBar(double progress, double speed)
        {
            Dispatcher.Invoke(new Action(delegate()
            {
                recivedProgress.Value = progress * 100;
                textBlockTransferSpeed.Text = string.Format("{0}, {1:P}",
                    Network.Network.PresentTransferSpeed(speed), progress);
            }));
        }

        private void btnReplay_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
            MainWindow window = (MainWindow)Application.Current.MainWindow;
            window.WindowState = WindowState.Normal;
            window.Activate();
            window.Select(Sender);
        }
    }
}
