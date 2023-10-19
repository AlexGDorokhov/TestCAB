using Events.Input;
using Views.Behaviours;

namespace Views.Mediators
{
    public class TargetMediator: BaseMediator<TargetBehaviour>
    {
        
        protected override void OnRegister()
        {

            base.OnRegister();
            
            Behaviour.JumpTo(-100 , -100);
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
            if (!Behaviour.gameObject.activeSelf)
            {
                return;
            }

            if (e.Position.x > 0 && e.Position.y > 0)
            {
                Behaviour.JumpTo(e.Position.x, e.Position.y);
            } 
        }

        

    }
}