using System;
using System.Collections.Generic;
using UnityEngine;

// using Twee


namespace BallMaze
{
    public class BaseView : MonoBehaviour
    {
        [Space, Header("View")]
        [SerializeField] protected Transform _root;
        [SerializeField] protected Transform _animObject;

        private Action _onHideView;
        private Action _onShowView;
        protected LTDescr _anim;

        protected virtual void OnDisable() 
        {
            if(_anim != null)
            {
                LeanTween.cancel(_anim.id);
            }
        }

        public virtual BaseView Show()
        {
            _root.gameObject.SetActive(true);
            ViewAnimation();
            return this;
        }
        public virtual void Hide(bool isTriggerHideAction = false)
        {
            ViewAnimation(false, isTriggerHideAction);
        }
        public virtual void HideInstanly()
        {
            _root.gameObject.SetActive(false);
        }
        public BaseView SetOnHide(Action onHideView)
        {
            _onHideView = onHideView;
            return this;
        }
        public BaseView SetOnShow(Action onShowView)
        {
            _onShowView = onShowView;
            return this;
        }
        protected virtual void ViewAnimation(bool isShow = true, bool isTriggerHideAction = false)
        {
            ScaleAnim(isShow, isTriggerHideAction);
        }

        #region Anim Show View
        protected void ScaleAnim(bool isShow, bool isTriggerHideAction)
        {
            if (!_root && isTriggerHideAction)
            {
                if (isTriggerHideAction) _onHideView?.Invoke();
                _root.gameObject.SetActive(false);
                return;
            }

            if(!_animObject)
            {
                _animObject = _root;
            }
            _animObject.localScale = isShow ? Vector3.zero : Vector3.one;
            _anim = LeanTween.value(_animObject.gameObject, v => _animObject.localScale = v, _animObject.localScale, isShow ? Vector3.one : Vector3.zero, 0.1f)
                .setEase(LeanTweenType.linear)
                .setOnComplete(() =>
                {
                    if (!isShow)
                    {
                        if (isTriggerHideAction) _onHideView?.Invoke();
                        _root.gameObject.SetActive(false);
                    }
                    else
                    {
                        _onShowView?.Invoke();
                    }
                });
        }
        protected void NoAnim(bool isShow, bool isTriggerHideAction)
        {
            if (!isShow)
            {
                this.ActionWaitForEndOfFrame(() =>
                {
                    if (isTriggerHideAction) _onHideView?.Invoke();
                    _root.gameObject.SetActive(false);
                });
                return;
            }
            this.ActionWaitTime(0.1f, () =>
            {
                _onShowView?.Invoke();
            });
            _root.gameObject.SetActive(true);
        }
        #endregion
    }
}