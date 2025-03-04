using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AirplaneAdventure.UI
{
    public class PausePanel : MonoBehaviour
    {
        [SerializeField] private Button _resume;
        [SerializeField] private Button _skins;
        [SerializeField] private Button _toMenu;
        
        private void Awake()
        {
            _resume.onClick.AddListener(ResumeGame);
            _toMenu.onClick.AddListener(ToMenu);
        }

        private void OnEnable()
        {
            Time.timeScale = 0f;
        }
        
        private void OnDisable()
        {
            Time.timeScale = 1f;
        }

        private void ResumeGame()
        {
            gameObject.SetActive(false);
        }

        private void ToMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }
    }
}