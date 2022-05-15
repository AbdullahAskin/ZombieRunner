using DamageNumbersPro;
using UnityEngine;

namespace Service
{
    public class PlayMenu : MenuBase
    {
        public static int Score;
        [SerializeField] private DamageNumber scoreNumber;
        [SerializeField] private DamageNumber scoreStartNumber;
        [SerializeField] private RectTransform scoreParent;

        private void Start()
        {
            Score = 0;
        }

        public override void Appear()
        {
            base.Appear();
            var damageNumber = scoreStartNumber.Spawn(Vector3.zero, 0);
            damageNumber.SetAnchoredPosition(scoreParent, new Vector2(0, 0));
        }

        public void IncreaseScore(int amount)
        {
            Score += amount;
            var damageNumber = scoreNumber.Spawn(Vector3.zero, amount);
            damageNumber.SetAnchoredPosition(scoreParent, new Vector2(0, 0));
        }
    }
}