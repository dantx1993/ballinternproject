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

        private void Awake() 
        {
            SpawnBall();
        }

        private void SpawnBall()
        {
            _ballController = Instantiate<BallController>(_ballPrefab, _mapController.StartPoint.position + Vector3.up, Quaternion.identity);
            _inputController.OnChangedAxisInput.Subscribe(_ballController.BallMovement.OnChangedAxisRaw);
            _cameraFollow.Target = _ballController;
        }
    }
}
