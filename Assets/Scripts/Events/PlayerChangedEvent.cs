using Models;

namespace Events
{
    public class PlayerChangedEvent : BaseEvent
    {
        public PlayerModel Player;
    }
}