using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using UniRx;

namespace BallMaze
{
    public class HighscorePopup : BaseView
    {
        public const string CONTENT_FORMAT = "Your score is: <color=green>{0}</color>. You got a highscore. Enter your name:";

        [SerializeField] private TextMeshProUGUI _txtContent;
        [SerializeField] private TMP_InputField _inputName;
        [SerializeField] private Button _btnOK;

        public string InputName => _inputName.text;

        private void Awake() 
        {
            _inputName.onValueChanged.AddListener(OnNameChanged);
            _btnOK.onClick.AddListener(() => this.Hide(true));
        }

        public BaseView Show(int yourScore)
        {
            this.Show();

            _txtContent.text = string.Format(CONTENT_FORMAT, yourScore);

            return this;
        }

        public void OnNameChanged(string input)
        {
            _btnOK.interactable = !string.IsNullOrEmpty(input);
        }
    }
}
