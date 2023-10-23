using TMPro;
using UnityEngine;

namespace Views.Behaviours
{
    public class EnvironmentBehaviour : BaseBehaviour
    {

        [SerializeField] private TextMeshProUGUI _touchPositionLabel;
        [SerializeField] private TextMeshProUGUI _lastCollided;
        [SerializeField] private TextMeshProUGUI _lastMouseRaycast;

        public void SetTouchPosition(string x, string y)
        {
            _touchPositionLabel.text = $"Touch position / x: {x}, y: {y} /";
        } 

        public void SetLastCollided(string objectNames)
        {
            _lastCollided.text = $"Last collided: {objectNames}";
        } 

        public void SetLastMouseRaycast(string goName)
        {
            _lastMouseRaycast.text = $"Last mouse raycast hit: {goName}";
        } 
    }
}