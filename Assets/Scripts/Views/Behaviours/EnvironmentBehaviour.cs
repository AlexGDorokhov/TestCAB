using TMPro;
using UnityEngine;

namespace Views.Behaviours
{
    public class EnvironmentBehaviour : BaseBehaviour
    {

        [SerializeField] private TextMeshProUGUI _touchPositionLabel;

        public void SetTouchPosition(string x, string y)
        {
            _touchPositionLabel.text = $"Touch position / x: {x}, y: {y} /";
        } 
    }
}