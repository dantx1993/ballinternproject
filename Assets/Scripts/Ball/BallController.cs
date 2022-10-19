using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallMaze
{
    public class BallController : MonoBehaviour
    {
        [SerializeField] private BallMovement _ballMovement;

        public BallMovement BallMovement => _ballMovement;
    }
}
