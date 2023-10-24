using System;
using UnityEngine;

namespace Views.Behaviours
{
    public class FruitBehaviour : MonoBehaviour
    {

        public Action<FruitBehaviour> FadeoutEndAction;
        
        private Animator _animator;
        private string _animFadeout = "FruitFadeout";

        private int _counter = 0;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }


        public void OnRotateEnd()
        {
            _counter++;
            if (_counter > 1)
            {
                _animator.Play(_animFadeout);
            }
        }
        
        public void OnFadeoutEnd()
        {
            FadeoutEndAction?.Invoke(this);
        }
        
    }
}