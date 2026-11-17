using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace tarkin.tradermod.shared
{
    public class SubtitlesUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private CanvasGroup canvasGroup;
        //[SerializeField] private RectTransform rectTransform;

        public void SetSize(float width, float height)
        {
            //var layoutElement = GetComponent<LayoutElement>();
            //if (layoutElement != null)
            //{
            //    layoutElement.preferredWidth = width;
            //    layoutElement.preferredHeight = height;
            //}
            //rectTransform.sizeDelta = new Vector2(width, height);
        }

        public void SetText(string subtitle)
        {
            if (string.IsNullOrEmpty(subtitle))
            {
                canvasGroup.alpha = 0f;
                return;
            }

            SetSize(100, 100);
            canvasGroup.alpha = 1f;
            text.text = subtitle;
        }
    }
}
