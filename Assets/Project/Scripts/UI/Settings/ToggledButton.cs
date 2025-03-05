using System;
using UnityEngine;
using UnityEngine.UI;

namespace AirplaneAdventure.UI.Settings
{
	[RequireComponent(typeof(Button))]
	public class ToggledButton : MonoBehaviour
	{
		[SerializeField] private Sprite _backgroundsOn;
		[SerializeField] private Sprite _backgroundsOff;

		[SerializeField] private Image _backImage;

		[SerializeField] private Button _button;

		public bool IsOn { get; private set; }

		public event Action<bool> StateChanged;

		private void OnEnable() => _button.onClick.AddListener(Toggle);

		private void OnDisable() => _button.onClick.RemoveListener(Toggle);

		public void Init(bool defaultState)
		{
			SetValue(defaultState);
		}

		private void Toggle()
		{
			SetValue(!IsOn);
		}

		private void SetValue(bool value)
		{
			IsOn = value;
			
			_backImage.sprite = value ? _backgroundsOn : _backgroundsOff;
			StateChanged?.Invoke(value);
		}
	}
}