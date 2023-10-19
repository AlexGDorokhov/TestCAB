using Controllers;
using Defines.Enums;
using Events;
using Services;
using Utils;
using Views.Behaviours;

namespace Views.Mediators
{
    public class StartScreenMediator: BaseMediator<StartScreenBehaviour>
    {
        
        protected override void OnRegister()
        {

            base.OnRegister();

            Behaviour.ChangeMoveSpeedAction = (str) =>
            {
                float val;
                if (float.TryParse(str, out val))
                {
                    if (val > 10)
                    {
                        Log.Error($"Speed cannot be greater than 10");
                    }
                    else
                    {
                        ChangeGameSettingsEvent(val, -1, -1, -1);
                    }
                }
                else
                {
                    Log.Error($"{str} is not a float");
                }
            };
            Behaviour.ChangeEnemiesCountAction = (str) =>
            {
                int val;
                if (int.TryParse(str, out val))
                {
                    if (val > 10)
                    {
                        Log.Error($"Enemies count cannot be greater than 10");
                    }
                    else
                    {
                        ChangeGameSettingsEvent(-1, val, -1, -1);
                    }
                }
                else
                {
                    Log.Error($"{str} is not an int");
                }
            };
            Behaviour.ChangeFruitsCountAction = (str) =>
            {
                int val;
                if (int.TryParse(str, out val))
                {
                    if (val > 10)
                    {
                        Log.Error($"Fruits count cannot be greater than 10");
                    }
                    else
                    {
                        ChangeGameSettingsEvent(-1, -1, val, -1);
                    }
                }
                else
                {
                    Log.Error($"{str} is not an int");
                }
            };
            Behaviour.ChangeLivesAction = (str) =>
            {
                int val;
                if (int.TryParse(str, out val))
                {
                    if (val > 10)
                    {
                        Log.Error($"Lives cannot be greater than 10");
                    }
                    else
                    {
                        ChangeGameSettingsEvent(-1, -1, -1, val);
                    }
                }
                else
                {
                    Log.Error($"{str} is not an int");
                }
            };

            Behaviour.ButtonPlayClickedAction = () =>
            {
                var gameController = ControllersService.Get<GameController>();
                new ChangeGameStateEvent() { GameState = GameStates.InProcess } .Fire();
            };
        }

        protected override void OnRemove()
        {

            Behaviour.RemoveListeners();
            
            Behaviour.ChangeMoveSpeedAction = null;
            Behaviour.ChangeEnemiesCountAction = null;
            Behaviour.ChangeFruitsCountAction = null;
            Behaviour.ChangeLivesAction = null;
            
            base.OnRemove();

        }

        public override void AddEventsHandlers()
        {
            base.AddEventsHandlers();
            
            //
        }

        public override void RemoveEventsHandlers()
        {
            
            //
            
            base.RemoveEventsHandlers();
        }

        private void ChangeGameSettingsEvent(float moveSpeed = -1, int enemiesCount = -1, int fruitsCount = -1, int lives = -1)
        {
            var gameController = ControllersService.Get<GameController>();
            new ChangeGameSettingsEvent()
            {
                MoveSpeed = moveSpeed == -1 ?  gameController.MoveSpeed : moveSpeed,
                EnemiesCount = enemiesCount == -1 ?  gameController.EnemiesCount : enemiesCount,
                FruitsCount = fruitsCount == -1 ?  gameController.FruitsCount : fruitsCount,
                PlayerLives = lives == -1 ?  gameController.PlayerLives : lives,
            } .Fire();
        }

        public override void Show()
        {
            var gameController = ControllersService.Get<GameController>();
            Behaviour.Init(gameController.MoveSpeed, gameController.EnemiesCount, gameController.FruitsCount, gameController.PlayerLives);
            base.Show();
        }
        

        

    }
}