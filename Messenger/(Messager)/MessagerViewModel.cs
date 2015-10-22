using Messenger.Properties;
using Messenger.Protocol;
using Messenger.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Messenger
{
    public class MessagerViewModel : NotifyPropertyChangedBase
    {
        /// <summary>
        /// Gets the instance of the MessagerViewModel  class.
        /// </summary>
        /// <value>
        /// The instance of the MessagerViewModel class.
        /// </value>
        public static MessagerViewModel Instance { get { return Nested.MainInstance; } }


        private class Nested
        {
            internal static readonly MessagerViewModel MainInstance = new MessagerViewModel();

            // Explicit static constructor to tell C# compiler
            // not to mark type as before field initialized
            static Nested()
            {
            }
        }

        public ObservableCollection<MessagerModel> Messagers = new ObservableCollection<MessagerModel>();

        #region Attach
        public void AttachFile(MessagerModel messager, string file)
        {
            if (messager == null)
                return;
            FileInfo info = new FileInfo(file);
            Attachment attachment = new Attachment();
            attachment.Name = info.Name;
            attachment.Size = info.Length;
            attachment.Time = info.LastWriteTime;
            attachment.IsFile = true;
            attachment.FullPath = info.FullName;
            if (!messager.Attachments.Contains(attachment))
                messager.Attachments.Add(attachment);
        }

        public void AttachDirectory(MessagerModel messager,string folderPath)
        {
            if (messager == null)
                return;
            DirectoryInfo info = new DirectoryInfo(folderPath);
            Attachment attachment = new Attachment();
            attachment.Name = info.Name;
            attachment.Time = info.LastWriteTime;
            attachment.IsDirectory = true;
            attachment.FullPath = info.FullName;
            if (!messager.Attachments.Contains(attachment))
                messager.Attachments.Add(attachment);
        }

        #endregion

        #region SendCommand

        private ICommand _sendCommand;


        public ICommand SendCommand
        {
            get
            {
                return _sendCommand ?? (_sendCommand = new RelayCommand(
                    SendExecute,
                    SendCanExecute
                    ));
            }
        }



        private static bool SendCanExecute(object parameter)
        {
            return true;
        }

        private async void SendExecute(object parameter)
        {
            var messager = parameter as MessagerModel;
            if (messager == null)
                return;
            if (messager.Member != null)
            {
                List<Attachment> attachments_clone = messager.Attachments.Select(attachment => attachment.Clone()).ToList();

                messager.Messages.Add(new Message
                {
                    Content = messager.MessageToSend,
                    Attachments = new List<Attachment>(attachments_clone),
                    HasAttachment = messager.Attachments.Any(),
                    SenderName = Resources.Me,
                    Date = DateTime.Now
                });

                Messager.SendMessage(messager.Member, messager.MessageToSend, attachments_clone);

                messager.MessageToSend = "";
                messager.Attachments.Clear();
            }
        }

        #endregion Process


        #region AttachFolder
        private ICommand _attachFolderCommand;


        public ICommand AttachFolderCommand
        {
            get
            {
                return _attachFolderCommand ?? (_attachFolderCommand = new RelayCommand(
                    AttachFolderExecute,
                    AttachFolderCanExecute
                    ));
            }
        }



        private static bool AttachFolderCanExecute(object parameter)
        {
            return true;
        }

        private async void AttachFolderExecute(object parameter)
        {
            var messager = parameter as MessagerModel;
            if (messager == null)
                return;
            FolderDialog dialog = new FolderDialog();
            dialog.Folder = Settings.Default.LastSendDirectory;
            if (dialog.ShowDialog(Application.Current.MainWindow) == true)
            {
                Settings.Default.LastSendDirectory = dialog.Folder;
                AttachDirectory(messager,dialog.Folder);
                //AutoAdjustHeight();
            }
        }
        #endregion
    }
}
