using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using Messenger.Properties;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Messenger.Themes
{
    public class ThemeSelectionViewModel
    {
        private static PaletteHelper paletteHelper;

        public ThemeSelectionViewModel()
        {
            Swatches = new SwatchesProvider().Swatches.Where(swatch => swatch.AccentHues.Any());
            paletteHelper = new PaletteHelper();

            ApplyPrimary(Swatches.FirstOrDefault(swatch => swatch.Name.Equals(Settings.Default.AccentTheme.ToLower())));
            ApplyBase(Settings.Default.IsDarkTheme);
        }

        public ICommand ToggleBaseCommand { get; } = new RelayCommand(o => ApplyBase((bool)o));

        private static void ApplyBase(bool isDark)
        {
            paletteHelper.SetLightDark(isDark);
            Settings.Default.IsDarkTheme = isDark;
            Settings.Default.Save();
        }

        public IEnumerable<Swatch> Swatches { get; }

        public ICommand ApplyPrimaryCommand { get; } = new RelayCommand(o => ApplyPrimary((Swatch)o));

        private static void ApplyPrimary(Swatch swatch)
        {
            paletteHelper.ReplacePrimaryColor(swatch);
        }

        public ICommand ApplyAccentCommand { get; } = new RelayCommand(o => ApplyAccent((Swatch)o));

        private static void ApplyAccent(Swatch swatch)
        {
            paletteHelper.ReplacePrimaryColor(swatch,true);
            paletteHelper.ReplaceAccentColor(swatch);
            Settings.Default.AccentTheme = swatch.Name;
            Settings.Default.Save();
        }
    }
}
