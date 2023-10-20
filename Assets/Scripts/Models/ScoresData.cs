using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    [CreateAssetMenu(fileName = "New ScoresData", menuName = "Scores Data", order = 51)]
    public class ScoresData : ScriptableObject
    {
        [SerializeField] private List<int> _scores;
        
        public List<int> GetScores()
        {
            return _scores;
        }
       
        public void AddScore(int score)
        {
            if (_scores == null)
            {
                _scores = new List<int>();
            }
            _scores.Add(score);
        }

        public int GetCount()
        {
            return _scores.Count;
        }
    }
}