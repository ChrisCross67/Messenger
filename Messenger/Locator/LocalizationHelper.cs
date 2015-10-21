using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Messenger.Locator
{
    /// <summary>
    /// This class allow to stock a cache of resource manager and notify all binding properties.
    /// </summary>
    public class LocalizationHelper : INotifyPropertyChanged
    {
        /// <summary>
        /// The cacheResourceManager.
        /// </summary>
        private Dictionary<string, ResourceManager> cacheResourceManager;

        /// <summary>
        /// Event PropertyChangedEventHandler.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Test if Exist the Resource Manager.
        /// </summary>
        /// <param name="resourceManager">The resourceManager.</param>
        /// <returns>Return true if exist.</returns>
        public bool ExistCacheResourceManager(ResourceManager resourceManager)
        {
            var res = from r in cacheResourceManager where r.Value == resourceManager select r;
            return res.Count() != 0;
        }

        /// <summary>
        /// Initializes a new instance of the LocalizationHelper class.
        /// </summary>
        public LocalizationHelper()
        {
            cacheResourceManager = new Dictionary<string, ResourceManager>();
        }

        /// <summary>
        /// Add new resource Manager.
        /// </summary>
        /// <param name="resourceManager"></param>
        public void AddResourceManager(ResourceManager resourceManager)
        {
            string[] splitName = resourceManager.BaseName.Split('.');
            string keyResource = splitName[splitName.Length - 1];
            cacheResourceManager.Add(keyResource, resourceManager);
        }

        /// <summary>
        /// Gets Value associated to a key.
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string this[string Key]
        {
            get
            {
                if (!validKey(Key))
                    throw new ArgumentException(@"Key is not in the valid [ManagerName].[ResourceKey] format");

                var key = GetManagerKey(Key);

                if (cacheResourceManager.ContainsKey(key))
                {
                    var resource = GetResourceKey(Key);
                    return cacheResourceManager[key].GetString(resource);
                }
                else
                {
                    return null;
                }
            }
        }
        public static void SetDefaultCulture(CultureInfo culture)
        {
            Type type = typeof(CultureInfo);

            try
            {
                if (type.GetField("s_userDefaultCulture", BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Static) != null)
                    type.InvokeMember("s_userDefaultCulture",
                                    BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Static,
                                    null,
                                    culture,
                                    new object[] { culture });
                if (type.GetField("s_userDefaultUICulture", BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Static) != null)
                    type.InvokeMember("s_userDefaultUICulture",
                                    BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Static,
                                    null,
                                    culture,
                                    new object[] { culture });
            }
            catch { }

            try
            {
                if (type.GetField("m_userDefaultCulture", BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Static) != null)
                    type.InvokeMember("m_userDefaultCulture",
                                        BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Static,
                                        null,
                                        culture,
                                        new object[] { culture });
                if (type.GetField("m_userDefaultUICulture", BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Static) != null)
                    type.InvokeMember("m_userDefaultUICulture",
                                    BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Static,
                                    null,
                                    culture,
                                    new object[] { culture });
            }
            catch { }
        }
        /// <summary>
        /// Update culture of all resource manager registered.
        /// </summary>
        /// <param name="culture"></param>
        public void UpdateCulture(string culture)
        {
            CultureInfo newCultureInfo = new CultureInfo(culture);

            //Thread.CurrentThread.CurrentCulture = newCultureInfo;
            //Thread.CurrentThread.CurrentUICulture = newCultureInfo;
            SetDefaultCulture(newCultureInfo);
            RaisePropertyChanged(string.Empty);
        }

        /// <summary>
        /// RaisePropertyChanged.
        /// </summary>
        /// <param name="propertyName">The propertyName.</param>
        protected void RaisePropertyChanged(string propertyName)
        {
            var evt = PropertyChanged;

            if (evt != null)
                evt.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Control if is valid key.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool validKey(string input)
        {
            return input.Contains(".");
        }

        /// <summary>
        /// Get manager key.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>Return manager value.</returns>
        private string GetManagerKey(string input)
        {
            return input.Split('.')[0];
        }

        /// <summary>
        /// Get resource key.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>Return resource value.</returns>
        private string GetResourceKey(string input)
        {
            return input.Substring(input.IndexOf('.') + 1);
        }
    }
}