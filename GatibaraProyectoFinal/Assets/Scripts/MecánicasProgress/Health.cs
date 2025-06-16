using DG.Tweening;
using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static event Action OnHealthDestroy;
    void Start()
    {
        Vector3 escalaOriginal = transform.localScale;

        Sequence latido = DOTween.Sequence();
        latido.Append(transform.DOScale(escalaOriginal * 1.1f, 0.2f).SetEase(Ease.OutQuad));
        latido.Append(transform.DOScale(escalaOriginal, 0.4f).SetEase(Ease.InQuad));       
        latido.SetLoops(-1); 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnHealthDestroy?.Invoke();
            Destroy(this.gameObject);
        }
    }
}
