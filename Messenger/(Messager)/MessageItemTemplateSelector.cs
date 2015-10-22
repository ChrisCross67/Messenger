using Messenger.Protocol;
using System.Windows;
using System.Windows.Controls;

namespace Messenger
{
    public class MessageItemTemplateSelector : DataTemplateSelector
    {

        /// <summary>
        /// Gets or sets the boolean template.
        /// </summary>
        /// <value>
        /// The boolean template.
        /// </value>
        public DataTemplate SenderTemplate { get; set; }

        /// <summary>
        /// Gets or sets the double template.
        /// </summary>
        /// <value>
        /// The double template.
        /// </value>
        public DataTemplate SelfTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;
            var message = item as Message;
            if (element != null && item != null && message != null)
            {
                return string.IsNullOrEmpty(message.SenderHost) ? SelfTemplate : SenderTemplate;
            }

            return null;
        }
    }
}
