using AirplaneAdventure.Gameplay.Saves;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace AirplaneAdventure.UI.Settings
{
    public class SettingsView : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        
        [SerializeField] private ToggledButton _musicButton;
        [SerializeField] private ToggledButton _soundsButton;
        [SerializeField] private AudioMixer _audioMixer;

        private SettingsPanelController _controller;

        private void Start()
        {
            _backButton.onClick.AddListener(() => gameObject.SetActive(false));
            _controller = new SettingsPanelController(_audioMixer);
            
            _musicButton.StateChanged += ToggleMusic;
            _soundsButton.StateChanged += ToggleSound;
            
            _musicButton.Init(SaveManager.SettingsData.Music);
            _soundsButton.Init(SaveManager.SettingsData.Sounds);
            
            gameObject.SetActive(false);
        }
        
        private void ToggleSound(bool isOn) => _controller.SetSound(isOn);

        private void ToggleMusic(bool isOn) => _controller.SetMusic(isOn);
    }
}