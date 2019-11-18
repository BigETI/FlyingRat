using TMPro;
using UnityEngine;

namespace FlyingRat.Controllers
{
    public class HUDControllerScript : MonoBehaviour
    {
        private static readonly string defaultScoreTextFormat = "{0} P";

        private static readonly string defaultTraveledDistanceTextFormat = "{0} m";

        [SerializeField]
        private TextMeshProUGUI scoreText = default;

        [SerializeField]
        private TextMeshProUGUI traveledDistanceText = default;

        [SerializeField]
        private string scoreTextFormat = defaultScoreTextFormat;

        [SerializeField]
        private string traveledDistanceTextFormat = defaultTraveledDistanceTextFormat;

        private uint score = default;

        private int traveledDistance = default;

        private void Update()
        {
            if (score != GameManager.Score)
            {
                score = GameManager.Score;
                if (scoreText != null)
                {
                    scoreText.text = string.Format(scoreTextFormat, score);
                }
            }
            int traveled_distance = Mathf.FloorToInt(GameManager.TraveledDistance * 10.0f);
            if (traveledDistance != traveled_distance)
            {
                traveledDistance = traveled_distance;
                if (traveledDistanceText != null)
                {
                    traveledDistanceText.text = string.Format(defaultTraveledDistanceTextFormat, (traveled_distance * 0.1f).ToString("0.0"));
                }
            }
        }
    }
}
