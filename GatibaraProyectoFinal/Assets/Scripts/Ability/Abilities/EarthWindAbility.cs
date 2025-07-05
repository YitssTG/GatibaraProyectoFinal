using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class EarthWindAbility : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float damagePerSecond;
    [SerializeField] private float moveSpeed;
    private List<EnemyFollow> enemies = new List<EnemyFollow>();
    void Start()
    {
        duration = 5f;
        rotateSpeed = 180f;
        StartCoroutine(DamageOverTime());
        Destroy(gameObject, duration);
        moveSpeed = 3f;
    }
    void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed *  Time.deltaTime);
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
    private IEnumerator DamageOverTime()
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            EnemyFollow enemy = enemies[i];
            enemy.RecibirAtaque(1f);
        }
        yield return new WaitForSeconds(1f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyFollow enemy = other.GetComponent<EnemyFollow>();
            bool itExist = false;
            for(int i = 0; i < enemies.Count; i++)
            {
                if(enemies[i] == enemy)
                {
                    itExist = true;
                    break;
                }
            }
            if (itExist)
            {
                enemies.Add(enemy);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyFollow enemy = other.GetComponent<EnemyFollow>();
            bool itExist = false;
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == enemy)
                {
                    itExist = true;
                    break;
                }
            }
            if (itExist)
            {
                enemies.Remove(enemy);
            }
        }
    }
}
