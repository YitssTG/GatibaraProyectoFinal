using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthWindAbility : MonoBehaviour
{
    [SerializeField] private float duration = 5f;
    [SerializeField] private float rotateSpeed = 180f;
    [SerializeField] private float damagePerSecond = 1f;
    [SerializeField] private float moveSpeed = 3f;
    private List<EnemyFollow> enemies = new List<EnemyFollow>();
    void Start()
    {
        StartCoroutine(DamageOverTime());
        Destroy(gameObject, duration);
    }
    void Update()
    {
        transform.Rotate(Vector3.down, rotateSpeed *  Time.deltaTime);
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
    private IEnumerator DamageOverTime()
    {
        while (true)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                EnemyFollow enemy = enemies[i];
                if(enemy != null)
                {
                    enemy.RecibirAtaque(damagePerSecond);
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyFollow enemy = other.GetComponent<EnemyFollow>();
            if(enemy != null)
            {
                bool itExist = false;
                for (int i = 0; i < enemies.Count; i++)
                {
                    if (enemies[i] == enemy)
                    {
                        itExist = true;
                        break;
                    }
                }
                if (!itExist)
                {
                    enemies.Add(enemy);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyFollow enemy = other.GetComponent<EnemyFollow>();
            if (enemy != null)
            {
                for (int i = 0; i < enemies.Count; i++)
                {
                    if (enemies[i] == enemy)
                    {
                        enemies.RemoveAt(i);
                        break;
                    }
                }
            }
        }
    }
}
