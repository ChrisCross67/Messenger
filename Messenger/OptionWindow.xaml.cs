
using Messenger.Properties;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel;

namespace Messenger
{
    /// <summary>
    /// OptionWindow.xaml
    /// </summary>
    public partial class OptionWindow
    {
        public OptionWindow()
        {
            InitializeComponent();

            Settings.Default.PropertyChanged += OnSettingsChanged;
        }

        private void OnSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName.Equals("UserName"))
            {
                var self = Messager.Members.FirstOrDefault(member => member.IPAddress.Equals(Messager.Self.IPAddress));
                if (self != null)
                {
                    self.UserName = Settings.Default.UserName;
                    Messager.RefreshMembers();
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string currentEncoding = Messager.TextEncoding.BodyName.ToUpper();
            comboBoxEncodings.Text = currentEncoding;
            comboBoxEncodings.Items.Add("US-ASCII");
            comboBoxEncodings.Items.Add("UTF-8");
            AddEncoding(currentEncoding);
            AddEncoding(Encoding.Default.BodyName.ToUpper());

            //foreach (string file in App.GetIconPaths())
            //{
            //    comBoxIcons.Items.Add(file);
            //}
        }

        private void AddEncoding(string encodingName)
        {
            if (!comboBoxEncodings.Items.Contains(encodingName))
            {
                comboBoxEncodings.Items.Add(encodingName);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
