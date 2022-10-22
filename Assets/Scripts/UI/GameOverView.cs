using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UniRx;
using TMPro;

namespace BallMaze
{
    public class GameOverView : BaseView
    {
        private const string SCORE_FORMAT = "Your Score: {0}";
        private const string WIN_FORMAT = "YOU WIN!!";
        private const string LOSE_FORMAT = "GAME OVER";

        [SerializeField] private Image _imgHeader1;
        [SerializeField] private Image _imgHeader2;
        [SerializeField] private TextMeshProUGUI _txtHeader;

        [SerializeField] private TextMeshProUGUI _txtScore;

        [SerializeField] private LeaderboardUI leaderboardUI;

        [SerializeField] private Button _btnPlayAgain;

        [SerializeField] private Color _winColor;
        [SerializeField] private Color _loseColor;

        private Subject<bool> _onOpenNewHighScore = new Subject<bool>();

        public IObservable<Unit> OnPlayAgainClicked => _btnPlayAgain.OnClickAsObservable();
        public IObservable<bool> OnOpenNewHighScore => _onOpenNewHighScore;

        public BaseView Show(List<ScoreData> highscore, int yourScore, bool isWin)
        {
            this.Show();

            UpdateHighscore(highscore);

            _imgHeader1.color = isWin ? _winColor : _loseColor;
            _imgHeader2.color = isWin ? _winColor : _loseColor;
            _txtHeader.text = isWin ? WIN_FORMAT : LOSE_FORMAT;

            _txtScore.text = string.Format(SCORE_FORMAT, yourScore);

            if(isWin)
            {
                int higherCount = highscore.Count(s => s.score > yourScore);
                _onOpenNewHighScore.OnNext(higherCount < highscore.Count);
            }

            return this;
        }

        public void UpdateHighscore(List<ScoreData> highscore)
        {
            leaderboardUI.SetData(highscore);
        }

        protected override void ViewAnimation(bool isShow = true, bool isTriggerHideAction = false)
        {
            NoAnim(isShow, isTriggerHideAction);
        }
    }
}