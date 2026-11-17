using EFT.UI;
using System;
using tarkin.tradermod.shared;
using UnityEngine;
using UnityEngine.UI;

namespace tarkin.tradermod.eft.Bep
{
    public class TraderUIManager : IDisposable
    {
        private readonly BepInEx.Logging.ManualLogSource _logger = BepInEx.Logging.Logger.CreateLogSource(nameof(TraderUIManager));
        private static bool IsAlive(UnityEngine.Object obj) => obj != null && obj;

        GameObject traderFaceButton;
        HoverTooltipArea traderFaceTooltip;

        public event Action OnTraderFaceClick;

        DialogDataWrapper dialogData;

        public TraderUIManager(TraderScreensGroup parent, DialogDataWrapper dialogData)
        {
            this.dialogData = dialogData;

            traderFaceButton = new GameObject("TraderFaceButton", typeof(RectTransform));
            RectTransform rectTransform = traderFaceButton.GetComponent<RectTransform>();

            rectTransform.SetParent(parent.transform, false);
            rectTransform.SetAsLastSibling();

            rectTransform.anchorMin = new Vector2(0.5f, 1f);
            rectTransform.anchorMax = new Vector2(0.5f, 1f);
            rectTransform.anchoredPosition = new Vector2(0, -363f);
            rectTransform.sizeDelta = new Vector2(200, 200);

            Image image = traderFaceButton.AddComponent<Image>();
            image.color = new Color(1, 1, 1, 0);

            CanvasGroup canvasGroup = traderFaceButton.AddComponent<CanvasGroup>();

            traderFaceTooltip = traderFaceButton.AddComponent<HoverTooltipArea>();
            traderFaceTooltip.SetMessageText("Trader");

            ButtonFeedback buttonFeedback = traderFaceButton.AddComponent<ButtonFeedback>();

            ClickTrigger clickTrigger = traderFaceButton.AddComponent<ClickTrigger>();
            clickTrigger.Init((_) => OnTraderFaceClick?.Invoke());
        }

        public void SetTraderCanInteractState(TraderScene traderScene, bool canInteract)
        {
            if (traderScene == null)
            {
                traderFaceButton.SetActive(false);
                return;
            }

            // check for destroy
            if (!IsAlive(traderFaceButton))
            {
                _logger.LogWarning("TraderFaceButton was null/destroyed. Skipping SetTraderCanInteractState.");
                return;
            }

            var subtitle = dialogData?.GetLocalizedSubtitle(traderScene.ChatterPrompt);
            bool hasValidText = !string.IsNullOrEmpty(subtitle);

            bool shouldShow = hasValidText && canInteract;

            traderFaceButton.SetActive(shouldShow);

            if (shouldShow)
            {
                // check "Tooltip" for destroy too
                if (IsAlive(traderFaceTooltip))
                {
                    traderFaceTooltip.SetMessageText(subtitle);
                }
                else
                {
                    _logger.LogWarning("TraderFaceTooltip was null/destroyed while trying to set message text.");
                }
            }
        }

        public void Dispose()
        {
            _logger.LogInfo("Disposing self");

            if (traderFaceButton == null)
            {
                _logger.LogInfo("Already Dispose Complete");
                return;
            }

            if (IsAlive(traderFaceButton))
            {
                traderFaceButton.SetActive(false);
                // 不 Destroy
                traderFaceButton = null;
                traderFaceTooltip = null;
                _logger.LogInfo("Dispose (hide only) Complete");
            }

        }
    }
}
