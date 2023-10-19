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
        
        public FruitBehaviour SpawnFruit(float x, float y)
        {
            var fruit = Instantiate(_fruit, transform);
            fruit.gameObject.transform.position = new Vector3(x, y, 0);
            return fruit;
        }
    }
}