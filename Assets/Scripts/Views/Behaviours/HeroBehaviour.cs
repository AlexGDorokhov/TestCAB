using System;
using DG.Tweening;
using MyBox;
using Services.Attributes;
using Services.Injected;
using UnityEngine;
using Utils;

namespace Views.Behaviours
{
    public class HeroBehaviour : BaseBehaviour
    {

        public Action<GameObject> CollisionAction;
        
        private Vector3 _toPoint;
        private float _speed = 2f;

        [InjectService] private ISoundService _soundService;

        private void Update()
        {
            if (_toPoint == Vector3.zero)
            {
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, _toPoint, _speed);
            
            if (Vector3.Distance(transform.position, _toPoint) < 0.1f)
            {
                _toPoint = Vector3.zero;
            }          
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }
        
        public void MoveTo(float x, float y)
        {
            _toPoint = new Vector3(x, y, 0);
        }
        
        public void Rotate(float degrees, bool immediately = false)
        {
            transform.DORotate(new Vector3(0, 0, degrees), immediately ? 0 : 1);
        }
        
        public void JumpTo(float x, float y)
        {
            transform.position = new Vector3(x, y, 0);
        }


        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.HasComponent<FruitBehaviour>())
            {
                _soundService.PlayFruit();
            }
            if (col.gameObject.HasComponent<EnemyBehaviour>())
            {
                _soundService.PlayEnemy();
            }
            CollisionAction?.Invoke(col.gameObject);
        }

    }
}