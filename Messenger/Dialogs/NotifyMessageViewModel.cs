﻿using System;
using System.Windows.Input;

namespace Messenger.Dialogs
{
    public class NotifyMessageViewModel
    {
        private readonly NotifyMessage _content;
        private readonly AnimatedLocation _location;
        private readonly Action _closedAction;

        public NotifyMessageViewModel(NotifyMessage content, AnimatedLocation location, Action closedAction)
        {
            this._content = content;
            this._location = location;
            this._closedAction = closedAction;
        }

        public NotifyMessage Message
        {
            get { return _content; }
        }
        private ICommand _closeCommand;
        public ICommand CloseCommand
        {
            get { return (_closeCommand ?? (_closeCommand = new DelegateCommand((_) => _closedAction()))); }
        }

        public AnimatedLocation Location
        {
            get { return _location; }
        }
    }
}
