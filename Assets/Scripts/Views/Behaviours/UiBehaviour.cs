using TMPro;
using UnityEngine;

namespace Views.Behaviours
{
    public class UiBehaviour : BaseBehaviour
    {

        [SerializeField] private TextMeshProUGUI _lives;
        [SerializeField] private TextMeshProUGUI _points;

        public void SetLives(int val)
        {
            _lives.text = val.ToString();
        }

        public void SetPoints(int val)
        {
            _points.text = val.ToString();
        }
        
    }
}