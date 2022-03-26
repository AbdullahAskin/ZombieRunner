using DG.Tweening;
using Dreamteck.Splines;
using UnityEngine;

namespace TheyAreComing
{
    [RequireComponent(typeof(SplineFollower))]
    public class PlayerSplineManager : MonoBehaviour
    {
        public float horizontalSpeed;
        public float horizontalDeadZone;
        public Vector2 horizontalLimit;
        private SplineFollower _splineFollower;

        public SplineFollower SplineFollower =>
            _splineFollower ? _splineFollower : _splineFollower = GetComponent<SplineFollower>();

        public float CurrentSpeed => SplineFollower.followSpeed;

        public Vector2 SplineMotion
        {
            get => SplineFollower.motion.offset;
            set => SplineFollower.motion.offset =
                new Vector2(Mathf.Clamp(value.x, horizontalLimit.x, horizontalLimit.y), 0);
        }

        public void SetSpeed(float targetSpeed, float duration)
        {
            DOVirtual.Float(SplineFollower.followSpeed, targetSpeed, duration,
                x => { SplineFollower.followSpeed = x; }).SetEase(Ease.Linear);
        }
    }
}