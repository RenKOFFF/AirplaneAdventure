using System;
using AirplaneAdventure.Gameplay.Saves;
using UnityEngine;
using UnityEngine.UI;

namespace AirplaneAdventure.UI.Settings
{
    public class SkinsPanel : MonoBehaviour
    {
        [SerializeField] private Button[] _buttons;

        private void Awake()
        {
            for (var i = 0; i < _buttons.Length; i++)
            {
                var button = _buttons[i];
                var skinIndex = i;
                button.onClick.AddListener(() =>
                {
                    SaveManager.MainData.Skin = skinIndex;
                    gameObject.SetActive(false);
                });
            }
            
            gameObject.SetActive(false);
        }
    }
}