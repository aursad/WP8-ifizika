using System; 
using System.IO.IsolatedStorage; 
using System.Diagnostics;
using System.Windows.Media;

namespace ifizika.Models
{ 
    public class AppSettings 
    { 

        // Our isolated storage settings 
        readonly IsolatedStorageSettings _settings; 
 
        // The isolated storage key names of our settings 
        const string ColorSettingsKeyName = "ColorSettings";
        private const string SaveDataSettingsKeyName = "SaveDataSettings";
 
        // The default value of our settings 
        private readonly Color _colorSettingDefault = Colors.White;
        private readonly bool _saveDataSettingsDefault = false;
 
        /// <summary> 
        /// Constructor that gets the application settings. 
        /// </summary> 
        public AppSettings() 
        { 
            try 
            { 
                // Get the settings for this application. 
                _settings = IsolatedStorageSettings.ApplicationSettings; 
 
            } 
            catch (Exception e) 
            { 
                Debug.WriteLine("Exception while using IsolatedStorageSettings: " + e.ToString()); 
            } 
        } 
 
        /// <summary> 
        /// Update a setting value for our application. If the setting does not 
        /// exist, then add the setting. 
        /// </summary> 
        /// <param name="key"></param> 
        /// <param name="value"></param> 
        /// <returns></returns> 
        public bool AddOrUpdateValue(string key, Object value) 
        { 
            bool valueChanged = false; 
 
            // If the key exists 
            if (_settings.Contains(key)) 
            { 
                // If the value has changed 
                if (_settings[key] != value) 
                { 
                    // Store the new value 
                    _settings[key] = value; 
                    valueChanged = true; 
                } 
            } 
            // Otherwise create the key. 
            else 
            { 
                _settings.Add(key, value); 
                valueChanged = true; 
            } 
 
            return valueChanged; 
        } 
 
 
        /// <summary> 
        /// Get the current value of the setting, or if it is not found, set the  
        /// setting to the default setting. 
        /// </summary> 
        /// <typeparam name="valueType"></typeparam> 
        /// <param name="key"></param> 
        /// <param name="defaultValue"></param> 
        /// <returns></returns> 
        public valueType GetValueOrDefault<valueType>(string key, valueType defaultValue) 
        { 
            valueType value; 
 
            // If the key exists, retrieve the value. 
            if (_settings.Contains(key)) 
            { 
                value = (valueType)_settings[key]; 
            } 
            // Otherwise, use the default value. 
            else 
            { 
                value = defaultValue; 
            } 
 
            return value; 
        } 
 
 
        /// <summary> 
        /// Save the settings. 
        /// </summary> 
        public void Save() 
        { 
            _settings.Save(); 
        } 
 
 
        /// <summary> 
        /// Property to get and set a CheckBox Setting Key. 
        /// </summary> 
        public Color ColorSetting 
        { 
            get 
            { 
                return GetValueOrDefault(ColorSettingsKeyName, _colorSettingDefault); 
            } 
            set 
            { 
                if (AddOrUpdateValue(ColorSettingsKeyName, value)) 
                { 
                    Save(); 
                } 
            } 
        }

        public bool SaveDataSetting
        {
            get
            {
                return GetValueOrDefault(SaveDataSettingsKeyName, _saveDataSettingsDefault);  
            }
            set
            {
                if (AddOrUpdateValue(SaveDataSettingsKeyName, value))
                {
                    Save();
                }
            }
        }

    } 
} 
