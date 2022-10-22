using System;
using System.Collections;
using UnityEngine;

namespace BallMaze
{
    public static class MonoBehaviourExtensions
    {
        #region Coroutine Defination
        private static IEnumerator DoAction(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action?.Invoke();
        }
        private static IEnumerator DoActionRealtime(float time, Action action)
        {
            yield return new WaitForSecondsRealtime(time);
            action?.Invoke();
        }
        private static IEnumerator DoActionWaitUntil(Func<bool> func, Action action)
        {
            yield return new WaitUntil(func);
            action?.Invoke();
        }
        private static IEnumerator DoActionWaitWhile(Func<bool> func, Action action)
        {
            yield return new WaitWhile(func);
            action?.Invoke();
        }
        private static IEnumerator DoActionWaitForEndOfFrame(Action action)
        {
            yield return new WaitForEndOfFrame();
            action?.Invoke();
        }
        private static IEnumerator DoActionWhile(Func<bool> func, Action action, Action after = null)
        {
            while (func.Invoke())
            {
                action?.Invoke();
                yield return new WaitForEndOfFrame();
            }
            after?.Invoke();
        }
        #endregion

        #region Extension Method
        public static Coroutine ActionWaitTime(this MonoBehaviour mono, float time, Action action)
        {
            return mono.StartCoroutine(DoAction(time, action));
        }
        public static Coroutine ActionWaitRealTime(this MonoBehaviour mono, float time, Action action, bool isUseMainThread = false)
        {
            return mono.StartCoroutine(DoActionRealtime(time, action));
        }
        public static Coroutine ActionWaitUntil(this MonoBehaviour mono, Func<bool> func, Action action, bool isUseMainThread = false)
        {
            return mono.StartCoroutine(DoActionWaitUntil(func, action));
        }
        public static Coroutine ActionWaitWhile(this MonoBehaviour mono, Func<bool> func, Action action, bool isUseMainThread = false)
        {
            return mono.StartCoroutine(DoActionWaitWhile(func, action));
        }
        public static Coroutine ActionWaitForEndOfFrame(this MonoBehaviour mono, Action action, bool isUseMainThread = false)
        {
            return mono.StartCoroutine(DoActionWaitForEndOfFrame(action));
        }
        public static Coroutine ActionWhile(this MonoBehaviour mono, Func<bool> func, Action action, Action after = null, bool isUseMainThread = false)
        {
            return mono.StartCoroutine(DoActionWhile(func, action, after));
        }
        #endregion
    }
}
