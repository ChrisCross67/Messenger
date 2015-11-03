using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Messenger.Dialogs
{
    public class NotifyMessageManager
    {
        private readonly object _syncRoot = new object();

        /// <summary>
        /// Gets or sets the maximum number of popup on the screen height.
        /// </summary>
        /// <value>
        /// The maximum number of popup on the screen height.
        /// </value>
        protected int MaxPopup { get; set; }

        /// <summary>
        /// Gets or sets the display locations.
        /// </summary>
        /// <value>
        /// The display locations.
        /// </value>
        protected List<AnimatedLocation> DisplayLocations { get; set; }

        /// <summary>
        /// Gets or sets the queued messages.
        /// </summary>
        /// <value>
        /// The queued messages.
        /// </value>
        protected ConcurrentQueue<NotifyMessage> QueuedMessages { get; set; }

        /// <summary>
        /// Gets or sets the display messages.
        /// </summary>
        /// <value>
        /// The display messages.
        /// </value>
        protected NotifyMessageViewModel[] DisplayMessages { get; set; }

        /// <summary>
        /// The _CTS
        /// </summary>
        private CancellationTokenSource _cts;

        private bool _isStarted;

        private delegate void MethodInvoker();

        public NotifyMessageManager(double popupWidth = 250, double popupHeight = 150)
        {
            MaxPopup = Convert.ToInt32(Screen.Height / popupHeight) - 1;
            DisplayLocations = new List<AnimatedLocation>(MaxPopup);
            DisplayMessages = new NotifyMessageViewModel[MaxPopup];
            QueuedMessages = new ConcurrentQueue<NotifyMessage>();

            double left = Screen.Width - popupWidth;
            double top = Screen.Height;

            for (int index = 0; index < MaxPopup; index++)
            {
                if (index == 0)
                {
                    DisplayLocations.Add(new AnimatedLocation(left, left, Screen.Height, top - popupHeight));
                }
                else
                {
                    var previousLocation = DisplayLocations[index - 1];
                    DisplayLocations.Add(new AnimatedLocation(
                        left, left, previousLocation.ToTop, previousLocation.ToTop - popupHeight));
                }
            }
            _isStarted = false;
        }

        public bool IsStarted
        {
            get { return _isStarted; }
        }

        private void Start()
        {
            lock (_syncRoot)
            {
                if (_isStarted)
                    return;
                _cts = new CancellationTokenSource();
                Task.WaitAll(new[] { StartService(_cts.Token) }, 0);
                _isStarted = true;
            }
        }

        private Task StartService(CancellationToken cancellationToken)
        {
            var dispatcher = Application.Current.Dispatcher;

            return Task.Run(() =>
            {
                do
                {
                    // Gets the next display location in the screen
                    int nextLocation = FindNextLocation();

                    if (nextLocation <= -1)
                        continue;
                    NotifyMessage msg;
                    //  Retrieve the message from the queue
                    if (!QueuedMessages.TryDequeue(out msg))
                        continue;
                    //  construct a View Model and binds it to the Popup Window
                    var viewModel = new NotifyMessageViewModel(msg,
                        DisplayLocations[nextLocation],
                        () => DisplayMessages[nextLocation] = null);    // Free the display location when the popup window is closed
                    DisplayMessages[nextLocation] = viewModel;

                    //  Use Application.Current.MainWindow.Dispatcher to switch back to the UI Thread to create popup window
                    dispatcher.BeginInvoke(new MethodInvoker(() =>
                    {
                        try
                        {
                            var window = new NotifyMessageWindow
                            {
                                //Owner = Application.Current.MainWindow,
                                Topmost = true,
                                DataContext = viewModel,
                                ShowInTaskbar = false
                            };
                            window.Show();
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.Message);
                        }
                    }), DispatcherPriority.Background);
                } while (QueuedMessages.Count > 0 && !cancellationToken.IsCancellationRequested);

                Stop();
            });
        }

        private void Stop()
        {
            lock (_syncRoot)
            {
                if (_isStarted)
                {
                    StopService();
                    _isStarted = false;
                }
            }
        }

        private void StopService()
        {
            _cts.Cancel();
            _cts.Dispose();
        }

        private int FindNextLocation()
        {
            for (int index = 0; index < DisplayMessages.Length; index++)
            {
                if (DisplayMessages[index] == null)
                    return index;
            }
            return -1;
        }

        public void SendMessage(NotifyMessage msg)
        {
            QueuedMessages.Enqueue(msg);
            Start();
        }
    }
}