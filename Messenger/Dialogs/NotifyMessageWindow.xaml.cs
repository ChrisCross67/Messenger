using System;
using System.Windows;

namespace Messenger.Dialogs
{
    /// <summary>
    /// Interaction logic for NotifyMessageWindow.xaml
    /// </summary>
    public partial class NotifyMessageWindow
    {
        public NotifyMessageWindow()
        {
            InitializeComponent();
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        // Handles the zoom-out storyboard's completed event.
        private void StoryboardCompleted(object sender, EventArgs e)
        {
            this.Close();
            var message = DataContext as NotifyMessageViewModel;
            if (message == null)
                return;
            message.CloseCommand.Execute(null);
        }
    }
}
