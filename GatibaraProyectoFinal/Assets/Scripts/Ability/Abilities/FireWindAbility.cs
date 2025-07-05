using UnityEngine;

public class FireWindAbility : MonoBehaviour
{
    [SerializeField] private float maxScale;
    [SerializeField] private float growSpeed;
    [SerializeField] private float damage;
    private Vector3 targetScale;
    private bool isGrowing;
    void Start()
    {
        maxScale = 5f;
        growSpeed = 2f;
        isGrowing = true;
        transform.position = Vector3.zero;
        targetScale = Vector3.one * maxScale;
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
            enemy.RecibirAtaque(5f);
        }
    }
}
