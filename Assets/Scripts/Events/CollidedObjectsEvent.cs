using System.Collections.Generic;

namespace Events
{
    public class CollidedObjectsEvent : BaseEvent
    {
        public List<string> ObjectNames;
    }
}