namespace Models
{
    public class GameSettingsModel
    {
        
        public float MoveSpeed;
        public int EnemiesCount;
        public int FruitsCount;

        public GameSettingsModel()
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR             
            MoveSpeed = 20f;
#else
            MoveSpeed = 2f;
#endif            
            EnemiesCount = 2;
            FruitsCount = 2;
        }

        public GameSettingsModel Clone()
        {
            return new GameSettingsModel()
            {
                MoveSpeed = MoveSpeed,
                EnemiesCount = EnemiesCount,
                FruitsCount = FruitsCount
            };
        }

    }
}