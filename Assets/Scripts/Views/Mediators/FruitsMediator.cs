using System.Collections.Generic;
using Controllers;
using Events;
using Scripts;
using Services;
using UnityEngine;
using UnityEngine.Pool;
using Views.Behaviours;

namespace Views.Mediators
{
    public class FruitsMediator: BaseMediator<FruitsBehaviour>
    {

        private List<FruitBehaviour> _fruits;
        
        private ObjectPool<FruitBehaviour> _pool;
        
        protected override void OnRegister()
        {

            base.OnRegister();

            var gameController = ControllersService.Get<GameController>();

            _pool = new ObjectPool<FruitBehaviour>(
                createFunc: () => Behaviour.SpawnFruit(), 
                actionOnGet: (obj) => obj.gameObject.SetActive(true), 
                actionOnRelease: (obj) => obj.gameObject.SetActive(false), 
                actionOnDestroy: Object.Destroy, 
                defaultCapacity: gameController.EnemiesCount);

            _fruits = new List<FruitBehaviour>();

        }

        protected override void OnRemove()
        {

            _pool = null;
            _fruits = null;
            
            base.OnRemove();

        }

        public override void AddEventsHandlers()
        {
            base.AddEventsHandlers();
            
            AddListener<CollisionWithFruitEvent>(CollisionWithFruit);
        }

        public override void RemoveEventsHandlers()
        {
            
            RemoveListener<CollisionWithFruitEvent>();
            
            base.RemoveEventsHandlers();
        }


        private void CollisionWithFruit(CollisionWithFruitEvent e)
        {
            ClearFruit(e.GameObject.GetComponent<FruitBehaviour>());
            SpawnFruit();
        }

        public override void Show()
        {
            ClearFruits();

            var gameController = ControllersService.Get<GameController>();
            
            for (int i = 0; i < gameController.FruitsCount; i++)
            {
                SpawnFruit();
            }
            
            base.Show();
        }

        public override void Hide()
        {

            ClearFruits();
            
            base.Hide();
        }

        private void SpawnFruit()
        {
            var fRectTransform = Behaviour.GetFruitRectTransform();
            var fWidth = fRectTransform.rect.width;
            var fHeight = fRectTransform.rect.height;
            var minX = fWidth / 2;
            var minY = fHeight / 2;
            var maxX = Main.CanvasRectTransform.rect.width - minX;
            var maxY = Main.CanvasRectTransform.rect.height - minY;

            while (true)
            {
                var randX = Random.Range(minX, maxX);
                var randY = Random.Range(minY, maxY);
                var isPointEmpty = true;
                for (int i = 0; i < _fruits.Count; i++)
                {
                    var pos = _fruits[i].gameObject.transform.position;
                    if (randX > pos.x - fWidth && randX < pos.x + fWidth
                        || randY > pos.y - fHeight && randY < pos.y + fHeight)
                    {
                        isPointEmpty = false;
                        break;
                    }
                }

                if (isPointEmpty)
                {
                    var fruit = _pool.Get();
                    fruit.gameObject.transform.position = new Vector3(randX, randY, 0);
                    _fruits.Add(fruit);
                    return;
                }
            }
            
            
        }

        private void ClearFruits()
        {
            for (int i = 0; i < _fruits.Count; i++)
            {
                Object.Destroy(_fruits[i].gameObject);
            }

            _fruits = new List<FruitBehaviour>();
        }
        
        private void ClearFruit(FruitBehaviour fruit)
        {
            _fruits.Remove(fruit);
            Object.Destroy(fruit.gameObject);
        }
        
    }
}