using UnityEngine;

public class EarthWaterAbility : MonoBehaviour
{
    [SerializeField] private float duration = 8f;
    [SerializeField] private float slowAmount = 0.5f;
    void Start()
    {
        Destroy(gameObject, duration);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyFollow enemy = other.GetComponent<EnemyFollow>();
            enemy.SpeedModify(slowAmount);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyFollow enemy = other.GetComponent<EnemyFollow>();
            enemy.SpeedRestore();
        }
    }
}
