using System;
using AirplaneAdventure.Gameplay.Saves;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace AirplaneAdventure.UI.Leaderboard
{
    public class LeaderboardView : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        
        [SerializeField] private int _leadersCount = 9;
        
        [SerializeField] private LeaderboardLine _leaderboardLinePrefab;
        [SerializeField] private Transform _leadersParent;

        private void Awake()
        {
            _backButton.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
            });
            
            var playerScore = SaveManager.MainData.Score;
            var players = new (string, int)[_leadersCount]; 
            
            for (var i = 0; i < players.Length - 1; i++)
            {
                var randomName = NVJOBNameGen.GiveAName(4);
                var randomScore = Random.Range(100, 1000);
                
                players[i] = (randomName, randomScore); 
            }
            
            players[_leadersCount - 1] = ("YOU", playerScore);
            Array.Sort(players, (x, y) => y.Item2.CompareTo(x.Item2));

            for (var i = 0; i < players.Length; i++)
            {
                var playerName = players[i].Item1;
                var score = players[i].Item2;
                
                var line = Instantiate(_leaderboardLinePrefab, _leadersParent);
                line.SetLine(i + 1, playerName, score);
            }
        }
    }
}