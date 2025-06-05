using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static event Action OnCoinsCollection;
    Vector3 AngleRotations;
    private void QuaternionRotation()
    {

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
