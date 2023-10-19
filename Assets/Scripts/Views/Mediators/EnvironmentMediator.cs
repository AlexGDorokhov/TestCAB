using Events.Input;
using Views.Behaviours;

namespace Views.Mediators
{
    public class EnvironmentMediator: BaseMediator<EnvironmentBehaviour>
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
            
            AddListener<InputEvent>(MouseInput);
        }

        public override void RemoveEventsHandlers()
        {
            
            RemoveListener<InputEvent>();
            
            base.RemoveEventsHandlers();
        }

        
        private void MouseInput(InputEvent e)
        {
            var x = e.Position.x > 0 ? e.Position.x.ToString() : "-"; 
            var y = e.Position.y > 0 ? e.Position.y.ToString() : "-"; 
            Behaviour.SetTouchPosition(x, y);
        }


    }
}