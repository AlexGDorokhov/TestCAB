using Defines.Enums;

namespace Events
{
    public class ChangeGameStateEvent : BaseEvent
    {
        public GameStates GameState;
    }
}