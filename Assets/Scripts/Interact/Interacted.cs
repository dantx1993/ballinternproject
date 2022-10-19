using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallMaze
{
    public abstract class Interacted : MonoBehaviour
    {
        public abstract void OnInteractByCollision(Interacter interacter);
        public abstract void OnInteractByTrigger(Interacter interacter);

        private void OnTriggerEnter(Collider other) 
        {
            Interacter interacter = other.GetComponent<Interacter>();
            if(interacter)
            {
                OnInteractByTrigger(interacter);
            }
        }
        private void OnCollisionEnter(Collision other) 
        {
            Interacter interacter = other.gameObject.GetComponent<Interacter>();
            if (interacter)
            {
                OnInteractByCollision(interacter);
            }
        }
    }
}