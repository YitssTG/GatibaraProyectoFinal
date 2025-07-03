using UnityEngine;
using DG.Tweening;

public class PopUpOptionsDrop : MonoBehaviour
{
    public RectTransform popup;
    public float dropToY = 0f;
    public float startOffsetY = 800f;
    public float duration = 1.2f;

    public void ShowPopup()
    {
        popup.gameObject.SetActive(true);

        popup.anchoredPosition = new Vector2(popup.anchoredPosition.x, startOffsetY);
        popup.DOAnchorPosY(dropToY, duration).SetEase(Ease.OutBounce).SetUpdate(true);

    }
    public void HidePopup()
    {
        popup.DOAnchorPosY(startOffsetY, 0.5f).SetEase(Ease.InBack).SetUpdate(true).OnComplete(() => popup.gameObject.SetActive(false));
    }
}
