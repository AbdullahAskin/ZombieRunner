using System;
using Lean.Touch;
using UnityEngine;

namespace TheyAreComing
{
    public class PlayerStateMovement : PlayerStateBase
    {
        private readonly Vector2 _horizontalLimit;
        private float _horizontalMovementX;

        public PlayerStateMovement(PlayerStateManager playerStateManager, PlayerSplineManager splineManager) : base(
            playerStateManager, splineManager)
        {
            _horizontalLimit = splineManager.horizontalLimit;
        }

        public void ToggleInput(bool bind)
        {
            if (bind) LeanTouch.OnFingerUpdate += HorizontalMovement;
            else LeanTouch.OnFingerUpdate -= HorizontalMovement;
        }

        public override void EnterState()
        {
            ToggleInput(true);
            playerStateManager.playerAnimationController.ToggleWalk(true);
            playerStateManager.playerSplineManager.SetSpeed(10f, 1f);
            _horizontalMovementX = playerStateManager.playerSplineManager.splineMotion.x;
        }

        public override void ExitState()
        {
            ToggleInput(false);
        }

        public override void UpdateState()
        {
            var targetMotionX = Mathf.Lerp(splineManager.splineMotion.x, _horizontalMovementX, .25f);
            splineManager.splineMotion = new Vector2(targetMotionX, 0);
        }

        public override void OnCollisionEnter(Collision collision)
        {
        }

        private void HorizontalMovement(LeanFinger leanFinger)
        {
            if (Math.Abs(leanFinger.ScreenDelta.x) < splineManager.horizontalDeadZone) return;
            var increaseAmount = leanFinger.ScreenDelta.x / Screen.width * splineManager.horizontalSpeed;
            _horizontalMovementX =
                Mathf.Clamp(_horizontalMovementX + increaseAmount, _horizontalLimit.x, _horizontalLimit.y);
        }
    }
}