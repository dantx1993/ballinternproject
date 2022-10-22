using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BallMaze
{
    public class LeaderboardPopup : BaseView
    {
        [SerializeField] private LeaderboardUI _leaderBoard;
        [SerializeField] private Button _btnClose;

        private void Awake() 
        {
            _btnClose.onClick.AddListener(() => this.Hide());
        }

        public BaseView Show(List<ScoreData> highscore)
        {
            this.Show();
            
            _leaderBoard.SetData(highscore);

            return this;
        }
    }
}