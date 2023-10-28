using Controllers;
using Controllers.Input;
using Defines.Enums;
using Events;
using Services;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Views.Mediators;

namespace Scripts
{

    public class Main : MonoBehaviour
    {

        [SerializeField] private GameObject _canvas;
        public static GameObject Canvas;
        public static Rect SpaceRect;
        private void Start()
        {

            Application.targetFrameRate = 120;

            Log.SetAllLogsEnabled();
            Log.LogsEnabledEvent = false;

            Canvas = _canvas;

            InitControllers();
            
            InitUi();
            
            new DimensionsChangedEvent() { } .Fire();
            
            new ChangeGameStateEvent() { GameState = GameStates.InPrepare } .Fire();
        }

        private void InitControllers()
        {
            ControllersService.Bind<SavesController>();
            ControllersService.Bind<GameController>();
            ControllersService.Bind<MouseInputController>();
        }

        private void InitUi()
        {
            ControllersService.Bind<EnvironmentMediator>();
            ControllersService.Bind<FruitsMediator>();
            ControllersService.Bind<EnemiesMediator>();
            ControllersService.Bind<TargetMediator>();
            ControllersService.Bind<HeroMediator>();
            ControllersService.Bind<UiMediator>();
            ControllersService.Bind<StartScreenMediator>();
            ControllersService.Bind<GameOverMediator>();
        }

        private void Update()
        {
            ControllersService.Update();
        }

        private void FixedUpdate()
        {
            ControllersService.FixedUpdate();
        }

        private void OnRectTransformDimensionsChange()
        {
            var canvasRect = _canvas.GetComponent<RectTransform>();
            var oldRect = SpaceRect;
            SpaceRect = GetSpaceRect(_canvas.GetComponent<RectTransform>());
            var newRect = SpaceRect;
            Log.Message($"SpaceRect dimensions changed from {oldRect.width}x{oldRect.height} to {newRect.width}x{newRect.height}");
            Log.Message($"Canvas {canvasRect.rect.width}x{canvasRect.rect.height}");
            
            new DimensionsChangedEvent() { } .Fire();
        }
        
        
        private Rect GetSpaceRect(RectTransform rect)
        {
            Rect spaceRect = rect.rect;
            Vector3 spacePos = rect.position;
            spaceRect.x = spaceRect.x * rect.lossyScale.x + spacePos.x;
            spaceRect.y = spaceRect.y * rect.lossyScale.y + spacePos.y;
            spaceRect.width = spaceRect.width * rect.lossyScale.x;
            spaceRect.height = spaceRect.height * rect.lossyScale.y;
            return spaceRect;
        }


        
        protected void OnApplicationQuit()
        {
            //
        }

    }
    
}