using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace Messenger
{
    /// <summary>
    /// AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow
    {
        public AboutWindow()
        {
            InitializeComponent();


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

            Version = fvi.ProductVersion;
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(weblink.NavigateUri.AbsoluteUri);
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }




        public string Version
        {
            get { return (string)GetValue(VersionProperty); }
            set { SetValue(VersionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Version.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VersionProperty =
            DependencyProperty.Register("Version", typeof(string), typeof(AboutWindow), new PropertyMetadata(""));



    }
}