using System;
using System.Collections.Generic;
using UnityEngine;

namespace BallMaze
{
    public class BaseSingletonCollection
    {
        private static readonly Dictionary<Type, object> _instances = new Dictionary<Type, object>();
        public static Dictionary<Type, object> Instances => _instances;
    }
}
