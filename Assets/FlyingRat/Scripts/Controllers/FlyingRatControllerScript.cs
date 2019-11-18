using UnityEngine;
using UnityUtils;

namespace FlyingRat.Controllers
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class FlyingRatControllerScript : MonoBehaviour
    {
        private static int verticalSpeedHash = Animator.StringToHash("verticalSpeed");

        private static int isAliveHash = Animator.StringToHash("isAlive");

        [SerializeField]
        private float flapForce = 30.0f;

        [SerializeField]
        [Range(0.0f, float.MaxValue)]
        private float maxUpSpeed = 30.0f;

        [SerializeField]
        [Range(0.0f, float.MaxValue)]
        private float maxDownSpeed = 20.0f;

        [SerializeField]
        [Range(0.0f, float.MaxValue)]
        private float horizontalSpeed = 5.0f;

        [SerializeField]
        private uint levelUpScore = 20U;

        [SerializeField]
        private float additionalLevelUpHorizontalSpeed = 2.5f;

        private Rigidbody2D flyingRatRigidbody = default;

        private Animator flyingRatAnimator = default;

        public float HorizontalSpeed => ((GameManager.GameState == EGameState.Death) ? ((flyingRatRigidbody == null) ? 0.0f : flyingRatRigidbody.velocity.x) : (horizontalSpeed + ((GameManager.Score / levelUpScore) * additionalLevelUpHorizontalSpeed)));

        public void Flap()
        {
            if (GameManager.GameState == EGameState.WaitingForInput)
            {
                GameManager.GameState = EGameState.Playing;
            }
            if ((GameManager.GameState == EGameState.Playing) && (GameManager.GameState != EGameState.Death) && (flyingRatRigidbody != null))
            {
                flyingRatRigidbody.velocity = Vector2.up * flapForce;
            }
        }

        private void Start()
        {
            flyingRatRigidbody = GetComponent<Rigidbody2D>();
            flyingRatAnimator = GetComponent<Animator>();
            if (flyingRatRigidbody == null)
            {
                flyingRatRigidbody = gameObject.AddComponent<Rigidbody2D>();
            }
            if (flyingRatRigidbody != null)
            {
                flyingRatRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }

        private void Update()
        {
            if (Game.AnyKeyDown)
            {
                Flap();
            }
        }

        private void FixedUpdate()
        {
            if (flyingRatRigidbody != null)
            {
                Vector2 velocity = flyingRatRigidbody.velocity;
                flyingRatRigidbody.velocity = new Vector2(HorizontalSpeed, (velocity.y > maxUpSpeed) ? maxUpSpeed : ((velocity.y < -maxDownSpeed) ? -maxDownSpeed : velocity.y));
                if (flyingRatAnimator != null)
                {
                    flyingRatAnimator.SetFloat(verticalSpeedHash, flyingRatRigidbody.velocity.y);
                    flyingRatAnimator.SetBool(isAliveHash, (GameManager.GameState != EGameState.Death));
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            GameManager.GameState = EGameState.Death;
        }
    }
}
