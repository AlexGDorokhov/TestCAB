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
            var gameController = ControllersService.Get<GameController>();
            var scores = gameController.GetScores();
            var currentPoints = gameController.PlayerPoints;
            
            scores.Sort();
            scores.Reverse();

            var currentPosition = scores.IndexOf(currentPoints);

            Behaviour.CreateGrid(scores, currentPosition);
            
            base.Show();
        }
    }
}