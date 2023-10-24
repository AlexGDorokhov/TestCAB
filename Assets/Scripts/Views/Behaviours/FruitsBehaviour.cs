using System;
using UnityEngine;

namespace Views.Behaviours
{
    public class FruitsBehaviour : BaseBehaviour
    {

        public Action<FruitBehaviour> FruitFadeoutEndAction;
        
        [SerializeField] private FruitBehaviour _fruit;

        public RectTransform GetFruitRectTransform()
        {
            return _fruit.GetComponent<RectTransform>();
        }
        
        public FruitBehaviour SpawnFruit()
        {
            var fruit = Instantiate(_fruit, transform);
            fruit.FadeoutEndAction = FruitFadeoutEndAction;
            return fruit;
        }

    }
}