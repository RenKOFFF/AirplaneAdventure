using UnityEngine;

namespace AirplaneAdventure.Gameplay
{
    public class RotatingObstacle : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 100f;

        private void Update()
        {
            transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
        }
    }
}