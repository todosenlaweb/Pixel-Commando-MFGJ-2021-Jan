using UnityEngine;

namespace StateImplements
{
    public interface ITarget
    {
        void MoveTo(Vector2 patrolTarget);
    }
}