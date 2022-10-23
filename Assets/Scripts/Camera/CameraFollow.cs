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
        [SerializeField] private LayerMask _wallLayer;

        private BallController _target;
        private RaycastHit _hit;

        public BallController Target { set => _target = value; }

        private void LateUpdate() 
        {
            if(_target && _target.BallMovement.ForwardGO)
            {
                Vector3 forward = _target.BallMovement.ForwardGO.transform.forward;
                Vector3 additionPos = new Vector3(forward.x * posOffsetXZ, forward.y, forward.z * posOffsetXZ);
                Vector3 desiredPosition = _target.transform.position + additionPos + Vector3.up * posOffsetU;
                Vector3 targetPos;


                Debug.DrawRay(_target.transform.position, (desiredPosition - _target.transform.position).normalized * 1.5f, Color.red);
                if (Physics.Raycast(_target.transform.position, desiredPosition - _target.transform.position, out _hit, (desiredPosition - _target.transform.position).magnitude + 0.5f, _wallLayer))
                {
                    Debug.Log("Hit case");
                    targetPos = (_hit.point - _target.transform.position) * 0.85f + _target.transform.position;
                }
                else
                {
                    targetPos = desiredPosition;
                }

                this.transform.position = targetPos;
                this.transform.LookAt(_target.transform);
                this.transform.eulerAngles = new Vector3(_eulerOffsetX, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
            }
        }
    }
}