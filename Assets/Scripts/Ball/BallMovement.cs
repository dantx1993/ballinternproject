using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

namespace BallMaze
{
    public class BallMovement : MonoBehaviour
    {
        [SerializeField] private float _additionMovementSpeed;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _turnSpeed;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private Rigidbody _rigidbody;

        private bool _isOnGround = false;
        private bool _isOnBoost = false;
        private Vector3 _axisRaw;
        private float _currentSpeed;
        private GameObject _forwardGO;

        public GameObject ForwardGO => _forwardGO;
        public bool IsOnGround { set => _isOnGround = value; }

        private void Awake() 
        {
            _isOnGround = false;
            _forwardGO = new GameObject("ForwardGO");
            _forwardGO.transform.position = this.transform.position;
            _forwardGO.transform.eulerAngles = this.transform.eulerAngles;
        }

        private void FixedUpdate()
        {
            if (!_isOnGround) return;
            if(_axisRaw.x != 0)
            {
                float additionZVelocity = (_axisRaw.x > 0 ? 1 : -1) * _additionMovementSpeed * Time.deltaTime;
                bool checkOppositeControl = Vector3.Angle(_rigidbody.velocity.normalized, _forwardGO.transform.forward * additionZVelocity) == 180;
                Vector3 velocity = _rigidbody.velocity + (checkOppositeControl ? 0.5f : 1f) * _forwardGO.transform.forward * additionZVelocity;
                float velMagnitute = velocity.magnitude;
                velMagnitute = Mathf.Clamp(velMagnitute, 0, _isOnBoost ? _maxSpeed * 2 : _maxSpeed);
                velocity = velocity.normalized * velMagnitute;
                _rigidbody.velocity = velocity;
            }
            if(_axisRaw.z != 0)
            {
                float additionYEuler = (_axisRaw.z > 0 ? 1 : -1) * _turnSpeed * Time.deltaTime;
                this.transform.eulerAngles += new Vector3(0, additionYEuler, 0);
                _forwardGO.transform.eulerAngles += new Vector3(0, additionYEuler, 0);
                _rigidbody.velocity = _forwardGO.transform.forward * _rigidbody.velocity.magnitude;
            }
            // if(_axisRaw.y != 0)
            // {
            //     Vector3 jumForce = new Vector3(0, _jumpHeight, 0);
            //     _rigidbody.AddForce(jumForce);
            //     Debug.Log(jumForce);
            //     // return;
            // }
        }

        public void OnChangedAxisRaw(Vector3 axisRaw)
        {
            _axisRaw = axisRaw;
        }
        public void OnJump(bool isJump)
        {
            if(!_isOnGround) return;
            Vector3 jumForce = new Vector3(0, _jumpHeight, 0);
            _rigidbody.AddForce(jumForce);
            _isOnGround = false;
        }
        public void OnBoostSpeed(Vector3 direction, float speedBoostMult = 20)
        {
            float angle = Vector3.Angle(_forwardGO.transform.forward, direction);
            float additionZVelocity = speedBoostMult * _additionMovementSpeed * Time.deltaTime;
            Vector3 velocity = _rigidbody.velocity + (angle <= 90 ? 1 : -1) *_forwardGO.transform.forward * additionZVelocity;
            float velMagnitute = velocity.magnitude;
            velMagnitute = Mathf.Clamp(velMagnitute, 0, _maxSpeed * 2);
            velocity = velocity.normalized * velMagnitute;
            _rigidbody.velocity = velocity;
            _isOnBoost = true;
            DOTween.Sequence()
                .AppendInterval(2f)
                .OnComplete(() => 
                {
                    _isOnBoost = false;
                })
                .Play();
        }
        public void Stop()
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }
}