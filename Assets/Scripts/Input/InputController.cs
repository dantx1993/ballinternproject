using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UniRx;

namespace BallMaze
{
    public class InputController : MonoBehaviour
    {
        private bool _isBlockInput = true;
        private Vector3 _inputAxis;
        private Subject<Vector3> _onChangedAxisInput = new Subject<Vector3>();
        private Subject<bool> _onPressedJump = new Subject<bool>();

        public bool IsBlockInput { set => _isBlockInput = value; }
        public IObservable<Vector3> OnChangedAxisInput => _onChangedAxisInput;
        public IObservable<bool> OnPressedJump => _onPressedJump;

        private void Update() 
        {
            if(!_isBlockInput)
            {
                _inputAxis = new Vector3(Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal"));
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    _onPressedJump.OnNext(true);
                }
            }
            else
            {
                _inputAxis = Vector3.zero;
            }
            _onChangedAxisInput.OnNext(_inputAxis);
        }
    }
}
