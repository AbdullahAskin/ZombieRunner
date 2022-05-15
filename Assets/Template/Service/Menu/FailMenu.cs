using DG.Tweening;
using Service;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheyAreComing
{
    public class FailMenu : MenuBase
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI bestScoreText;
        private bool _isClicked;

        public override void Appear()
        {
            base.Appear();
            SetScores();
            _isClicked = false;
        }

        public void Exit()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void RewardedContinue()
        {
            if (_isClicked) return;
            _isClicked = true;
            AdsManager.PlayRewardedAd(RewardedClicked);
        }

        private void RewardedClicked()
        {
            DOVirtual.DelayedCall(1f, () =>
            {
                GameManager.RevivePlayer();
                Disappear();
            });
        }

        private void SetScores()
        {
            scoreText.text = "Score : " + PlayMenu.Score;
            bestScoreText.text = "Best Score : " + GetBestScore();
        }

        private int GetBestScore()
        {
            var currentScore = PlayMenu.Score;
            var bestScore = ES3.Load("bestScore", 0);
            if (currentScore > bestScore) ES3.Save("bestScore", currentScore);
            return ES3.Load("bestScore", currentScore);
        }
    }
}