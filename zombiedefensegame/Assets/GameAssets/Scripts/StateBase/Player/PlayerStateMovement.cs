using System;
using Lean.Touch;
using UnityEngine;

namespace TheyAreComing
{
    public class PlayerStateMovement : PlayerStateBase
    {
        private readonly Vector2 _horizontalLimit;
        private float _horizontalMovementX;

        public PlayerStateMovement(PlayerStateManager stateManager) : base(stateManager)
        {
            _horizontalLimit = SplineManager.horizontalLimit;
        }

        public void ToggleInput(bool bind)
        {
            if (bind) LeanTouch.OnFingerUpdate += HorizontalMovement;
            else LeanTouch.OnFingerUpdate -= HorizontalMovement;
        }

        public override void EnterState()
        {
            ToggleInput(true);
            AnimationController.ToggleWalk(true);
            SplineManager.SetSpeed(CharacterSettings.Speed, 1f);
            _horizontalMovementX = StateManager.PlayerSplineManager.SplineMotion.x;
        }

        public override void ExitState()
        {
            ToggleInput(false);
        }

        public override void UpdateState()
        {
            var targetMotionX = Mathf.Lerp(SplineManager.SplineMotion.x, _horizontalMovementX, .25f);
            SplineManager.SplineMotion = new Vector2(targetMotionX, 0);
        }

        private void HorizontalMovement(LeanFinger leanFinger)
        {
            if (Math.Abs(leanFinger.ScreenDelta.x) < SplineManager.horizontalDeadZone) return;
            var increaseAmount = leanFinger.ScreenDelta.x / Screen.width * SplineManager.horizontalSpeed;
            _horizontalMovementX =
                Mathf.Clamp(_horizontalMovementX + increaseAmount, _horizontalLimit.x, _horizontalLimit.y);
        }
    }
}