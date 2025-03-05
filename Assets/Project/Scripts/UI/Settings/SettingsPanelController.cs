using AirplaneAdventure.Gameplay.Saves;
using UnityEngine.Audio;

namespace AirplaneAdventure.UI.Settings
{
    public class SettingsPanelController
    {
        private readonly AudioMixer _audioMixer;

        public bool MusicEnabled { get; private set; }
        public bool SoundEnabled { get; private set; }

        public SettingsPanelController(AudioMixer audioMixer)
        {
            _audioMixer = audioMixer;
        }

        public void SetMusic(bool isOn)
        {
            MusicEnabled = isOn;
            SetMusicEnabled(MusicEnabled);

            SaveManager.SettingsData.Music = isOn;
        }

        public void SetSound(bool isOn)
        {
            SoundEnabled = isOn;
            SetSoundEnabled(SoundEnabled);

            SaveManager.SettingsData.Sounds = isOn;
        }

        private void SetMusicEnabled(bool enabled)
        {
            _audioMixer.SetFloat("MusicVolume", enabled ? 0 : -80);
        }

        private void SetSoundEnabled(bool enabled)
        {
            _audioMixer.SetFloat("SoundVolume", enabled ? 0 : -80);
        }
    }
}