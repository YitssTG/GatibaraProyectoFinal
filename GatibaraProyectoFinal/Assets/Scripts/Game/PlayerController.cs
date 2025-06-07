using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Awake()
    {
        GameManager.instance.SetPlayerTransform(transform);
    }
    private void Start()
    {
    }
}
