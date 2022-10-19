using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallMaze
{
    public class Goal : Interacted
    {
        public override void OnInteractByCollision(Interacter interacter) { }

        public override void OnInteractByTrigger(Interacter interacter)
        {
            if (interacter.GetComponent<BallController>())
            {
                GameManager.Instance.WinGame(true);
            }
        }
    }
}
