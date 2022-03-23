using DG.Tweening;
using UnityEngine;

namespace TheyAreComing
{
    [RequireComponent(typeof(GunGuide))]
    public class GunManager : MonoBehaviour
    {
        public Transform aimPivotTrans;
        public Transform aimTargetTrans;
        public Gun currentGun;
        public float rotateSpeed;
        private bool _canFire = true;
        private Tween _recoilTween;

        public void Fire()
        {
            if (!_canFire) return;
            DOVirtual.DelayedCall(currentGun.fireOffset, () => _canFire = true).OnStart(() => _canFire = false);
            currentGun.Fire();
            Recoil();
        }

        private void Recoil()
        {
            _recoilTween?.Kill(true);
            _recoilTween = DOTween.Sequence()
                .Append(aimTargetTrans.DOLocalMoveY(-currentGun.recoilAmount, currentGun.recoilDuration - .02f)
                    .SetRelative()
                    .SetEase(Ease.OutSine))
                .Append(aimTargetTrans.DOLocalMoveY(0, currentGun.recoilDuration + .02f).SetEase(Ease.InSine));
        }
    }
}