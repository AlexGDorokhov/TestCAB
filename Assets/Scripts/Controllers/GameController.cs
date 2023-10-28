﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Defines.Constants;
using Defines.Enums;
using Events;
using Models;
using Scripts;
using Services;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using Utils;
using Views.Mediators;

namespace Controllers
{
    public class GameController : BaseController
    {

        private GameSettingsModel _gameSettings; 
        private PlayerModel _player;
        public static GameStates GameState;
        
        private UnityEvent _gameOverEvent = new UnityEvent();

        public override void Init()
        {
            base.Init();

            _gameSettings = new GameSettingsModel().Clone();
            GameState = GameStates.Undefined;
            
        }
        
        public override void AddEventsHandlers()
        {
            base.AddEventsHandlers();

            _gameOverEvent.AddListener(ShowGameOver);

            AddListener<ChangeGameStateEvent>(ChangeGameState);
            AddListener<ChangeGameSettingsEvent>(ChangeGameSettings);
            AddListener<CollisionWithEnemyEvent>(CollisionWithEnemy);
            AddListener<CollisionWithFruitEvent>(CollisionWithFruit);
            AddListener<DimensionsChangedEvent>(DimensionsChanged);
            AddListener<ExitEvent>(Exit);
        }

        public override void RemoveEventsHandlers()
        {
            
            _gameOverEvent.RemoveListener(ShowGameOver);

            RemoveListener<ChangeGameStateEvent>();
            RemoveListener<ChangeGameSettingsEvent>();
            RemoveListener<CollisionWithEnemyEvent>();
            RemoveListener<CollisionWithFruitEvent>();
            RemoveListener<DimensionsChangedEvent>();
            RemoveListener<ExitEvent>();
            
            base.RemoveEventsHandlers();

        }

        public float MoveSpeed
        {
            get => _gameSettings.MoveSpeed;
            set
            {
                if (value < 1)
                {
                    value = 1;
                }
                _gameSettings.MoveSpeed = value;
            }
        }

        public float FullMoveSpeed => _gameSettings.MoveSpeed * _gameSettings.MoveSpeedMultiplayer;

        public float MoveSpeedMultiplayer
        {
            get => _gameSettings.MoveSpeedMultiplayer;
            set
            {
                if (value < 0.1f)
                {
                    value = 0.1f;
                }
                _gameSettings.MoveSpeedMultiplayer = value;
            }
        }

        public int EnemiesCount
        {
            get => _gameSettings.EnemiesCount;
            set
            {
                if (value < 1)
                {
                    value = 1;
                }
                _gameSettings.EnemiesCount = value;
            }
        }

        public int FruitsCount
        {
            get => _gameSettings.FruitsCount;
            set
            {
                if (value < 1)
                {
                    value = 1;
                }
                _gameSettings.FruitsCount = value;
            }
        }

        public int PlayerLives
        {
            get => _player.Lives;
            set
            {
                _player.Lives = value;
                if (value < 1)
                {
                    _gameOverEvent?.Invoke();
                }
            }
        }

        public int PlayerPoints
        {
            get => _player.Points;
            set => _player.Points = value;
        }

        private void Exit(ExitEvent e)
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else  
            Application.Quit();
#endif 
        }

        private void CollisionWithFruit(CollisionWithFruitEvent e)
        {
            PlayerPoints += 1;
            new PlayerChangedEvent() { Player = _player }.Fire();
        }

        private void CollisionWithEnemy(CollisionWithEnemyEvent e)
        {
            PlayerLives -= 1;
            new PlayerChangedEvent() { Player = _player }.Fire();
        }

        private void DimensionsChanged(DimensionsChangedEvent e)
        {
            ChangeMoveSpeedMultiplayer();
        }

        private void ChangeMoveSpeedMultiplayer()
        {
            var canvasRect = Main.Canvas.GetComponent<RectTransform>();
            var moveSpeedMultiplayer = Main.SpaceRect.width > Main.SpaceRect.height ? canvasRect.lossyScale.x : canvasRect.lossyScale.y;
            MoveSpeedMultiplayer = moveSpeedMultiplayer;
            Log.Message($"New speed is {FullMoveSpeed}");
        }

        private void ChangeGameState(ChangeGameStateEvent e)
        {
            switch (e.GameState)
            {
                case GameStates.InPrepare :
                    ShowPrepareGame();
                    break;
                case GameStates.InProcess :
                    ShowGame();
                    break;
                case GameStates.IsFinished :
                    break;
            }
        }

        private void ChangeGameSettings(ChangeGameSettingsEvent e)
        {
            MoveSpeed = e.MoveSpeed;
            EnemiesCount = e.EnemiesCount;
            FruitsCount = e.FruitsCount;
            PlayerLives = e.PlayerLives;
        }
        

        private void HideAllPrefabs()
        {

            //ControllersService.Get<EnvironmentMediator>().Hide();
            ControllersService.Get<FruitsMediator>().Hide();
            ControllersService.Get<EnemiesMediator>().Hide();
            ControllersService.Get<TargetMediator>().Hide();
            ControllersService.Get<HeroMediator>().Hide();
            ControllersService.Get<UiMediator>().Hide();
            ControllersService.Get<StartScreenMediator>().Hide();
            ControllersService.Get<GameOverMediator>().Hide();

        }

        private void ShowPrepareGame()
        {
            _player = new PlayerModel().Clone();
            HideAllPrefabs();
            ControllersService.Get<StartScreenMediator>().Show();
            GameState = GameStates.InPrepare;
        }
        
        private async void ShowGame()
        {
            HideAllPrefabs();
            ControllersService.Get<FruitsMediator>().Show();
            ControllersService.Get<EnemiesMediator>().Show();
            ControllersService.Get<TargetMediator>().Show();
            ControllersService.Get<HeroMediator>().Show();
            ControllersService.Get<UiMediator>().Show();
            await Task.Delay(TimeSpan.FromSeconds(0.05f));
            GameState = GameStates.InProcess;
        }
        
        private void ShowGameOver()
        {
            new SaveScoreEvent() { Points = PlayerPoints } .Fire(); 
            
            HideAllPrefabs();
            ControllersService.Get<GameOverMediator>().Show();
            GameState = GameStates.IsFinished;
        }

    }
}