using System.Collections.Generic;
using UnityEngine;

namespace FlyingRat.Controllers
{
    [RequireComponent(typeof(Animator))]
    [ExecuteInEditMode]
    public class PipesControllerScript : MonoBehaviour
    {
        [SerializeField]
        [Range(0.0f, 100.0f)]
        private float pipesDistance = 8.0f;

        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float screenHeight = 10.0f;

        [SerializeField]
        private uint movingPipesScore = 50U;

        [SerializeField]
        private uint extendingRetractingPipesScore = 100U;

        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float retract = 1.0f;

        [SerializeField]
        [Range(-1.0f, 1.0f)]
        private float center = default;

        [SerializeField]
        private Transform topPipeTransform = default;

        [SerializeField]
        private Transform bottomPipeTransform = default;

        private static readonly int moveHash = Animator.StringToHash("Move");

        private static readonly int extendRetractHash = Animator.StringToHash("ExtendRetract");

        public float ScreenHeight => Mathf.Max(screenHeight, 0.0f);

        public float Retract
        {
            get => Mathf.Clamp(retract, 0.0f, 1.0f);
            set => retract = Mathf.Clamp(value, 0.0f, 1.0f);
        }

        public float Center
        {
            get => Mathf.Clamp(center, -1.0f, 1.0f);
            set => center = Mathf.Clamp(value, -1.0f, 1.0f);
        }

        private void Start()
        {
            Animator pipes_animator = GetComponent<Animator>();
            if (pipes_animator != null)
            {
                List<int> animations = new List<int>();
                if (GameManager.Score >= movingPipesScore)
                {
                    animations.Add(moveHash);
                }
                if (GameManager.Score >= extendingRetractingPipesScore)
                {
                    animations.Add(extendRetractHash);
                }
                if (animations.Count > 0)
                {
                    pipes_animator.Play(animations[Random.Range(0, animations.Count)]);
                    animations.Clear();
                }
            }
        }

        private void Update()
        {
            float retraction = (pipesDistance * Mathf.Clamp(Retract, 0.0f, 1.0f));
            float offset = (Center * screenHeight);
            if (topPipeTransform != null)
            {
                topPipeTransform.localPosition = new Vector3(0.0f, (retraction * 0.5f) + offset, 0.0f);
            }
            if (bottomPipeTransform != null)
            {
                bottomPipeTransform.localPosition = new Vector3(0.0f, (retraction * -0.5f) + offset, 0.0f);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<FlyingRatControllerScript>() != null)
            {
                ++GameManager.Score;
            }
        }
    }
}
