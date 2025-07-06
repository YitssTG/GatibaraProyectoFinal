using UnityEngine;

public class FireWindAbility : MonoBehaviour
{
    [SerializeField] private float maxScale = 5f;
    [SerializeField] private float growSpeed = 2f;
    [SerializeField] private float damage;
    private Vector3 targetScale;
    private bool isGrowing;
    void Start()
    {
        isGrowing = true;
        targetScale = new Vector3(maxScale, 1f, maxScale);
    }
    void Update()
    {
        if (isGrowing)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, growSpeed * Time.deltaTime);
            if (transform.localScale == targetScale)
            {
                isGrowing = false;
                Destroy(gameObject, 0.2F);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyFollow enemy = other.GetComponent<EnemyFollow>();
            enemy.RecibirAtaque(damage);
        }
    }
}
