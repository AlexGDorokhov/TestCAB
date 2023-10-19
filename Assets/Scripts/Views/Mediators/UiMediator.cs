using Controllers;
using Events;
using Services;
using Views.Behaviours;

namespace Views.Mediators
{
    public class UiMediator: BaseMediator<UiBehaviour>
    {
        
        protected override void OnRegister()
        {

            base.OnRegister();

            //
        }

        protected override void OnRemove()
        {

            //
            
            base.OnRemove();

        }

        public override void AddEventsHandlers()
        {
            base.AddEventsHandlers();
            
            AddListener<PlayerChangedEvent>(PlayerChanged);
        }

        public override void RemoveEventsHandlers()
        {
            
            RemoveListener<PlayerChangedEvent>();
            
            base.RemoveEventsHandlers();
        }

        private void PlayerChanged(PlayerChangedEvent e)
        {
            Behaviour.SetLives(e.Player.Lives);
            Behaviour.SetPoints(e.Player.Points);

        }

        public override void Show()
        {
            var gameController = ControllersService.Get<GameController>();
            Behaviour.SetLives(gameController.PlayerLives);
            Behaviour.SetPoints(gameController.PlayerPoints);
            base.Show();
        }
    }
}