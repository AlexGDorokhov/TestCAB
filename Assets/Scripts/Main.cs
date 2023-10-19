using Controllers;
using Controllers.Input;
using Defines.Enums;
using Events;
using Services;
using UnityEngine;
using Utils;
using Views.Mediators;

namespace Scripts
{

    public class Main : MonoBehaviour
    {

        [SerializeField] private GameObject _canvas;
        public static GameObject Canvas;
        public static RectTransform CanvasRectTransform;
        private void Start()
        {

            Log.SetAllLogsEnabled();
            Log.LogsEnabledEvent = false;

            Canvas = _canvas;
            CanvasRectTransform = _canvas.GetComponent<RectTransform>();

            InitControllers();
            
            InitUi();
            
            new ChangeGameStateEvent() { GameState = GameStates.InPrepare } .Fire();

        }

        private void InitControllers()
        {
            ControllersService.Bind<GameController>();
            ControllersService.Bind<MouseInputController>(true);
        }

        private void InitUi()
        {
            ControllersService.Bind<EnvironmentMediator>();
            ControllersService.Bind<FruitsMediator>();
            ControllersService.Bind<EnemiesMediator>(true);
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

        protected void OnApplicationQuit()
        {
            //
        }

    }
    
}