using FlyingRat.Managers;

namespace FlyingRat
{
    public static class GameManager
    {
        public static EGameState GameState
        {
            get => ((GameManagerScript.Instance == null) ? EGameState.Nothing : GameManagerScript.Instance.GameState);
            set
            {
                if (GameManagerScript.Instance != null)
                {
                    GameManagerScript.Instance.GameState = value;
                }
            }
        }

        public static uint Score
        {
            get => ((GameManagerScript.Instance == null) ? 0U : GameManagerScript.Instance.Score);
            set
            {
                if (GameManagerScript.Instance != null)
                {
                    GameManagerScript.Instance.Score = value;
                }
            }
        }

        public static float TraveledDistance => ((GameManagerScript.Instance == null) ? 0.0f : GameManagerScript.Instance.TraveledDistance);
    }
}
