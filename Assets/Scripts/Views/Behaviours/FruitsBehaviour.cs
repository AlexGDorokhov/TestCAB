using UnityEngine;

namespace Views.Behaviours
{
    public class FruitsBehaviour : BaseBehaviour
    {
        [SerializeField] private FruitBehaviour _fruit;

        public RectTransform GetFruitRectTransform()
        {
            return _fruit.GetComponent<RectTransform>();
        }
        
        public FruitBehaviour SpawnFruit()
        {
            var fruit = Instantiate(_fruit, transform);
            return fruit;
        }
    }
}