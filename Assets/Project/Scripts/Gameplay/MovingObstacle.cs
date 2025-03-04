using UnityEngine;

namespace AirplaneAdventure.Gameplay
{
    public class MovingObstacle : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 2f;

        private RectTransform _rectTransform;
        private RectTransform _parentRect;
        private float _leftBound;
        private float _rightBound;
        private int _direction = 1;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _parentRect = transform.parent.GetComponent<RectTransform>();

            // _leftBound = -_parentRect.rect.width / 2 + _rectTransform.rect.width / 2;
            // _rightBound = _parentRect.rect.width / 2 - _rectTransform.rect.width / 2;
            _leftBound = -_parentRect.rect.width / 2 - _rectTransform.rect.width / 3;
            _rightBound = _parentRect.rect.width / 2 + _rectTransform.rect.width / 3;
        }

        private void Update()
        {
            MovePlatform();
        }

        private void MovePlatform()
        {
            var newX = _rectTransform.anchoredPosition.x + _moveSpeed * _direction * Time.deltaTime;

            if (newX > _rightBound)
            {
                newX = _rightBound;
                _direction = -1;
            }
            else if (newX < _leftBound)
            {
                newX = _leftBound;
                _direction = 1;
            }

            _rectTransform.anchoredPosition = new Vector2(newX, _rectTransform.anchoredPosition.y);
        }
    }
}