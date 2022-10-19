using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallMaze
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float posOffsetXZ = -3;
        [SerializeField] private float posOffsetU = 3;
        [SerializeField] private float _eulerOffsetX = 30;

        private BallController _target;

        public BallController Target { set => _target = value; }

        private void LateUpdate() 
        {
            if(_target && _target.BallMovement.ForwardGO)
            {
                Vector3 forward = _target.BallMovement.ForwardGO.transform.forward;
                Vector3 additionPos = new Vector3(forward.x * posOffsetXZ, forward.y, forward.z * posOffsetXZ);
                this.transform.position = _target.transform.position + additionPos + Vector3.up * posOffsetU;
                this.transform.LookAt(_target.transform);
                this.transform.eulerAngles = new Vector3(_eulerOffsetX, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
            }
        }
    }
}