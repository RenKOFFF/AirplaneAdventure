using UnityEngine;
using System.Collections.Generic;

namespace AirplaneAdventure.Gameplay
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private RectTransform[] _obstaclePrefabs;
        [SerializeField] private RectTransform _parent;
        
        [SerializeField] private int _poolSize = 10;
        [SerializeField] private float _spawnInterval = 2f;
        [SerializeField] private float _obstacleSpeed = 3f;
        [SerializeField] private float _centerX = 0f;

        private Queue<RectTransform> _obstaclePool;
        private float _screenTop;
        private float _screenBottom;

        private void Start()
        {
            _screenTop = Screen.height / 2f;
            _screenBottom = -Screen.height / 2f;

            InitializePool();
            StartCoroutine(SpawnObstacles());
        }

        private void InitializePool()
        {
            _obstaclePool = new Queue<RectTransform>();

            for (var i = 0; i < _poolSize; i++)
            {
                var randomIndex = Random.Range(0, _obstaclePrefabs.Length);
                var obstacle = Instantiate(_obstaclePrefabs[randomIndex].gameObject, _parent);
                var obstacleRect = obstacle.GetComponent<RectTransform>();
                obstacleRect.gameObject.SetActive(false);
                _obstaclePool.Enqueue(obstacleRect);
            }
        }

        private System.Collections.IEnumerator SpawnObstacles()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnInterval);

                if (_obstaclePool.Count > 0)
                {
                    var obstacle = _obstaclePool.Dequeue();
                    obstacle.gameObject.SetActive(true);
                    obstacle.anchoredPosition = new Vector2(_centerX, _screenTop + obstacle.rect.height / 2);

                    StartCoroutine(MoveObstacle(obstacle));
                }
            }
        }

        private System.Collections.IEnumerator MoveObstacle(RectTransform obstacle)
        {
            while (obstacle.anchoredPosition.y > _screenBottom - obstacle.rect.height / 2)
            {
                obstacle.anchoredPosition += Vector2.down * (_obstacleSpeed * Time.deltaTime);
                yield return null;
            }

            obstacle.gameObject.SetActive(false);
            _obstaclePool.Enqueue(obstacle);
        }
    }
}