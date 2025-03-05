using System;
using System.Collections;
using AirplaneAdventure.Gameplay.Saves;
using UnityEngine;
using UnityEngine.UI;

namespace AirplaneAdventure.Gameplay
{
    public class AirplaneController : MonoBehaviour
    {
        [SerializeField] private GameObject _obstacles;

        [SerializeField] private float _tapForce = 400f;
        [SerializeField] private float _gravity = 800f;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private AudioSource _audioSource;
        
        [SerializeField] private Sprite[] _skins;

        private Vector2 _velocity;
        private float _screenTop;
        private float _screenBottom;
        private float _airplaneHalfHeight;

        private int _health = 3;
        private bool _isImmune;
        
        private Collider2D _collider;
        private Image _image;

        public static event Action<int> OnScoreChanged;
        public static event Action<int> OnHealthChanged;
        public static event Action OnGameOver;

        public float Score { get; private set; }

        private void Awake()
        {
            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();
            
            _image = GetComponent<Image>();

            var canvas = GetComponentInParent<Canvas>();
            var canvasHeight = canvas.GetComponent<RectTransform>().rect.height;

            _airplaneHalfHeight = _rectTransform.rect.height / 2;
            _screenTop = canvasHeight / 2f - _airplaneHalfHeight;
            _screenBottom = -canvasHeight / 2f + _airplaneHalfHeight;

            _collider = GetComponent<Collider2D>();
            Score = 0;

            SaveManager.MainData.SkinChanged += ChangeSkin;
            ChangeSkin();
        }

        private void OnDestroy()
        {
            SaveManager.MainData.SkinChanged -= ChangeSkin;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                Flap();
            }
            else
            {
                ApplyGravity();
            }
            
            ClampPosition();

            Score += Time.deltaTime;

            OnScoreChanged?.Invoke((int)Score);
        }

        private void Flap()
        {
            _velocity.y = _tapForce;
        }

        private void ApplyGravity()
        {
            _velocity.y -= _gravity * Time.deltaTime;
            _velocity.y = Mathf.Max(_velocity.y, -_gravity);

            _rectTransform.anchoredPosition += _velocity * Time.deltaTime * Vector2.up;
        }

        private void ClampPosition()
        {
            var position = _rectTransform.anchoredPosition;
            position.y = Mathf.Clamp(position.y, _screenBottom, _screenTop);
            _rectTransform.anchoredPosition = position;
        }

        private void OnCollisionEnter2D(Collision2D _)
        {
            if (_isImmune) return;

            _health--;
            _audioSource.Play();
            OnHealthChanged?.Invoke(_health);

            if (_health <= 0)
            {
                _obstacles.SetActive(false);
                gameObject.SetActive(false);
                OnGameOver?.Invoke();
            }
            else
            {
                StartCoroutine(ActivateImmunity());
            }
        }

        private IEnumerator ActivateImmunity()
        {
            _isImmune = true;
            _collider.enabled = false;

            var blinkInterval = 0.2f;
            var duration = 2f;
            var elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                _image.enabled = !_image.enabled;
                yield return new WaitForSeconds(blinkInterval);
                elapsedTime += blinkInterval;
            }

            _image.enabled = true;
            _collider.enabled = true;
            _isImmune = false;
        }

        private void ChangeSkin()
        {
            _image.sprite = _skins[SaveManager.MainData.Skin];
        }
    }
}