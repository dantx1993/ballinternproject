using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallMaze
{
    public class Wall : Interacted
    {
        public override void OnInteractByCollision(Interacter interacter)
        {
            if(interacter.GetComponent<BallController>())
            {
                GameManager.Instance.WinGame(false);
            }
        }

        public override void OnInteractByTrigger(Interacter interacter) { }
    }
}
