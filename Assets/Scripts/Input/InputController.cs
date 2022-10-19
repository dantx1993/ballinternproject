using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UniRx;

namespace BallMaze
{
    public class InputController : MonoBehaviour
    {
        private Vector3 _inputAxis;
        private Subject<Vector3> _onChangedAxisInput = new Subject<Vector3>();

        public IObservable<Vector3> OnChangedAxisInput => _onChangedAxisInput;

        private void Update() 
        {
            _inputAxis = new Vector3(Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal"));
            _onChangedAxisInput.OnNext(_inputAxis);
        }
    }
}
