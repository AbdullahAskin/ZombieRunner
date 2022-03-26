using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using UnityEngine;

namespace TheyAreComing
{
    [RequireComponent(typeof(GunGuide))]
    public class GunManager : MonoBehaviour
    {
        public List<Gun> guns;
        public Transform aimTargetTrans;
        public Gun currentGun;
        public float rotateSpeed;
        private bool _canFire = true;
        private Player _player;
        private Tween _recoilTween;
        private Player Player => _player ? _player : _player = GetComponent<Player>();

        private void Awake()
        {
            SwitchGun(0);
        }

        public void Fire()
        {
            if (!_canFire) return;
            MMVibrationManager.Haptic(HapticTypes.LightImpact);
            DOVirtual.DelayedCall(currentGun.fireOffset, () => _canFire = true).OnStart(() => _canFire = false)
                .SetUpdate(UpdateType.Fixed);
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

        public void SwitchGun(int index)
        {
            if (index == currentGun.transform.GetSiblingIndex()) return;

            currentGun.gameObject.SetActive(false);
            currentGun = guns[index];
            currentGun.gameObject.SetActive(true);
            Player.aimIK.solver.transform = currentGun.aimTrans;
        }
    }
}