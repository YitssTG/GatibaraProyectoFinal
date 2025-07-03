using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public List<EnemyFollow> detectedEnemies = new List<EnemyFollow>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyFollow enemy = other.GetComponent<EnemyFollow>();
            if (enemy != null && !detectedEnemies.Contains(enemy))
            {
                detectedEnemies.Add(enemy);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyFollow enemy = other.GetComponent<EnemyFollow>();
            if(enemy != null)
            {
                detectedEnemies.Remove(enemy);
            }
        }
    }
    public List<EnemyFollow> GetEnemies()
    {
        return detectedEnemies;
    }
}
