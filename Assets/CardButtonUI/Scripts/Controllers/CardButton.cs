using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Card button UI controllers namespace
/// </summary>
namespace CardButtonUI.Controllers
{
    /// <summary>
    /// Card button class
    /// </summary>
    [RequireComponent(typeof(Button))]
    [ExecuteInEditMode]
    public class CardButton : MonoBehaviour
    {
        /// <summary>
        /// Revealing
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float revealing = default;

        /// <summary>
        /// Closed offset
        /// </summary>
        [SerializeField]
        private float closedOffset = 8.0f;

        /// <summary>
        /// Open offset
        /// </summary>
        [SerializeField]
        private float openOffset = 8.0f;

        /// <summary>
        /// Direction
        /// </summary>
        [SerializeField]
        private ECardButtonDirection direction = ECardButtonDirection.Right;

        /// <summary>
        /// Car panel rectangle transform
        /// </summary>
        [SerializeField]
        private RectTransform cardPanelRectTransform = default;

        /// <summary>
        /// Rectangle transform
        /// </summary>
        private RectTransform rectTransform = default;

        /// <summary>
        /// Revealing
        /// </summary>
        public float Revealing
        {
            get => Mathf.Clamp(revealing, 0.0f, 1.0f);
            set => revealing = Mathf.Clamp(value, 0.0f, 1.0f);
        }

        /// <summary>
        /// Direction
        /// </summary>
        public ECardButtonDirection Direction
        {
            get => direction;
            set => direction = value;
        }

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            if ((rectTransform != null) && (cardPanelRectTransform != null))
            {
                Rect rect = rectTransform.rect;
                Vector2 size = rect.size;
                cardPanelRectTransform.anchorMin = Vector2.zero;
                cardPanelRectTransform.anchorMax = Vector2.zero;
                cardPanelRectTransform.pivot = Vector2.zero;
                switch (direction)
                {
                    case ECardButtonDirection.Up:
                        size = new Vector2(size.x, size.y - closedOffset);
                        cardPanelRectTransform.anchoredPosition = new Vector2(0.0f, ((size.y - openOffset) * Revealing) + closedOffset);
                        break;
                    case ECardButtonDirection.Down:
                        size = new Vector2(size.x, size.y - closedOffset);
                        cardPanelRectTransform.anchoredPosition = new Vector2(0.0f, (size.y - openOffset) * -Revealing);
                        break;
                    case ECardButtonDirection.Left:
                        size = new Vector2(size.x - closedOffset, size.y);
                        cardPanelRectTransform.anchoredPosition = new Vector2((size.x - openOffset) * -Revealing, 0.0f);
                        break;
                    case ECardButtonDirection.Right:
                        size = new Vector2(size.x - closedOffset, size.y);
                        cardPanelRectTransform.anchoredPosition = new Vector2(((size.x - openOffset) * Revealing) + closedOffset, 0.0f);
                        break;
                }
                cardPanelRectTransform.sizeDelta = size;
            }
        }
    }
}
