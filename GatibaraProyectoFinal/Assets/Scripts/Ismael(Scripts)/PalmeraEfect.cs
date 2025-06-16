using DG.Tweening;
using UnityEngine;

public class PalmeraEfect : MonoBehaviour
{
    void Start()
    {
        transform.DORotate(new Vector3(0, 0, 5f), 2f, RotateMode.LocalAxisAdd)
                 .SetLoops(-1, LoopType.Yoyo)
                 .SetEase(Ease.InOutSine);
    }
}
