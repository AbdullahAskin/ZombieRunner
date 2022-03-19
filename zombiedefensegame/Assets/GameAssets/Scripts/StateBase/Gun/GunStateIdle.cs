using DG.Tweening;
using UnityEngine;

namespace TheyAreComing
{
    public class GunStateIdle : GunStateBase
    {
        private readonly TweenParams _movTweenParam = new TweenParams().SetEase(Ease.Linear).SetSpeedBased();
        private Tween _movTween;

        public GunStateIdle(PlayerStateManager stateManager) : base(stateManager)
        {
        }

        private Vector2 AimLimit => GunGuide.aimLimit;

        public override void EnterState()
        {
            var sign = 1;
            var startAngle = AimLimit.x + 20f * sign;
            _movTween = AimPivotTrans.DOLocalRotate((startAngle) * Vector3.up, 60f)
                .SetEase(Ease.Linear).SetSpeedBased().SetLoops(-1,LoopType.Yoyo);


            // _movTween = DOTween.Sequence()
            //     .Append(AimPivotTrans.DOLocalRotate((AimLimit.x + 20) * Vector3.up, 60f).SetAs(_movTweenParam))
            //     .Append(AimPivotTrans.DOLocalRotate((AimLimit.y - 20) * Vector3.up, 60f).SetAs(_movTweenParam))
            //     .SetLoops(-1);
        }

        public override void ExitState()
        {
            _movTween?.Kill();
        }

        public override void UpdateState()
        {
            var enemiesInRange = GunGuide.GetEnemiesInRange();
            if (enemiesInRange.Count != 0) StateManager.SwitchState<GunStateAttack>(1);
        }

        public override void OnCollisionEnter(Collision collision)
        {
        }
    }
}