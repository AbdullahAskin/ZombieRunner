using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using TheyAre;
using UnityEngine;

namespace TheyAreComing
{
    [RequireComponent(typeof(GunGuide))]
    public class GunManager : MonoBehaviour
    {
        [SerializeField] private List<Gun> guns;
        [SerializeField] private Transform aimTargetTrans;
        public float rotateSpeed;
        private bool _canFire = true;
        private Gun _currentGun;
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
            DOVirtual.DelayedCall(_currentGun.fireOffset, () => _canFire = true).OnStart(() => _canFire = false)
                .SetUpdate(UpdateType.Fixed);
            _currentGun.Fire();
            Recoil();
        }

        private void Recoil()
        {
            _recoilTween?.Kill(true);
            _recoilTween = DOTween.Sequence()
                .Append(aimTargetTrans.DOLocalMoveY(-_currentGun.recoilAmount, _currentGun.recoilDuration / 2 - .02f)
                    .SetRelative()
                    .SetEase(Ease.OutSine))
                .Append(aimTargetTrans.DOLocalMoveY(0, _currentGun.recoilDuration / 2 + .02f).SetEase(Ease.InSine));
        }

        private void SwitchGun(int index)
        {
            if (_currentGun && index == _currentGun.transform.GetSiblingIndex()) return;
            if (_currentGun) _currentGun.gameObject.SetActive(false);
            _currentGun = guns[index];
            _currentGun.gameObject.SetActive(true);
            Player.aimIK.solver.transform = _currentGun.aimTrans;
        }

        public void SwitchGunToRandomOne()
        {
            var iCurrentGun = _currentGun.transform.GetSiblingIndex();
            var iTargetGun = SharedFunctions.GetRandomExcept(guns.Count, iCurrentGun);
            SwitchGun(iTargetGun);
        }
    }
}