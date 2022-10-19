using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallMaze
{
    public class BallMovement : MonoBehaviour
    {
        [SerializeField] private float _additionMovementSpeed;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _turnSpeed;
        [SerializeField] private Rigidbody _rigidbody;

        private Vector3 _axisRaw;
        private float _currentSpeed;
        private GameObject _forwardGO;

        public GameObject ForwardGO => _forwardGO;

        private void Awake() 
        {
            _forwardGO = new GameObject("ForwardGO");
            _forwardGO.transform.position = this.transform.position;
            _forwardGO.transform.eulerAngles = this.transform.eulerAngles;
        }

        private void FixedUpdate()
        {
            if(_axisRaw.x != 0)
            {
                float additionZVelocity = (_axisRaw.x > 0 ? 1 : -1) * _additionMovementSpeed * Time.deltaTime;
                bool checkOppositeControl = Vector3.Angle(_rigidbody.velocity.normalized, _forwardGO.transform.forward * additionZVelocity) == 180;
                Vector3 velocity = _rigidbody.velocity + (checkOppositeControl ? 0.5f : 1f) * _forwardGO.transform.forward * additionZVelocity;
                float velMagnitute = velocity.magnitude;
                velMagnitute = Mathf.Clamp(velMagnitute, 0, _maxSpeed);
                velocity = velocity.normalized * velMagnitute;
                _rigidbody.velocity = velocity;
            }
            if(_axisRaw.z != 0)
            {
                float additionYEuler = (_axisRaw.z > 0 ? 1 : -1) * _turnSpeed * Time.deltaTime;
                this.transform.eulerAngles += new Vector3(0, additionYEuler, 0);
                _forwardGO.transform.eulerAngles += new Vector3(0, additionYEuler, 0);
                _rigidbody.velocity = _forwardGO.transform.forward * _rigidbody.velocity.magnitude;
                // _rigidbody.velocity = new Vector3(_rigidbody.velocity.x * _forwardGO.transform.forward.x, _rigidbody.velocity.y * _forwardGO.transform.forward.y, _rigidbody.velocity.z * _forwardGO.transform.forward.z);
            }
        }

        public void OnChangedAxisRaw(Vector3 axisRaw)
        {
            _axisRaw = axisRaw;
        }
    }
}