using Controllers.Interfaces;
using Defines.Enums;
using Events.Input;
using Scripts;
using UnityEngine;

namespace Controllers.Input
{
    public class MouseInputController : BaseController, IControllerWithUpdate
    {

        private bool _isMouseDown = false;
        private Vector3 _oldPosition;
            
        public override void Update()
        {

            if (GameController.GameState != GameStates.InProcess)
            {
                return;
            }
            
            var mouseLeftClickDown = UnityEngine.Input.GetMouseButtonDown(0);
            var mouseLeftClick = UnityEngine.Input.GetMouseButton(0);
            var mouseLeftClickUp = UnityEngine.Input.GetMouseButtonUp(0);

            if (UnityEngine.Input.mousePosition.x > 0
                && UnityEngine.Input.mousePosition.x < Main.CanvasRectTransform.rect.width
                && UnityEngine.Input.mousePosition.y > 0
                && UnityEngine.Input.mousePosition.x < Main.CanvasRectTransform.rect.height)
            {
                if (mouseLeftClick)
                { 
                    if (_oldPosition == UnityEngine.Input.mousePosition)
                    {
                        return;
                    }
                    _oldPosition = UnityEngine.Input.mousePosition;
                    _isMouseDown = true;
                    new InputEvent() { Position = UnityEngine.Input.mousePosition }.Fire();
                }
            }
            else if (_isMouseDown)
            {
                _isMouseDown = false;
                new InputEvent() { Position = new Vector3(0, 0, 0) }.Fire();
            }
            
            if (mouseLeftClickUp)
            {
                _isMouseDown = false;
                new InputEvent() { Position = new Vector3(0, 0, 0) }.Fire();
            }

            
        }

    }
}