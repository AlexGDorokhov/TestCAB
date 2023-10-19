using UnityEngine;

namespace Views.Behaviours
{
    public class EnemiesBehaviour	: BaseBehaviour
    {

        [SerializeField] private EnemyBehaviour _enemy;

        public RectTransform GetEnemyRectTransform()
        {
            return _enemy.GetComponent<RectTransform>();
        }
        
        public EnemyBehaviour SpawnEnemy(float x, float y)
        {
            var enemy = Instantiate(_enemy, transform);
            enemy.gameObject.transform.position = new Vector3(x, y, 0);
            return enemy;
        }


    }
}