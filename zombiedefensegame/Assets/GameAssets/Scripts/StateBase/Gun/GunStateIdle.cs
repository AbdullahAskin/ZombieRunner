using DG.Tweening;
using UnityEngine;

namespace TheyAreComing
{
    public class GunStateIdle : GunStateBase
    {
        private Tween _movTween;

        public GunStateIdle(PlayerStateManager stateManager) : base(stateManager)
        {
        }

        public override void EnterState()
        {
            const float start = 70;
            const float end = 110;

            _movTween = AimPivotTrans.DOLocalRotate(start * Vector3.up, end - start).SetEase(Ease.Linear)
                .SetSpeedBased()
                .SetEase(Ease.Linear).OnComplete(() =>
                {
                    _movTween = DOTween.Sequence()
                        .Append(AimPivotTrans.DOLocalRotate(end * Vector3.up, 1.75f).SetEase(Ease.InOutSine))
                        .AppendInterval(.1f)
                        .SetLoops(-1, LoopType.Yoyo);
                });
        }

        public override void ExitState()
        {
            _movTween?.Kill();
        }

        public override void UpdateState()
        {
            if (GunGuide.IsAnyEnemyShootable()) StateManager.SwitchState<GunStateAttack>(1);
        }
    }
}