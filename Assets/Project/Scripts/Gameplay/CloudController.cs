using UnityEngine;
using UnityEngine.UI;

namespace AirplaneAdventure.Gameplay
{
    public class CloudController : MonoBehaviour
    {
        [SerializeField] private Sprite[] _cloudSprites;
        [SerializeField] private RectTransform _parent;
        [SerializeField] private float _minSpeed = 1f;
        [SerializeField] private float _maxSpeed = 3f;
        
        [SerializeField] private bool _needMove;

        private Image[] _cloudImages;
        private float[] _cloudSpeeds;
        private float[] _cloudHeights;
        
        private float _parentTop;
        private float _parentBottom;
        private float _parentLeft;
        private float _parentRight;

        private void Start()
        {
            _parentTop = _parent.rect.height / 2;
            _parentBottom = -_parent.rect.height / 2;
            _parentLeft = -_parent.rect.width / 2;
            _parentRight = _parent.rect.width / 2;

            _cloudImages = new Image[_cloudSprites.Length];
            _cloudSpeeds = new float[_cloudSprites.Length];
            _cloudHeights = new float[_cloudSprites.Length];

            for (var i = 0; i < _cloudSprites.Length; i++)
            {
                var cloudObject = new GameObject("Cloud");
                cloudObject.transform.SetParent(_parent, false);

                var image = cloudObject.AddComponent<Image>();
                image.sprite = _cloudSprites[i];
                image.preserveAspect = true;
                image.SetNativeSize();

                var rectTransform = cloudObject.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = GetRandomPositionWithinParent(i);

                _cloudImages[i] = image;
                _cloudSpeeds[i] = Mathf.Lerp(_minSpeed, _maxSpeed, (float)i / (_cloudSprites.Length - 1));
                _cloudHeights[i] = rectTransform.rect.height;
            }
        }

        private void Update()
        {
            if (_needMove == false)
                return;
            
            for (var i = 0; i < _cloudImages.Length; i++)
            {
                var rectTransform = _cloudImages[i].rectTransform;
                rectTransform.anchoredPosition += Vector2.down * (_cloudSpeeds[i] * Time.deltaTime);

                if (rectTransform.anchoredPosition.y < _parentBottom - _cloudHeights[i])
                {
                    rectTransform.anchoredPosition = new Vector2(GetRandomX(i), _parentTop + _cloudHeights[i]);
                }
            }
        }

        private Vector2 GetRandomPositionWithinParent(int index)
        {
            return new Vector2(GetRandomX(index), GetRandomY());
        }

        private float GetRandomX(int index)
        {
            var sectionWidth = (_parentRight - _parentLeft) / _cloudSprites.Length;
            var sectionCenter = _parentLeft + sectionWidth * (index + 0.5f);
            var randomOffset = Random.Range(-sectionWidth * 0.4f, sectionWidth * 0.4f);
            
            return sectionCenter + randomOffset;
        }

        private float GetRandomY()
        {
            return Random.Range(_parentBottom, _parentTop);
        }
    }
}