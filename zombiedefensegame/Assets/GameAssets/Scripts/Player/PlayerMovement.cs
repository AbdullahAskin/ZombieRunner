using DG.Tweening;
using UnityEngine;

namespace TheyAreComing
{
    public class PlayerMovement : MonoBehaviour
    {
        public Transform movementPivot;
        public float horizontalSpeed;
        public float horizontalDeadZone;
        public Vector2 horizontalLimit;
        private float _currentSpeed;

        public float HorizontalMovementX
        {
            get => movementPivot.localPosition.x;
            set => movementPivot.localPosition =
                new Vector3(Mathf.Clamp(value, horizontalLimit.x, horizontalLimit.y), 0, 0);
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