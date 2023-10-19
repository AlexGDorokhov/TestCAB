using UnityEngine;

namespace Views.Behaviours
{
    public class TargetBehaviour : BaseBehaviour
    {

        public void JumpTo(float x, float y)
        {
            transform.position = new Vector3(x, y, 0);
        }

    }
}