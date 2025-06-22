using DG.Tweening;
using UnityEngine;

public class TituloAnim : MonoBehaviour
{
    void Start()
    {
        transform.localScale = new Vector3(0, 0, 0);
        transform.DOScale(Vector3.one, 1f)
            .SetEase(Ease.OutBack);
    }
}
