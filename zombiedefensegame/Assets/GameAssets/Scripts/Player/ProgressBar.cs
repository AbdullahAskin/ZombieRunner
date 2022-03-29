using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace TheyAreComing
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;
        private int _maxHealth;
        private Player _player;
        private Tween _sliderMovementTween;
        public Action<int> ONHealthChange;
        private Player Player => _player ? _player : _player = GetComponent<Player>();

        private void Awake()
        {
            _maxHealth = Player.PlayerCharacterSettings.MaxHealth;
            ONHealthChange += OnHealthChange;

            //Init slider visualization
            healthSlider.maxValue = _maxHealth;
            healthSlider.value = _maxHealth;
        }

        private void OnHealthChange(int currentHealth)
        {
            _sliderMovementTween?.Kill();
            _sliderMovementTween = healthSlider.DOValue(currentHealth, .25f);
        }
    }
}