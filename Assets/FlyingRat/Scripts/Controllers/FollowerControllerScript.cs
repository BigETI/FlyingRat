using UnityEngine;

namespace FlyingRat.Controllers
{
    [ExecuteInEditMode]
    public class FollowerControllerScript : MonoBehaviour
    {
        [SerializeField]
        private Vector3 offset = default;

        [SerializeField]
        private Transform followTransform = default;

        [SerializeField]
        private EFollowContraints followConstraints = EFollowContraints.FollowAll;

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            if (followTransform != null)
            {
                Vector3 position = followTransform.position;
                transform.position = new Vector3(offset.x + (((followConstraints & EFollowContraints.FollowX) == EFollowContraints.FollowX) ? position.x : 0.0f), offset.y + (((followConstraints & EFollowContraints.FollowY) == EFollowContraints.FollowY) ? position.y : 0.0f), offset.z + (((followConstraints & EFollowContraints.FollowZ) == EFollowContraints.FollowZ) ? position.z : 0.0f));
            }
        }
    }
}
