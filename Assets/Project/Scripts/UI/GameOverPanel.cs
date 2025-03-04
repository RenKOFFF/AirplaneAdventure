using System;
using AirplaneAdventure.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AirplaneAdventure.UI
{
    public class GameOverPanel : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private TMP_Text _scoreText;

        private void Awake()
        {
            _restartButton.onClick.AddListener(RestartGame);
        }

        private void OnEnable()
        {
            var airplaneController = FindObjectOfType<AirplaneController>(true);
            _scoreText.text = ((int)airplaneController.Score).ToString();
        }

        private void RestartGame()
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex);
        }
    }
}