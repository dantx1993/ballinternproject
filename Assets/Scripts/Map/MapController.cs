using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallMaze
{
    public class MapController : MonoBehaviour
    {
        [SerializeField] private Transform _startPoint;

        public Transform StartPoint => _startPoint;
    }
}