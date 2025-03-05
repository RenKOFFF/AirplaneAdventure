using System;
using AirplaneAdventure.Gameplay;
using AirplaneAdventure.UI.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AirplaneAdventure.UI
{
    public class GameplayPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private GameOverPanel _gameOverPanel;
        [SerializeField] private Slider _hp;
        
        [SerializeField] private Button _pauseButton;
        [SerializeField] private PausePanel _pausePanel;

        private void Awake()
        {
            _scoreText.text = "0";
            
            _pauseButton.onClick.AddListener(OpenPausePanel);
            
            AirplaneController.OnScoreChanged += OnScoreChanged;
            AirplaneController.OnGameOver += OnGameOver;
            AirplaneController.OnHealthChanged += OnHealthChanged;
        }

        private void OnDestroy()
        {
            AirplaneController.OnScoreChanged -= OnScoreChanged;
            AirplaneController.OnGameOver -= OnGameOver;
            AirplaneController.OnHealthChanged -= OnHealthChanged;
        }

        private void OpenPausePanel()
        {
            _pausePanel.gameObject.SetActive(true);
        }

        private void OnHealthChanged(int hp)
        {
            _hp.value = hp/3f;
        }

        private void OnGameOver()
        {
            _gameOverPanel.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        private void OnScoreChanged(int value)
        {
            _scoreText.text = value.ToString();
        }
    }
}