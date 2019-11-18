using UnityEngine;

namespace FlyingRat.Controllers
{
    public class PipesSpawnerControllerScript : MonoBehaviour
    {
        [SerializeField]
        private GameObject pipesAsset = default;

        [SerializeField]
        private float pipesLifetime = 10.0f;

        public void Spawn()
        {
            if (pipesAsset != null)
            {
                GameObject go = Instantiate(pipesAsset, transform.position, Quaternion.identity);
                if (go != null)
                {
                    PipesControllerScript pipes_controller = go.GetComponent<PipesControllerScript>();
                    if (pipes_controller != null)
                    {
                        go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y + (pipes_controller.ScreenHeight * Random.Range(-0.5f, 0.5f)), go.transform.position.z);
                        Destroy(go, pipesLifetime);
                    }
                    else
                    {
                        Destroy(go);
                    }
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, Vector3.one);
        }
    }
}
