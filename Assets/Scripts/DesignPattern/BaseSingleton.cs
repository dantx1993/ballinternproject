using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallMaze
{
    public class BaseSingleton<T> where T : BaseSingleton<T>, new()
    {
        public static T Instance
        {
            get
            {
                object obj;
                if (!BaseSingletonCollection.Instances.TryGetValue(typeof(T), out obj))
                {
                    obj = (object)new T();
                    BaseSingletonCollection.Instances.Add(typeof(T), obj);
                    ((T)obj).Init();
                }
                return (T)obj;
            }
        }

        protected virtual void Init()
        {
        }

        public virtual void Load()
        {
        }

        public static void ResetSingleton()
        {
            if (!BaseSingletonCollection.Instances.ContainsKey(typeof(T)))
                return;
            BaseSingletonCollection.Instances.Remove(typeof(T));
        }
    }
}
