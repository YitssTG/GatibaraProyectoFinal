using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Vector3 escalaOriginal;
    Sequence respiracion;

    void Start()
    {
        escalaOriginal = transform.localScale;
        respiracion = DOTween.Sequence();
        respiracion.Append(transform.DOScale(escalaOriginal * 1.05f, 1f).SetEase(Ease.InOutSine));
        respiracion.Append(transform.DOScale(escalaOriginal, 1f).SetEase(Ease.InOutSine));
        respiracion.SetLoops(-1);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        respiracion.Pause(); 
        transform.DOScale(escalaOriginal * 1.15f, 0.2f).SetEase(Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(escalaOriginal, 0.2f)
                 .SetEase(Ease.InBack)
                 .OnComplete(() => respiracion.Play());
    }
}
