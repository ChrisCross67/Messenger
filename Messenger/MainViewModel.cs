using Messenger.Extensions;
using Messenger.Properties;
using Messenger.Protocol;
using Messenger.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Messenger
{
    public class MainViewModel : NotifyPropertyChangedBase
    {
        /// <summary>
        /// Gets the instance of the MainViewModel  class.
        /// </summary>
        /// <value>
        /// The instance of the MainViewModel class.
        /// </value>
        public static MainViewModel Instance { get { return Nested.MainInstance; } }


        private class Nested
        {
            internal static readonly MainViewModel MainInstance = new MainViewModel();

            // Explicit static constructor to tell C# compiler
            // not to mark type as before field initialized
            static Nested()
            {
            }
        }

        MemberList _members = new MemberList();
        List<IPAddress> _networks = new List<IPAddress>();

        public MemberList Members
        {
            get
            {
                return _members;
            }

            set
            {
                _members = value;
                OnPropertyChanged();
            }
        }

        public List<IPAddress> Networks
        {
            get
            {
                return _networks;
            }

            set
            {
                _networks = value;
                OnPropertyChanged();
            }
        }

        #region OptionCommand
        private ICommand _optionCommand;


        public ICommand OptionCommand
        {
            get
            {
                return _optionCommand ?? (_optionCommand = new RelayCommand(
                    OptionExecute,
                    OptionCanExecute
                    ));
            }
        }



        private static bool OptionCanExecute(object parameter)
        {
            return true;
        }

        private void OptionExecute(object parameter)
        {
            OptionWindow window = new OptionWindow();
            window.Owner = Application.Current.MainWindow;
            window.ShowDialog();
        }
        #endregion

        #region AboutCommand
        private ICommand _aboutCommand;


        public ICommand AboutCommand
        {
            get
            {
                return _aboutCommand ?? (_aboutCommand = new RelayCommand(
                    AboutExecute,
                    AboutCanExecute
                    ));
            }
        }



        private static bool AboutCanExecute(object parameter)
        {
            return true;
        }

        private void AboutExecute(object parameter)
        {
            AboutWindow window = new AboutWindow();
            window.Owner = Application.Current.MainWindow;
            window.ShowDialog();
        }
        #endregion

        #region RefreshMembers
        private ICommand _refreshMembers;


        public ICommand RefreshMembersCommand
        {
            get
            {
                return _refreshMembers ?? (_refreshMembers = new RelayCommand(
                    RefreshMembersExecute,
                    RefreshMembersCanExecute
                    ));
            }
        }



        private static bool RefreshMembersCanExecute(object parameter)
        {
            return true;
        }

        private void RefreshMembersExecute(object parameter)
        {
            Messager.RefreshMembers();
        }
        #endregion

        #region OpenUserMessengerWindowCommand
        private ICommand _openUserMessengerWindowCommandCommand;


        public ICommand OpenUserMessengerWindowCommand
        {
            get
            {
                return _openUserMessengerWindowCommandCommand ?? (_openUserMessengerWindowCommandCommand = new RelayCommand(
                    OpenUserMessengerExecute,
                    OpenUserMessengerCanExecute
                    ));
            }
        }



        private static bool OpenUserMessengerCanExecute(object parameter)
        {
            var listBox = parameter as ListBox;
            if (listBox != null)
            {
                var row = listBox.TryFindFromPoint<ListBoxItem>(Mouse.GetPosition(listBox));
                if (row == null)
                    return false;
                if (!listBox.TryFindFromPoint<ListBoxItem>(Mouse.GetPosition(listBox))
                                                                    .GetType()
                                                                    .Equals(typeof(ListBoxItem)))
                    return false;

                return true;
            }
            else
            {
                return true;
            }

        }

        private void OpenUserMessengerExecute(object parameter)
        {
            Member member;
            var listBox = parameter as ListBox;
            member = listBox != null ? listBox.SelectedItem as Member : parameter as Member;

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
        #endregion

    }
}
