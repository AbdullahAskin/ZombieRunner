using System;
using Lean.Touch;
using UnityEngine;

namespace TheyAreComing
{
    public class PlayerMovementState : PlayerBaseState
    {
        private readonly Vector2 _horizontalLimit;
        private float horizontalMovementX;

        public PlayerMovementState(PlayerStateManager stateManager, PlayerSplineManager splineManager) : base(
            stateManager, splineManager)
        {
            _horizontalLimit = splineManager.horizontalLimit;
        }

        public void ToggleInput(bool bind)
        {
            if (bind) LeanTouch.OnFingerUpdate += HorizontalMovement;
            else LeanTouch.OnFingerUpdate -= HorizontalMovement;
        }

        public override void EnterState(PlayerStateManager playerStateManager)
        {
            ToggleInput(true);
            playerStateManager.playerAnimationController.ToggleWalk(true);
            playerStateManager.playerSplineManager.SetSpeed(10f, 1f);
            horizontalMovementX = playerStateManager.playerSplineManager.splineMotion.x;
        }

        public override void ExitState(PlayerStateManager playerStateManager)
        {
            ToggleInput(false);
        }

        public override void UpdateState(PlayerStateManager playerStateManager)
        {
            var targetMotionX = Mathf.Lerp(splineManager.splineMotion.x, horizontalMovementX, .25f);
            splineManager.splineMotion = new Vector2(targetMotionX, 0);
        }

        public override void OnCollisionEnter(PlayerStateManager playerStateManager, Collision collision)
        {
        }

        private void HorizontalMovement(LeanFinger leanFinger)
        {
            if (Math.Abs(leanFinger.ScreenDelta.x) < splineManager.horizontalDeadZone) return;
            var increaseAmount = leanFinger.ScreenDelta.x / Screen.width * splineManager.horizontalSpeed;
            horizontalMovementX =
                Mathf.Clamp(horizontalMovementX + increaseAmount, _horizontalLimit.x, _horizontalLimit.y);
        }
    }
}