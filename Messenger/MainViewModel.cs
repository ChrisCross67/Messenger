using Messenger.Properties;
using Messenger.Protocol;
using Messenger.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Messenger
{
    public class MainViewModel : NotifyPropertyChangedBase
    {
        /// <summary>
        /// Gets the instance of the ViewModelLocator  class.
        /// </summary>
        /// <value>
        /// The instance of the ViewModelLocator class.
        /// </value>
        public static MainViewModel Instance { get { return Nested.MainInstance; } }

        public ObservableCollection<Attachment> Attachments
        {
            get
            {
                return attachments;
            }

            set
            {
                attachments = value; OnPropertyChanged();
            }
        }

        public string Message
        {
            get
            {
                return message;
            }

            set
            {
                message = value; OnPropertyChanged();
            }
        }

        private class Nested
        {
            internal static readonly MainViewModel MainInstance = new MainViewModel();

            // Explicit static constructor to tell C# compiler
            // not to mark type as before field initialized
            static Nested()
            {
            }
        }

        private ObservableCollection<Attachment> attachments = new ObservableCollection<Attachment>();
        private Member selectedMember;
        private string message;
        public Member SelectedMember
        {
            get
            {
                return selectedMember;
            }

            set
            {
                selectedMember = value; OnPropertyChanged();
            }
        }

        #region Attach
        private void AttachFile(string file)
        {
            FileInfo info = new FileInfo(file);
            Attachment attachment = new Attachment();
            attachment.Name = info.Name;
            attachment.Size = info.Length;
            attachment.Time = info.LastWriteTime;
            attachment.IsFile = true;
            attachment.FullPath = info.FullName;
            if (!attachments.Contains(attachment))
            {
                attachments.Add(attachment);
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
            if (!attachments.Contains(attachment))
            {
                attachments.Add(attachment);
            }
        }

        #endregion

        #region Process

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
            if (SelectedMember != null)
            {
                List<Attachment> attachments_clone = attachments.Select(attachment => attachment.Clone()).ToList();
                Messager.SendMessage(SelectedMember, Message, attachments_clone);

                Message = "";
                attachments.Clear();
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
            FolderDialog dialog = new FolderDialog();
            dialog.Folder = Settings.Default.LastSendDirectory;
            if (dialog.ShowDialog(Application.Current.MainWindow) == true)
            {
                Settings.Default.LastSendDirectory = dialog.Folder;
                AttachDirectory(dialog.Folder);
                //AutoAdjustHeight();
            }
        }
        #endregion
    }
}
