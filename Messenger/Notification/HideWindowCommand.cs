using System.Windows.Input;

namespace Messenger.Notification
{
    /// <summary>
    /// Hides the main window.
    /// </summary>
    public class HideWindowCommand : CommandBase<HideWindowCommand>
    {
        public override void Execute(object parameter)
        {
            GetTaskbarWindow(parameter).Hide();
            CommandManager.InvalidateRequerySuggested();
        }


        public override bool CanExecute(object parameter)
        {
            var win = GetTaskbarWindow(parameter);
            return win != null && win.IsVisible;
        }
    }
}
