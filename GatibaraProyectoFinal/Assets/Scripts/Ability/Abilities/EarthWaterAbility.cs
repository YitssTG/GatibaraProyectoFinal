using UnityEngine;

public class EarthWaterAbility : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float slowAmount;
    void Start()
    {
        duration = 5f;
        slowAmount = 0.5f;
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
