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

        public SplineFollower splineFollower => 
            _splineFollower ? _splineFollower : _splineFollower = GetComponent<SplineFollower>();

        public Vector2 splineMotion
        {
            get => splineFollower.motion.offset;
            set => splineFollower.motion.offset =
                new Vector2(Mathf.Clamp(value.x, horizontalLimit.x, horizontalLimit.y), 0);
        }

        public void SetSpeed(float targetSpeed, float duration)
        {
            DOVirtual.Float(splineFollower.followSpeed, targetSpeed, duration,
                x => { splineFollower.followSpeed = x; }).SetEase(Ease.Linear);
        }
    }
}