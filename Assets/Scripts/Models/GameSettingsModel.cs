namespace Models
{
    public class GameSettingsModel
    {
        public float MoveSpeed;
        public float MoveSpeedMultiplayer;
        public int EnemiesCount;
        public int FruitsCount;

        public GameSettingsModel()
        {
            MoveSpeed = 3f;
            MoveSpeedMultiplayer = 1f;
            EnemiesCount = 2;
            FruitsCount = 2;
        }

        public GameSettingsModel Clone()
        {
            return new GameSettingsModel()
            {
                MoveSpeed = MoveSpeed,
                MoveSpeedMultiplayer = MoveSpeedMultiplayer,
                EnemiesCount = EnemiesCount,
                FruitsCount = FruitsCount
            };
        }

    }
}