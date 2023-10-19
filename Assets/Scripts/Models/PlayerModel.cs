namespace Models
{
    public class PlayerModel
    {

        public int Lives;
        public int Points;

        public PlayerModel()
        {
            Lives = 5;
            Points = 0;
        }
        
        public PlayerModel Clone()
        {
            return new PlayerModel()
            {
                Lives = Lives,
                Points = Points
            };
        }
    }
}