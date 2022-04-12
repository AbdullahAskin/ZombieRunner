using DG.Tweening;
using UnityEngine;

namespace TheyAreComing
{
    public class PlayerMovement : MonoBehaviour
    {
        public Transform movementPivot;
        private float _currentSpeed;
        private Player _player;
        private float MovementRange => Player.PlayerCharacterSettings.movementRange;
        public float HorizontalTouchSpeed => Player.PlayerCharacterSettings.horizontalTouchSpeed;

        private Player Player => _player ? _player : _player = GetComponent<Player>();

        public float HorizontalMovementX
        {
            get => movementPivot.localPosition.x;
            set => movementPivot.localPosition =
                new Vector3(Mathf.Clamp(value, -MovementRange, MovementRange), 0, 0);
        }

        private void FixedUpdate()
        {
            transform.Translate(Vector3.forward * (_currentSpeed * Time.fixedDeltaTime), Space.World);
        }

        public void SetSpeed(float targetSpeed, float duration)
        {
            DOVirtual.Float(_currentSpeed, targetSpeed, duration,
                x => { _currentSpeed = x; }).SetEase(Ease.Linear);
        }
    }
}