using TMPro;
using UnityEngine;

namespace AirplaneAdventure.UI.Leaderboard
{
    public class LeaderboardLine : MonoBehaviour
    {
        [SerializeField] private GameObject _playerOutline;
        [SerializeField] private TMP_Text _numberText;
        [SerializeField] private TMP_Text _nameText;
        
        public void SetLine(int number, string playerName, int score)
        {
            _numberText.text = $"{number}.";
            _nameText.text = $"{playerName} {score} PTS";
            
            _playerOutline.SetActive(playerName == "YOU");
        }
    }
}