using Messenger.Properties;
using Messenger.Utils;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows;

namespace Messenger.Locator
{
    /// <summary>
    /// Initializes the <see cref="ViewModelLocator"/> class.
    /// </summary>
    public class ViewModelLocator : NotifyPropertyChangedBase
    {
        /// <summary>
        /// Initializes the <see cref="ViewModelLocator"/> class.
        /// </summary>
        static ViewModelLocator()
        {
            Instance.Languages = new Dictionary<string, string>
            {
                    {"fr-FR", "Français"},
                    {"en-US", "English"}
                };
            Instance.Language = Settings.Default.CultureInfo;

            localizationHelper = new LocalizationHelper();
            localizationHelper.AddResourceManager(Resources.ResourceManager);
        }

        /// <summary>
        /// Gets the instance of the ViewModelLocator  class.
        /// </summary>
        /// <value>
        /// The instance of the ViewModelLocator class.
        /// </value>
        public static ViewModelLocator Instance { get { return Nested.LocatorInstance; } }

        private class Nested
        {
            internal static readonly ViewModelLocator LocatorInstance = new ViewModelLocator();

            // Explicit static constructor to tell C# compiler
            // not to mark type as before field initialized
            static Nested()
            {
            }
        }

        private bool? _isInDesignMode;

        /// <summary>
        /// Gets a value indicating whether the control is in design mode (running in Blend
        /// or Visual Studio).
        /// </summary>
        public bool IsInDesignModeStatic
        {
            get
            {
                if (_isInDesignMode.HasValue)
                    return _isInDesignMode.Value;
#if SILVERLIGHT
 _isInDesignMode = DesignerProperties.IsInDesignTool;
#else
                var prop = DesignerProperties.IsInDesignModeProperty;
                _isInDesignMode
                    = (bool)DependencyPropertyDescriptor
                        .FromProperty(prop, typeof(FrameworkElement))
                        .Metadata.DefaultValue;
#endif

                return _isInDesignMode.Value;
            }
        }


        /// <summary>
        /// The localization helper
        /// </summary>
        private static LocalizationHelper localizationHelper;
        private Dictionary<string, string> _languages;
        private string _language;

        /// <summary>
        /// Gets the localization helper.
        /// </summary>
        /// <value>
        /// The localization helper.
        /// </value>
        public LocalizationHelper LocalizationHelper
        {
            get
            {
                return localizationHelper;
            }
        }

        /// <summary>
        /// Gets or sets the current language.
        /// </summary>
        /// <value>
        /// The current language.
        /// </value>
        public string Language
        {
            get { return _language; }
            set
            {
                if (_language == value)
                    return;
                if (_language != null)
                    LocalizationHelper.UpdateCulture(value);
                else
                    LocalizationHelper.SetDefaultCulture(new CultureInfo(value));
                _language = value;

                Settings.Default.CultureInfo = value;
                Settings.Default.Save();

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the  languages.
        /// </summary>
        /// <value>
        /// The available languages.
        /// </value>
        public Dictionary<string, string> Languages
        {
            get { return _languages; }
            set { _languages = value; OnPropertyChanged(); }
        }
    }
}
