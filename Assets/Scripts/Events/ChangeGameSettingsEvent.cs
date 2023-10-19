namespace Events
{
    public class ChangeGameSettingsEvent : BaseEvent
    {
        public float MoveSpeed;
        public int EnemiesCount;
        public int FruitsCount;
        public int PlayerLives;
    }
}