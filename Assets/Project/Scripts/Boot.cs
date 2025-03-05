using AirplaneAdventure.Gameplay.Saves;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AirplaneAdventure
{
    public class Boot : MonoBehaviour
    {
        private void Awake()
        {
            SaveManager.Load();
            SceneManager.LoadScene(1);
        }
    }
}