using System;
using System.Collections.Generic;

namespace AirplaneAdventure.Gameplay.Saves
{
    public static class SaveManager
    {
        public static MainData MainData { get; private set; }
        public static SettingsData SettingsData { get; private set; }

        public static void Load()
        {
            MainData = SaveHandler.GetValue(typeof(MainData), new MainData());
            SettingsData = SaveHandler.GetValue(typeof(SettingsData), new SettingsData());
        }

        public static void Save<T>(T value) where T : class
        {
            SaveHandler.SetValue(typeof(T), value);
        }
    }

    [Serializable]
    public class MainData
    {
        public event Action SkinChanged;
        
        private int _score;

        private int _skin;

        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                Save();
            }
        }
        
        public int Skin
        {
            get => _skin;
            set
            {
                _skin = value;
                SkinChanged?.Invoke();
                Save();
            }
        }

        public void Save()
        {
            SaveManager.Save(this);
        }
    }
    
    [Serializable]
    public class SettingsData
    {
        private bool _music = true;
        private bool _sounds = true;

        public bool Music
        {
            get => _music;
            set
            {
                _music = value;
                SaveManager.Save(this);
            }
        }

        public bool Sounds
        {
            get => _sounds;
            set
            {
                _sounds = value;
                SaveManager.Save(this);
            }
        }
    }
}