using System.Collections.Generic;
using UnityEngine;

namespace FlyingRat.Controllers
{
    public class PipesSpawnerControllerScript : MonoBehaviour
    {
        [SerializeField]
        private GameObject pipesAsset = default;

        [SerializeField]
        private float destroyPipesAtDistance = 50.0f;

        private List<Transform> pipeTransforms = new List<Transform>();

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
                        pipeTransforms.Add(go.transform);
                    }
                    else
                    {
                        Destroy(go);
                    }
                }
            }
        }

        private void Update()
        {
            List<int> destroy_pipes_indicies = null;
            for (int i = pipeTransforms.Count - 1; i >= 0; i--)
            {
                if ((pipeTransforms[i].position.x + destroyPipesAtDistance) < transform.position.x)
                {
                    if (destroy_pipes_indicies == null)
                    {
                        destroy_pipes_indicies = new List<int>();
                    }
                    destroy_pipes_indicies.Add(i);
                }
            }
            if (destroy_pipes_indicies != null)
            {
                foreach (int destroy_pipes_index in destroy_pipes_indicies)
                {
                    Destroy(pipeTransforms[destroy_pipes_index].gameObject);
                    pipeTransforms.RemoveAt(destroy_pipes_index);
                }
                destroy_pipes_indicies.Clear();
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, Vector3.one);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(new Vector3(transform.position.x - destroyPipesAtDistance, transform.position.y, transform.position.z), Vector3.one);
        }
    }
}
