using DG.Tweening;
using UnityEngine;

public class Health : MonoBehaviour
{
    void Start()
    {
        Vector3 escalaOriginal = transform.localScale;

        Sequence latido = DOTween.Sequence();
        latido.Append(transform.DOScale(escalaOriginal * 1.1f, 0.2f).SetEase(Ease.OutQuad));
        latido.Append(transform.DOScale(escalaOriginal, 0.4f).SetEase(Ease.InQuad));       
        latido.SetLoops(-1); 
    }
}
