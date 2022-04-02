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
            _horizontalLimit = Movement.horizontalLimit;
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
            Movement.SetSpeed(CharacterSettings.Speed, 1f);
            _horizontalMovementX = StateManager.PlayerMovement.HorizontalMovementX;
        }

        public override void ExitState()
        {
            ToggleInput(false);
        }

        public override void UpdateState()
        {
            var targetMotionX = Mathf.Lerp(Movement.HorizontalMovementX, _horizontalMovementX, .25f);
            Movement.HorizontalMovementX = targetMotionX;
        }

        private void HorizontalMovement(LeanFinger leanFinger)
        {
            if (Math.Abs(leanFinger.ScreenDelta.x) < Movement.horizontalDeadZone) return;
            var increaseAmount = leanFinger.ScreenDelta.x / Screen.width * Movement.horizontalSpeed;
            _horizontalMovementX =
                Mathf.Clamp(_horizontalMovementX + increaseAmount, _horizontalLimit.x, _horizontalLimit.y);
        }
    }
}