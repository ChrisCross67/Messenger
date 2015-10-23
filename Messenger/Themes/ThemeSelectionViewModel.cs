using MahApps.Metro;
using Messenger.Properties;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Messenger.Themes
{
    public class ThemeSelectionViewModel
    {
        public List<AccentColorMenuData> AccentColors { get; set; }
        public List<AppThemeMenuData> AppThemes { get; set; }

        public ThemeSelectionViewModel()
        {
            Initialize();
        }

        public void Initialize()
        {
            // create accent color menu items for the demo
            this.AccentColors = ThemeManager.Accents
                .Select(
                    a =>
                        new AccentColorMenuData { Name = a.Name, ColorBrush = a.Resources["AccentColorBrush"] as Brush })
                .ToList();

            // create metro theme color menu items for the demo
            this.AppThemes = ThemeManager.AppThemes
                .Select(
                    a =>
                        new AppThemeMenuData
                        {
                            Name = a.Name,
                            Description = a.Name.Equals("BaseLight") ? Resources.Light : a.Name.Equals("BaseDark") ? Resources.Dark : a.Name,
                            BorderColorBrush = a.Resources["BlackColorBrush"] as Brush,
                            ColorBrush = a.Resources["WhiteColorBrush"] as Brush
                        })
                .ToList();

            var defaultAccentData = AccentColors.FirstOrDefault(accent => accent.Name.Equals(Settings.Default.AccentTheme));
            var defaultThemeData = AppThemes.FirstOrDefault(theme => theme.Name.Equals(Settings.Default.AppTheme));

            var defaultAccent = defaultAccentData != null ? ThemeManager.GetAccent(defaultAccentData.Name) : ThemeManager.GetAccent("Violet");
            var defaultTheme = defaultThemeData != null ? ThemeManager.GetAppTheme(defaultThemeData.Name) : ThemeManager.GetAppTheme("BaseLight");
            if(defaultTheme != null)
            {
                ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.DetectAppStyle(Application.Current.MainWindow).Item2, defaultTheme);

            }

        }

    }

    public class AccentColorMenuData
    {
        public string Name { get; set; }
        public Brush BorderColorBrush { get; set; }
        public Brush ColorBrush { get; set; }

        private ICommand changeAccentCommand;

        public ICommand ChangeAccentCommand
        {


            get
            {
                return changeAccentCommand
                    ?? (changeAccentCommand =
                    new RelayCommand(x => DoChangeTheme(x), x => true));
            }
        }

        protected virtual void DoChangeTheme(object sender)
        {
            var theme = ThemeManager.DetectAppStyle(Application.Current.MainWindow);
            var accent = ThemeManager.GetAccent(this.Name);
            ThemeManager.ChangeAppStyle(Application.Current, accent, theme.Item1);
            Settings.Default.AccentTheme = Name;
            Settings.Default.Save();
        }
    }

    public class AppThemeMenuData : AccentColorMenuData
    {
        public string Description { get; internal set; }

        protected override void DoChangeTheme(object sender)
        {
            var theme = ThemeManager.DetectAppStyle(Application.Current.MainWindow);
            var appTheme = ThemeManager.GetAppTheme(Name);
            ThemeManager.ChangeAppStyle(Application.Current, theme.Item2, appTheme);
            Settings.Default.AppTheme = Name;
            Settings.Default.Save();
        }
    }
}
