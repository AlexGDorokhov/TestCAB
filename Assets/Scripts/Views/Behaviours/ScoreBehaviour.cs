using TMPro;
using UnityEngine;

namespace Views.Behaviours
{
    public class ScoreBehaviour : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _position;
        [SerializeField] private TextMeshProUGUI _score;

        public void SetPosition(int val)
        {
            _position.text = val.ToString();
        }

        public void SetScore(int val)
        {
            _score.text = val.ToString();
        }
    }
}