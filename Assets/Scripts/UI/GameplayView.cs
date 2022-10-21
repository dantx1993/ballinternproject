using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UniRx;

namespace BallMaze
{
    public class GameplayView : BaseView
    {
        private const string TIME_FORMAT = "Time: {0}:{1}";

        [SerializeField] private Button _btnSetting;
        [SerializeField] private Text _txtTime;

        public IObservable<Unit> OnSettingClicked => _btnSetting.OnClickAsObservable();

        protected override void ViewAnimation(bool isShow = true, bool isTriggerHideAction = false)
        {
            NoAnim(isShow, isActiveAndEnabled);
        }

        public void ChangeTime(int time)
        {
            _txtTime.text = string.Format(TIME_FORMAT, (time / 60).ToString("D2"), (time % 60).ToString("D2"));
        }
    } 
}