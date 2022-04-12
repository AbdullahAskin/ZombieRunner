using System;
using Lean.Touch;
using UnityEngine;

namespace TheyAreComing
{
    public class PlayerStateMovement : PlayerStateBase
    {
        private readonly float _movementRange;
        private readonly float _touchDeadZone;
        private float _currentHorizontalX;

        public PlayerStateMovement(PlayerStateManager stateManager) : base(stateManager)
        {
            _touchDeadZone = stateManager.Player.PlayerCharacterSettings.touchDeadZone;
            _movementRange = stateManager.Player.PlayerCharacterSettings.movementRange;
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
            _currentHorizontalX = StateManager.PlayerMovement.HorizontalMovementX;
        }

        public override void ExitState()
        {
            ToggleInput(false);
        }

        public override void UpdateState()
        {
            var targetMotionX = Mathf.Lerp(Movement.HorizontalMovementX, _currentHorizontalX, .25f);
            Movement.HorizontalMovementX = targetMotionX;
        }

        private void HorizontalMovement(LeanFinger leanFinger)
        {
            if (Math.Abs(leanFinger.ScreenDelta.x) < _touchDeadZone) return;
            var increaseAmount = leanFinger.ScreenDelta.x / Screen.width * Movement.HorizontalTouchSpeed;
            _currentHorizontalX =
                Mathf.Clamp(_currentHorizontalX + increaseAmount, -_movementRange, _movementRange);
        }
    }
}