using UnityEngine;

namespace StateImplements
{
    public class TargetFinder:MonoBehaviour
    {
        [SerializeField] private float countToClose, countToCloseForShoot;
        public bool PlayerIsclose(Vector2 positionFromFind)
        {
            var findWithTag = GameObject.FindWithTag("Player");
            var playerIsclose = (positionFromFind - (Vector2) findWithTag.transform.position).sqrMagnitude <= countToClose;
            Debug.Log($"Player is close {(positionFromFind - (Vector2) findWithTag.transform.position).sqrMagnitude} {playerIsclose}");
            return playerIsclose;
        }

        public Vector2 GetPositionPlayer()
        {
            var findWithTag = GameObject.FindWithTag("Player");
            return findWithTag.transform.position;
        }

        public bool PlayerIsCloseForShoot(Vector2 positionFromFind)
        {
            var findWithTag = GameObject.FindWithTag("Player");
            var playerIsClose = (positionFromFind - (Vector2) findWithTag.transform.position).sqrMagnitude <= countToCloseForShoot;
            return playerIsClose;
        }
    }
}