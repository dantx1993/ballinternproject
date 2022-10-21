using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UniRx;

namespace BallMaze
{
    public class MainMenuView : BaseView
    {
        [SerializeField] private Button _btnPlayGame;
        [SerializeField] private Button _btnRanking;

        public IObservable<Unit> OnPlayGameClicked => _btnPlayGame.OnClickAsObservable();
        public IObservable<Unit> OnRankingClicked => _btnRanking.OnClickAsObservable();

        protected override void ViewAnimation(bool isShow = true, bool isTriggerHideAction = false)
        {
            NoAnim(isShow, isActiveAndEnabled);
        }
    }
}