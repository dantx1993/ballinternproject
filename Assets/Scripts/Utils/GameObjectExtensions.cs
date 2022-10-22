using UnityEngine;

namespace BallMaze
{
    public static class GameObjectExtensions
    {
        public static T AddOrGetComponent<T>(this GameObject gameObject) where T : Component
        {
            T component = gameObject.GetComponent<T>();
            if (component == null)
            {
                component = gameObject.AddComponent<T>();
            }
            return component;
        }
        public static void DestroyAllChilren(this Transform parent)
        {
            while (parent.childCount > 0)
                Object.DestroyImmediate(parent.GetChild(0).gameObject);
        }
        public static void DestroyAllChilren(this GameObject parent)
        {
            DestroyAllChilren(parent.transform);
        }
    }
}
