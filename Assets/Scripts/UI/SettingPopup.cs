using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UniRx;

namespace BallMaze
{
    public class SettingPopup : BaseView
    {
        [SerializeField] private Button _btnResume;
        [SerializeField] private Button _btnExit;

        public IObservable<Unit> OnExitClicked => _btnExit.OnClickAsObservable();

        private void Awake()
        {
            _btnResume.onClick.AddListener(() => this.Hide(true));
        }
    }
}