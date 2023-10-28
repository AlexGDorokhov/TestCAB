﻿using System.Collections.Generic;
using Controllers;
using Controllers.Interfaces;
using Events;
using MyBox;
using Scripts;
using Services;
using UnityEngine;
using UnityEngine.Pool;
using Views.Behaviours;

namespace Views.Mediators
{
    public class EnemiesMediator: BaseMediator<EnemiesBehaviour>, IControllerWithUpdate
    {
        
        private List<EnemyBehaviour> _enemies;
        private float _speed;

        private ObjectPool<EnemyBehaviour> _pool;

        protected override void OnRegister()
        {

            base.OnRegister();
            
            var gameController = ControllersService.Get<GameController>();
            
            _pool = new ObjectPool<EnemyBehaviour>(
                createFunc: () => Behaviour.SpawnEnemy(), 
                actionOnGet: (obj) => obj.gameObject.SetActive(true), 
                actionOnRelease: (obj) => obj.gameObject.SetActive(false), 
                actionOnDestroy: Object.Destroy, 
                defaultCapacity: gameController.EnemiesCount);

            _enemies = new List<EnemyBehaviour>();

        }

        protected override void OnRemove()
        {

            _enemies = null;
            _pool = null;
            
            base.OnRemove();

        }

        public override void AddEventsHandlers()
        {
            base.AddEventsHandlers();
            
            AddListener<CollisionWithEnemyEvent>(CollisionWithEnemy);
            AddListener<DimensionsChangedEvent>(DimensionsChanged);
        }

        public override void RemoveEventsHandlers()
        {
            
            RemoveListener<CollisionWithEnemyEvent>();
            RemoveListener<DimensionsChangedEvent>();
            
            base.RemoveEventsHandlers();
        }

        private void CollisionWithEnemy(CollisionWithEnemyEvent e)
        {
            ClearEnemy(e.GameObject.GetComponent<EnemyBehaviour>());
            SpawnEnemy();
        }

        public override void Show()
        {
            ClearEnemies();

            var gameController = ControllersService.Get<GameController>();
            _speed = gameController.FullMoveSpeed;
            for (int i = 0; i < gameController.EnemiesCount; i++)
            {
                SpawnEnemy();
            }
            
            base.Show();
        }

        public override void Hide()
        {

            ClearEnemies();
            
            base.Hide();
        }
        
        private void DimensionsChanged(DimensionsChangedEvent e)
        {

            for (int i = 0; i < _enemies.Count; i++)
            {
                var enemy = _enemies[i];
                var enemyPosition = enemy.gameObject.transform.position;
                if (enemyPosition.x > Main.SpaceRect.width || enemyPosition.y > Main.SpaceRect.height)
                {
                    ClearEnemy(enemy);
                    SpawnEnemy();
                    continue;
                }

                if (enemy.MoveToPoint.x > Main.SpaceRect.width || enemy.MoveToPoint.y > Main.SpaceRect.height)
                {
                    enemy.MoveToPoint = GetNewRandomMoveToPoint();
                }
            }

            var gameController = ControllersService.Get<GameController>();
            _speed = gameController.FullMoveSpeed;
        }

        private Vector3 GetNewRandomMoveToPoint()
        {
            
            var fRectTransform = Behaviour.GetEnemyRectTransform();
            var fWidth = fRectTransform.rect.width;
            var fHeight = fRectTransform.rect.height;
            var minX = fWidth / 2;
            var minY = fHeight / 2;
            var maxX = Main.SpaceRect.width - minX;
            var maxY = Main.SpaceRect.height - minY;
            var randX = Random.Range(minX, maxX);
            var randY = Random.Range(minY, maxY);

            return new Vector3(randX, randY, 0);
        }

        private void SpawnEnemy()
        {
            var fRectTransform = Behaviour.GetEnemyRectTransform();
            var fWidth = fRectTransform.rect.width;
            var fHeight = fRectTransform.rect.height;
            var minX = fWidth / 2;
            var minY = fHeight / 2;
            var maxX = Main.SpaceRect.width - minX;
            var maxY = Main.SpaceRect.height - minY;

            var position = Vector2.zero;
            var rnd = Random.Range(1, 4);
            switch (rnd)
            {
                case 1:
                    position = new Vector3(minX, minY);
                    break;
                case 2:
                    position = new Vector3(minX, maxY);
                    break;
                case 3:
                    position = new Vector3(maxX, maxY);
                    break;
                case 4:
                    position = new Vector3(maxX, minY);
                    break;
            }
            
            var enemy = _pool.Get();
            var rect = enemy.GetComponent<RectTransform>();
            rect.SetPositionX(position.x);
            rect.SetPositionY(position.y);
            enemy.MoveToPoint = GetNewRandomMoveToPoint();
            _enemies.Add(enemy);
        }

        private void ClearEnemies()
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                Object.Destroy(_enemies[i].gameObject);
            }

            _enemies = new List<EnemyBehaviour>();
        }
        
        private void ClearEnemy(EnemyBehaviour enemy)
        {
            _enemies.Remove(enemy);
            Object.Destroy(enemy.gameObject);
        }

        public override void Update()
        {

            if (!Behaviour.gameObject.activeSelf)
            {
                return;
            }

            for (int i = 0; i < _enemies.Count; i++)
            {
                var enemy = _enemies[i];
                if (Vector3.Distance(enemy.transform.position, enemy.MoveToPoint) < 0.1f)
                {
                    enemy.MoveToPoint = GetNewRandomMoveToPoint();
                }
                
                enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.MoveToPoint, _speed);
            }
            
            base.Update();
        }
    }
}