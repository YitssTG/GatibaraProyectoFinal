using UnityEngine;

public class GenerateEnemy : MonoBehaviour
{
    [Header("Gizmos")]
    [SerializeField] private Color color;
    [SerializeField] private float radius;
    private void Start()
    {
        GameManager.instance.AddpointGenerateEnemy(this);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
