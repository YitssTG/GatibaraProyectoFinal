using UnityEngine;
using DG.Tweening;

public class EnemyPatron : MonoBehaviour
{
    void Start()
    {
        transform.DOMoveX(transform.position.x + 2f, 1.5f)
                 .SetLoops(-1, LoopType.Yoyo)
                 .SetEase(Ease.InOutSine);
    }
}
