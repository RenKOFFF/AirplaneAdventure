using UnityEngine;

namespace AirplaneAdventure.Gameplay
{
    public class AirplaneController : MonoBehaviour
    {
        [SerializeField] private float _tapForce = 400f;
        [SerializeField] private float _gravity = 800f;
        [SerializeField] private RectTransform _rectTransform;

        private Vector2 _velocity;
        private float _screenTop;
        private float _screenBottom;
        private float _airplaneHalfHeight;

        private void Awake()
        {
            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();

            Canvas canvas = GetComponentInParent<Canvas>();
            float canvasHeight = canvas.GetComponent<RectTransform>().rect.height;

            _airplaneHalfHeight = _rectTransform.rect.height / 2;
            _screenTop = canvasHeight / 2f - _airplaneHalfHeight;
            _screenBottom = -canvasHeight / 2f + _airplaneHalfHeight;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                Flap();
            }

            ApplyGravity();
            ClampPosition();
        }

        private void Flap()
        {
            _velocity.y = _tapForce;
        }

        private void ApplyGravity()
        {
            _velocity.y -= _gravity * Time.deltaTime;
            
            if (_velocity.y < -_gravity)
                _velocity.y = -_gravity;
            
            _rectTransform.anchoredPosition += _velocity * Time.deltaTime * Vector2.up;
        }

        private void ClampPosition()
        {
            Vector2 position = _rectTransform.anchoredPosition;
            position.y = Mathf.Clamp(position.y, _screenBottom, _screenTop);
            _rectTransform.anchoredPosition = position;
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("Game Over!");
            Time.timeScale = 0;
        }
    }
}