﻿//-------------------------------------------------------------------------------------
// Author:   Murray Foxcroft - April 2009
// Comments: code behind for the main WPF popup window
//-------------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Messenger.Dialogs
{
    public partial class NotifyIcon : Window
    {
        public ExtendedNotifyIcon extendedNotifyIcon; // global class scope for the icon as it needs to exist foer the lifetime of the window
        private Storyboard gridFadeInStoryBoard;
        private Storyboard gridFadeOutStoryBoard;

        /// <summary>
        /// Sets up the popup window and instantiates the notify icon
        /// </summary>
        public NotifyIcon()
        {
            // Create a manager (ExtendedNotifyIcon) for handling interaction with the notification icon and wire up events.
            extendedNotifyIcon = new ExtendedNotifyIcon();
            extendedNotifyIcon.MouseLeave += extendedNotifyIcon_OnHideWindow;
            extendedNotifyIcon.MouseMove += extendedNotifyIcon_OnShowWindow;
            extendedNotifyIcon.targetNotifyIcon.Text = Title;


            InitializeComponent();

            // Set the startup position and the startup state to "not visible"
            SetWindowToBottomRightOfScreen();
            this.Opacity = 0;
            uiGridMain.Opacity = 0;

            // Locate these storyboards and "cache" them - we only ever want to find these once for performance reasons
            gridFadeOutStoryBoard = (Storyboard)this.TryFindResource("gridFadeOutStoryBoard");
            gridFadeOutStoryBoard.Completed += gridFadeOutStoryBoard_Completed;
            gridFadeInStoryBoard = (Storyboard)TryFindResource("gridFadeInStoryBoard");
            gridFadeInStoryBoard.Completed += gridFadeInStoryBoard_Completed;
        }

        #region Dependency Properties
        #region Control
        /// <summary>
        /// Gets or sets the control.
        /// </summary>
        /// <value>
        /// The control.
        /// </value>
        public ContentControl Control
        {
            get { return (ContentControl)GetValue(ControlProperty); }
            set { SetValue(ControlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Source.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ControlProperty =
            DependencyProperty.Register("Control", typeof(ContentControl), typeof(NotifyIcon));
        #endregion
        #endregion


        /// <summary>
        /// Does what it says on the tin - ensures the popup window appears at the bottom right of the screen, just above the task bar
        /// </summary>
        private void SetWindowToBottomRightOfScreen()
        {
            Left = SystemParameters.WorkArea.Width - Width - 10;
            Top = SystemParameters.WorkArea.Height - Height;
        }

        /// <summary>
        /// When the notification manager requests the popup to be displayed through this event, perform the below actions
        /// </summary>
        void extendedNotifyIcon_OnShowWindow()
        {
            gridFadeOutStoryBoard.Stop();
            this.Opacity = 1; // Show the window (backing)
            this.Topmost = true; // Very rarely, the window seems to get "buried" behind others, this seems to resolve the problem
            if (uiGridMain.Opacity > 0 && uiGridMain.Opacity < 1) // If its animating, just set it directly to visible (avoids flicker and keeps the UX slick)
            {
                uiGridMain.Opacity = 1;
            }
            else if (uiGridMain.Opacity == 0)
            {
                gridFadeInStoryBoard.Begin();  // If it is in a fully hidden state, begin the animation to show the window
            }
        }

        /// <summary>
        /// When the notification manager requests the popup to be hidden through this event, perform the below actions
        /// </summary>
        void extendedNotifyIcon_OnHideWindow()
        {
            if (PinButton.IsChecked == true)
                return; // Dont hide the window if its pinned open

            gridFadeInStoryBoard.Stop(); // Stop the fade in storyboard if running.

            // Only start fading out if fully faded in, otherwise you get a flicker effect in the UX because the animation resets the opacity
            if (uiGridMain.Opacity == 1 && this.Opacity == 1)
                gridFadeOutStoryBoard.Begin();
            else // Just hide the window and grid
            {
                uiGridMain.Opacity = 0;
                this.Opacity = 0;
            }
        }

        /// <summary>
        /// When the mouse enters the popup window's bounds, cancel any pending closing actions and immediately show the popup.
        /// This is primarily to handle the case where the mouse termporarily leaves the popup window and returns again -
        /// a UX / usability enhancement.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiWindowMainNotification_MouseEnter(object sender, MouseEventArgs e)
        {
            // Cancel the mouse leave event from firing, stop the fade out storyboard from running and enusre the grid is fully visible
            extendedNotifyIcon.StopMouseLeaveEventFromFiring();
            gridFadeOutStoryBoard.Stop();
            uiGridMain.Opacity = 1;
        }

        /// <summary>
        /// If the mouse leaves the popup, start the process to close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiWindowMainNotification_MouseLeave(object sender, MouseEventArgs e)
        {
            extendedNotifyIcon_OnHideWindow();
        }

        /// <summary>
        /// Once the grid fades out, set the backing window to "not visible"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gridFadeOutStoryBoard_Completed(object sender, EventArgs e)
        {
            this.Opacity = 0;
        }

        /// <summary>
        /// Once the grid fades out, set the backing window to "visible"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gridFadeInStoryBoard_Completed(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        /// <summary>
        /// Switch the icon depending on which colour is selected on the radio button group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void colourRadioButton_Click(object sender, RoutedEventArgs e)
        {
            //SetNotifyIcon(((RadioButton)sender).Tag.ToString());
        }

        /// <summary>
        /// Shut down the popup window and dispose the notify icon (otherwise it hangs around in the task bar until you mouse over)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            extendedNotifyIcon.Dispose();
            this.Close();
        }
    }
}