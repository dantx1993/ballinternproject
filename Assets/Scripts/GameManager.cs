using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace BallMaze
{
    public class GameManager : Singleton<GameManager>
    {
        public const int MAX_TIME = 180;

        [SerializeField] private BallController _ballPrefab;
        [SerializeField] private InputController _inputController;
        [SerializeField] private MapController _mapController;
        [SerializeField] private CameraFollow _cameraFollow;

        private BallController _ballController;
        private EGameState _gameState;
        private bool _isGameStart;
        private int _time;
        private bool _isGameWin;
        private int _yourScore;

        public bool IsGameWin => _isGameWin;
        public int YourScore => _yourScore;

        public EGameState GameState
        {
            get => _gameState;
            set
            {
                _gameState = value;
                _inputController.IsBlockInput = _gameState != EGameState.Gameplay;
                EventHub.Instance.UpdateEvent(ActionKeyDefine.STATE_KEY, _gameState);
                switch (_gameState)
                {
                    case EGameState.MainMenu:
                        break;
                    case EGameState.Prepare:
                        _time = MAX_TIME;
                        EventHub.Instance.UpdateEvent(ActionKeyDefine.COUNTDOWN_KEY, _time);
                        SpawnBall();
                        break;
                    case EGameState.Gameplay:
                        if(!_isGameStart)
                        {
                            Countdown();
                            _isGameStart = true;
                        }
                        break;
                    case EGameState.GamePause:
                        break;
                    case EGameState.GameOver:
                        _ballController.BallMovement.Stop();
                        break;
                }
            }
        }

        private void Awake() 
        {
            GameState = EGameState.MainMenu;
            _isGameStart = false;
        }

        private void SpawnBall()
        {
            _ballController = Instantiate<BallController>(_ballPrefab, _mapController.StartPoint.position + Vector3.up * 10, Quaternion.identity);
            _inputController.OnChangedAxisInput.Subscribe(_ballController.BallMovement.OnChangedAxisRaw);
            _inputController.OnPressedJump.Subscribe(_ballController.BallMovement.OnJump);
            _cameraFollow.Target = _ballController;
        }

        public void WinGame(bool isWin = false)
        {
            if(GameState == EGameState.Gameplay || GameState == EGameState.GamePause)
            {
                _isGameWin = isWin;
                CaculateScore(_isGameWin);
                GameState = EGameState.GameOver;
            }
        }

        public void CaculateScore(bool isWin)
        {
            _yourScore = isWin ? _time : 0;
        }

        private void Countdown()
        {
            Observable
                .Interval(System.TimeSpan.FromSeconds(1))
                .TakeWhile(l => l >= 0 && _gameState != EGameState.GameOver)
                .Subscribe(l =>
                {
                    _time -= 1;
                    EventHub.Instance.UpdateEvent(ActionKeyDefine.COUNTDOWN_KEY, _time);
                }, () =>
                {
                    WinGame(false);
                }).AddTo(this.gameObject);
        }
    }
}
;