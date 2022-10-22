using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UniRx;

namespace BallMaze
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private GameplayView _gameplayView;
        [SerializeField] private SettingPopup _settingPopup;
        [SerializeField] private HighscorePopup _highscorePopup;
        [SerializeField] private GameOverView _gameOverView;
        [SerializeField] private LeaderboardPopup _leaderboardPopup;

        private void Awake() 
        {
            InitMainView();
            InitGameoverView();
            InitGameplayView();
            InitSettingPopup();
            InitHighscoreView();
            RegisterEvent();
        }

        private void OnChangeGameState(object data)
        {
            EGameState gameState = (EGameState)data;
            switch (gameState)
            {
                case EGameState.MainMenu:
                    _mainMenuView.Show();
                    _gameplayView.HideInstanly();
                    _settingPopup.HideInstanly();
                    _highscorePopup.HideInstanly();
                    _gameOverView.HideInstanly();
                    _leaderboardPopup.HideInstanly();
                    break;
                case EGameState.Prepare:
                    _mainMenuView.HideInstanly();
                    _gameplayView.Show();
                    _settingPopup.HideInstanly();
                    _highscorePopup.HideInstanly();
                    _gameOverView.HideInstanly();
                    _leaderboardPopup.HideInstanly();
                    break;
                case EGameState.Gameplay:
                    break;
                case EGameState.GamePause:
                    _settingPopup.Show();
                    break;
                case EGameState.GameOver:
                    _gameOverView.Show(StorageUserInfo.Instance.HighScore, GameManager.Instance.YourScore, GameManager.Instance.IsGameWin);
                    _settingPopup.HideInstanly();
                    break;
            }
        }
        private void OnChangeGameTime(object data)
        {
            int time = (int)data;
            _gameplayView.ChangeTime(time);
        }

        private void RegisterEvent()
        {
            EventHub.Instance.RegisterEvent(ActionKeyDefine.STATE_KEY, OnChangeGameState);
            EventHub.Instance.RegisterEvent(ActionKeyDefine.COUNTDOWN_KEY, OnChangeGameTime);
        }
        private void InitMainView()
        {
            _mainMenuView.OnPlayGameClicked.Subscribe(_ =>
            {
                GameManager.Instance.GameState = EGameState.Prepare;
            });
            _mainMenuView.OnRankingClicked.Subscribe(_ =>
            {
                _leaderboardPopup.Show(StorageUserInfo.Instance.HighScore);
            });
        }
        private void InitGameplayView()
        {
            _gameplayView.OnSettingClicked.Subscribe(_ =>
            {
                GameManager.Instance.GameState = EGameState.GamePause;
            });
        }
        private void InitSettingPopup()
        {
            _settingPopup.OnExitClicked.Subscribe(_ =>
            {
                SceneManager.LoadScene(SceneDefine.GAMEPLAY);
            });
            _settingPopup.SetOnHide(() =>
            {
                if(GameManager.Instance.GameState == EGameState.GamePause)
                {
                    GameManager.Instance.GameState = EGameState.Gameplay;
                }
            });
        }
        private void InitGameoverView()
        {
            _gameOverView.OnOpenNewHighScore.Subscribe(isOpen =>
            {
                _highscorePopup.Show(GameManager.Instance.YourScore);
            });
            _gameOverView.OnPlayAgainClicked.Subscribe(_ =>
            {
                SceneManager.LoadScene(SceneDefine.GAMEPLAY);
            });
        }
        private void InitHighscoreView()
        {
            _highscorePopup.SetOnHide(() =>
            {
                StorageUserInfo.Instance.HighScore.Add(new ScoreData(_highscorePopup.InputName, GameManager.Instance.YourScore));
                StorageUserInfo.Instance.HighScore = StorageUserInfo.Instance.HighScore.OrderByDescending(s => s.score).ToList();
                if(StorageUserInfo.Instance.HighScore.Count > 5)
                {
                    StorageUserInfo.Instance.HighScore.RemoveAt(StorageUserInfo.Instance.HighScore.Count - 1);
                }
                StorageUserInfo.Instance.SaveNewHighScore(StorageUserInfo.Instance.HighScore);
                _gameOverView.UpdateHighscore(StorageUserInfo.Instance.HighScore);
            });
        }
    }
}
