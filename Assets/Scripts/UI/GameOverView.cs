using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UniRx;
using TMPro;

namespace BallMaze
{
    public class GameOverView : BaseView
    {
        [SerializeField] private Image _imgHeader1;
        [SerializeField] private Image _imgHeader2;
        [SerializeField] private TextMeshProUGUI _txtHeader;

        [SerializeField] private TextMeshProUGUI _txtScore;

        [SerializeField] private RankItemUI _rankItemPrefab;
        [SerializeField] private Transform _rankItemParent;

        [SerializeField] private Button _btnPlayAgain;

        public IObservable<Unit> OnPlayAgainClicked => _btnPlayAgain.OnClickAsObservable();

        // public void Set
    }
}