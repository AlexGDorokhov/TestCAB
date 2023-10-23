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
        
        public EnemyBehaviour SpawnEnemy()
        {
            return Instantiate(_enemy, transform);
        }


    }
}