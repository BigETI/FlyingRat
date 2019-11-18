using UnityEngine;
using UnityEngine.Events;
using UnitySceneLoaderManager;
using UnityTiming.Data;
using UnityUtils;

namespace FlyingRat.Managers
{
    public class GameManagerScript : MonoBehaviour
    {
        [SerializeField]
        private TimingData deathMenuTiming = default;

        [SerializeField]
        private Transform flyingRatTransform = default;

        [SerializeField]
        private UnityEvent onWaitingForInput = default;

        [SerializeField]
        private UnityEvent onGameStarted = default;

        [SerializeField]
        private UnityEvent onGamePaused = default;

        [SerializeField]
        private UnityEvent onGameResumed = default;

        [SerializeField]
        private UnityEvent onDeath = default;

        [SerializeField]
        private UnityEvent onShowDeathMenu = default;

        private EGameState gameState = default;

        private bool canContinueToDeathMenu = default;

        public EGameState GameState
        {
            get => gameState;
            set
            {
                if (gameState != value)
                {
                    switch (gameState)
                    {
                        case EGameState.Nothing:
                            if (value == EGameState.WaitingForInput)
                            {
                                gameState = EGameState.WaitingForInput;
                                onWaitingForInput?.Invoke();
                            }
                            break;
                        case EGameState.WaitingForInput:
                            if (value == EGameState.Playing)
                            {
                                gameState = EGameState.Playing;
                                onGameStarted?.Invoke();
                            }
                            break;
                        case EGameState.Playing:
                            switch (value)
                            {
                                case EGameState.Pausing:
                                    gameState = EGameState.Pausing;
                                    onGamePaused?.Invoke();
                                    break;
                                case EGameState.Death:
                                    gameState = EGameState.Death;
                                    onDeath?.Invoke();
                                    break;
                            }
                            break;
                        case EGameState.Pausing:
                            if (value == EGameState.Playing)
                            {
                                gameState = EGameState.Playing;
                                onGameResumed?.Invoke();
                            }
                            break;
                        case EGameState.Death:
                            if ((value == EGameState.DeathMenu) && canContinueToDeathMenu)
                            {
                                gameState = EGameState.DeathMenu;
                                onShowDeathMenu?.Invoke();
                            }
                            break;
                    }
                    Time.timeScale = ((gameState == EGameState.Playing) ? 1.0f : 0.0f);
                }
            }
        }

        public uint Score { get; set; }

        public float TraveledDistance => ((flyingRatTransform == null) ? 0.0f : (flyingRatTransform.position.x * 0.3f));

        public static GameManagerScript Instance { get; private set; }

        public void ResumeGame()
        {
            GameState = EGameState.Playing;
        }

        public void RestartGame()
        {
            SceneLoaderManager.LoadScene("GameScene");
        }

        public void ExitGame()
        {
            SceneLoaderManager.LoadScene("MainMenuScene");
        }

        private void OnEnable()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void OnDisable()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        private void Start()
        {
            GameState = EGameState.WaitingForInput;
        }

        private void Update()
        {
            if (gameState == EGameState.Death)
            {
                if (!canContinueToDeathMenu)
                {
                    if (deathMenuTiming.ProceedUpdate(false, true) > 0)
                    {
                        canContinueToDeathMenu = true;
                    }
                }
                if (Game.AnyKeyDown)
                {
                    GameState = EGameState.DeathMenu;
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                switch (gameState)
                {
                    case EGameState.Playing:
                        GameState = EGameState.Pausing;
                        break;
                    case EGameState.Pausing:
                        GameState = EGameState.Playing;
                        break;
                }
            }
        }
    }
}
