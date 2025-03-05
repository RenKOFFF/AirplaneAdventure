using System;
using AirplaneAdventure.UI.Leaderboard;
using AirplaneAdventure.UI.Settings;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AirplaneAdventure.UI
{
    public class MenuPanel : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _leaderboardButton;
        [SerializeField] private Button _optionsButton;

        [SerializeField] private LeaderboardView _leaderboardView;
        [SerializeField] private SettingsView _settingsView;

        private void Awake()
        {
            _playButton.onClick.AddListener(() => SceneManager.LoadScene(2));
            _leaderboardButton.onClick.AddListener(() => _leaderboardView.gameObject.SetActive(true));
            _optionsButton.onClick.AddListener(() => _settingsView.gameObject.SetActive(true));
        }
    }
}