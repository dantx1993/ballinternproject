using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallMaze
{
    public class Floor : Interacted
    {
        public override void OnInteractByCollision(Interacter interacter)
        {
            BallController ball = interacter.GetComponent<BallController>();
            if (ball)
            {
                ball.BallMovement.IsOnGround = true;
                if(GameManager.Instance.GameState == EGameState.Prepare)
                {
                    GameManager.Instance.GameState = EGameState.Gameplay;
                }
            }
        }

        public override void OnInteractByTrigger(Interacter interacter) { }
    }
}
