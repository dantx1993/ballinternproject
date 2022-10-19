using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallMaze
{
    public class SpeedBoost : Interacted
    {
        public override void OnInteractByCollision(Interacter interacter) { }

        public override void OnInteractByTrigger(Interacter interacter)
        {
            BallController ball = interacter.GetComponent<BallController>();
            if(ball)
            {
                ball.BallMovement.OnBoostSpeed(this.transform.forward);
            }
        }
    }
}