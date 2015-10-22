
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows;

namespace Messenger
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public MainWindow SendMessageWindow;

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string msg = string.Format("An unhandled exception has occurred.{0}{0}{1}",
                              Environment.NewLine,
                              e.Exception);
            Messager.LogError(msg);

            File.WriteAllText(Path.Combine(AppFolder, "Messager.log"), Messager.Logger.Messages);
        }

        public static string AppFolder
        {
            get
            {
                return Path.GetDirectoryName(Application.ResourceAssembly.Location);
            }
        }

        public static Version GetVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        }

        public static List<string> GetIconPaths()
        {
            List<string> iconPaths = new List<string>();
            //iconPaths.Add("Icons/IPMSG.ICO");
            //iconPaths.Add("Icons/screen.ico");
            //iconPaths.Add("Icons/computer.ico");
            //iconPaths.Add("Icons/man.ico");
            //iconPaths.Add("Icons/boy.ico");
            //iconPaths.Add("Icons/girl.ico");
            string iconFolder = Path.Combine(AppFolder, "Icons");
            if (Directory.Exists(iconFolder))
            {
                iconPaths.AddRange(Directory.GetFiles(iconFolder));
            }
            return iconPaths;
        }

        public static byte[] LoadIcon(int iconIndex)
        {
            List<string> iconPaths = GetIconPaths();
            string path = iconPaths[iconIndex];
            if (File.Exists(path))
            {
                Icon icon = Icon.ExtractAssociatedIcon(path);
                MemoryStream stream = new MemoryStream();
                icon.Save(stream);
                return stream.ToArray();
            }
            else
            {
                Stream stream = GetResourceStream(new Uri(path, UriKind.Relative)).Stream;
                byte[] data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);
                return data;
            }
        }
    }
}
