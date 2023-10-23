using Controllers;
using Defines.Enums;
using Events;
using Services;
using Views.Behaviours;

namespace Views.Mediators
{
    public class GameOverMediator: BaseMediator<GameOverBehaviour>
    {
        
        protected override void OnRegister()
        {

            base.OnRegister();

            Behaviour.ExitAction = () =>
            {
                new ExitEvent().Fire();
            };

            Behaviour.NewGameAction = () =>
            {
                new ChangeGameStateEvent() { GameState = GameStates.InPrepare } .Fire();
            };

            Behaviour.AddListeners();
        }

        protected override void OnRemove()
        {

            Behaviour.RemoveListeners();
            
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

        public override void Show()
        {
            var savesController = ControllersService.Get<SavesController>();
            var scores = savesController.GetScores();
            var gameController = ControllersService.Get<GameController>();
            var currentPoints = gameController.PlayerPoints;
            
            scores.Sort();
            scores.Reverse();

            var currentPosition = scores.IndexOf(currentPoints);

            base.Show();

            Behaviour.CreateGrid(scores, currentPosition);
        }
    }
}