using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace AirplaneAdventure.Gameplay.Saves
{
    public static class SaveHandler
    {
        private static Dictionary<Type, object> _dataDictionary = new();
        
        private static string _json;
        private static bool _loaded;

        private const string _SAVE = "Save";

        public static void SetValue(Type key, object value, bool save = true)
        {
            if (!_loaded) Load();
            
            if (_dataDictionary.TryAdd(key, value) == false)
            {
                _dataDictionary[key] = value;
            }
            
            if (save) Save();
        }

        public static T GetValue<T>(Type key, T defaultValue = default)
        {
            if (!_loaded) Load();
            
            if (_dataDictionary.TryGetValue(key, out var value))
            {
                try
                {
                    if (value is T o) return o;
                    
                    return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(value)); 
                }
                catch (Exception exception)
                {
                    Debug.LogError(exception);
                }
            }

            return defaultValue;
        }

        public static void Save()
        {
            _json = JsonConvert.SerializeObject(_dataDictionary);
            PlayerPrefs.SetString(_SAVE, _json);
        }
        
        private static void Load()
        {
            _loaded = true;
            
            _json = PlayerPrefs.GetString(_SAVE);
            _dataDictionary = JsonConvert.DeserializeObject<Dictionary<Type, object>>(_json) ?? new Dictionary<Type, object>();
        }
    }
}