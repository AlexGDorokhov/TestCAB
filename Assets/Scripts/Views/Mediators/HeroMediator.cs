using System.Collections.Generic;
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

            var uiRect = Main.SpaceRect;
            Behaviour.JumpTo(uiRect.width / 2, uiRect.height / 2);

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

                new CollidedObjectsEvent() { ObjectNames = new List<string> { Behaviour.gameObject.name, go.name } } .Fire();
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
            AddListener<DimensionsChangedEvent>(DimensionsChanged);
        }

        public override void RemoveEventsHandlers()
        {
            
            RemoveListener<InputEvent>();
            RemoveListener<DimensionsChangedEvent>();
            
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
        
        private void DimensionsChanged(DimensionsChangedEvent e)
        {
            var needToJump = false;
            var pos = Behaviour.transform.position;
            var rect = Behaviour.GetComponent<RectTransform>();
            if (pos.x > Main.SpaceRect.width)
            {
                pos.x = Main.SpaceRect.width - rect.rect.width;
                needToJump = true;
            }

            if (pos.y > Main.SpaceRect.height)
            {
                pos.y = Main.SpaceRect.height - rect.rect.height;
                needToJump = true;
            }

            if (needToJump)
            {
                Behaviour.JumpTo(pos.x, pos.y);
            }
            
            var gameController = ControllersService.Get<GameController>();
            Behaviour.SetSpeed(gameController.FullMoveSpeed);
        }

        public override void Show()
        {
            var gameController = ControllersService.Get<GameController>();
            Behaviour.SetSpeed(gameController.FullMoveSpeed);
            
            base.Show();
        }
    }
}