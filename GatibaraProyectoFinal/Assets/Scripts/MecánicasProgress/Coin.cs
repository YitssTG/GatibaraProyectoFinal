using DG.Tweening;
using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static event Action OnCoinsCollection;
    //Vector3 AngleRotations;

    void Start()
    {
        transform.DORotate(new Vector3(0, 360, 0), 1f, RotateMode.FastBeyond360)
                 .SetEase(Ease.Linear)
                 .SetLoops(-1, LoopType.Restart);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCoinsCollection?.Invoke();
            Destroy(this.gameObject);
        }
    }
}
