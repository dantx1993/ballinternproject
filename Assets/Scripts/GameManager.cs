using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace BallMaze
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private BallController _ballPrefab;
        [SerializeField] private InputController _inputController;
        [SerializeField] private MapController _mapController;
        [SerializeField] private CameraFollow _cameraFollow;

        private BallController _ballController;
        private EGameState _gameState;

        public EGameState GameState
        {
            get => _gameState;
            set
            {
                _gameState = value;
                Debug.LogError(_gameState.ToString());
                _inputController.IsBlockInput = _gameState != EGameState.Gameplay;
                switch (_gameState)
                {
                    case EGameState.MainMenu:
                        break;
                    case EGameState.Prepare:
                        break;
                    case EGameState.Gameplay:                        
                        break;
                    case EGameState.GamePause:
                        break;
                    case EGameState.GameOver:
                        break;
                }
            }
        }

        private void Awake() 
        {
            GameState = EGameState.MainMenu;
            SpawnBall();
            GameState = EGameState.Prepare;
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
                Debug.Log($"IsWin {isWin}");
                GameState = EGameState.GameOver;
            }
        }
    }
}
