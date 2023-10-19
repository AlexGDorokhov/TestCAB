using Controllers;
using Events;
using Events.Input;
using Scripts;
using Services;
using UnityEngine;
using Views.Behaviours;

namespace Views.Mediators
{
    public class HeroMediator: BaseMediator<HeroBehaviour>
    {
        
        protected override void OnRegister()
        {

            base.OnRegister();

            var uiRect = Main.CanvasRectTransform;
            Behaviour.JumpTo(uiRect.rect.width / 2, uiRect.rect.height / 2);

            Behaviour.CollisionAction = (go) =>
            {
                if (go.GetComponent<FruitBehaviour>() != null)
                {
                    new CollisionWithFruitEvent() { GameObject =  go } .Fire();
                }
                if (go.GetComponent<EnemyBehaviour>() != null)
                {
                    new CollisionWithEnemyEvent() { GameObject =  go } .Fire();
                }
            };
        }

        protected override void OnRemove()
        {

            Behaviour.CollisionAction = null;
            
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
                var pos = Behaviour.transform.position;
                var vect = pos - new Vector3(pos.x, 5000, 0f);
                var vect2 = new Vector2(pos.x, pos.y) - new Vector2(e.Position.x, e.Position.y);
                var angle = Vector2.Angle(new Vector2(vect.x, vect.y), vect2);
                if (e.Position.x > pos.x)
                {
                    angle *= -1;
                }
                Behaviour.Rotate(angle);
                Behaviour.MoveTo(e.Position.x, e.Position.y);
            }
        }

        public override void Show()
        {
            var gameController = ControllersService.Get<GameController>();
            Behaviour.SetSpeed(gameController.MoveSpeed);
            
            base.Show();
        }
    }
}