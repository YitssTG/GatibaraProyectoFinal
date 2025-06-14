using DG.Tweening;
using UnityEngine;

public class SetaEfect : MonoBehaviour
{
    void Start()
    {

        Vector3 escalaOriginal = transform.localScale;

        transform.DOScale(escalaOriginal * 1.05f, 1.5f)
                 .SetLoops(-1, LoopType.Yoyo)
                 .SetEase(Ease.InOutSine);
    }
}
