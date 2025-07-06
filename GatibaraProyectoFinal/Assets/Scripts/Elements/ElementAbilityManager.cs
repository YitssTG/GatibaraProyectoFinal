using System.Collections.Generic;
using UnityEngine;

public class EnemyDistance
{
    public EnemyFollow enemy;
    public float distance;
}
public class ElementAbilityManager : MonoBehaviour
{
    [SerializeField] private PlayerGatibara player;
    [SerializeField] private GameObject fireFirePrefab;
    [SerializeField] private GameObject fireWindPrefab;
    [SerializeField] private GameObject earthWaterPrefab;
    [SerializeField] private GameObject earthWindPrefab;
    [SerializeField] private GameObject fireEarthPrefab;
    [SerializeField] private GameObject stonePrefab;
    [SerializeField] private float fireFireSpeed;
    [SerializeField] private float fireEarthRadius;
    [SerializeField] private float fireEarthDamage;
    public void ApplyAbilityEffect(CombinationData combination)
    {
        switch (combination.combinationKey)
        {
            case "Earth+Earth":
                //this
                break;
            case "Fire+Earth"://hecho
                ApplyFireEarthAbility();
                break;
            case "Fire+Fire"://hecho
                ApplyFireFireAbility(5f);
                break;
            case "Fire+Wind"://hecho
                ApplyFireWindAbility();
                break;
            case "Earth+Water"://hecho
                ApplyEarthWaterPrefab();
                break;
            case "Water+Water":
                break;
            case "Water+Fire":
                break;
            case "Water+Earth":
                break;
            case "Wind+Fire":
                break;
            case "Earth+Wind"://hecho
                ApplyEarthWindAbility();
                break;
            case "Wind+Water":
                break;
            case "Wind+Wind":
                break;
            default:
                Debug.Log("No existe esa combinación.");
                break;
        }
    }
    public void ApplyFireFireAbility(float radius)
    {
        Collider[] hits = Physics.OverlapSphere(player.transform.position, radius, LayerMask.GetMask("Enemy"));
        List<EnemyDistance> distances = new List<EnemyDistance>();

        for (int i = 0; i < hits.Length; i++)
        {
            Collider enemyHit = hits[i];
            EnemyFollow enemy = enemyHit.GetComponent<EnemyFollow>();
            float dist = Vector3.Distance(player.transform.position, enemy.transform.position);
            EnemyDistance data = new EnemyDistance();
            data.enemy = enemy;
            data.distance = dist;
            distances.Add(data);
        }

        // Sort Selection(es directo, no se necesita crear nuevas listas, es una lista pequeña)
        for(int i = 0; i< distances.Count -1; i++)
        {
            int min = i;
            for (int j = i + 1; j < distances.Count; j++)
            {
                if (distances[j].distance < distances[min].distance)
                {
                    min = j;
                }
            }
            EnemyDistance temp = distances[min];
            distances[i] = distances[min];
            distances[min] = temp;
        }

        for (int i = 0; i < distances.Count; i++)
        {
            Vector3 direction = (distances[i].enemy.transform.position - player.transform.position).normalized;
            float distance = distances[i].distance;
            float speed = Mathf.Clamp(distance * 2f, 5f, 30f);
            GameObject bullet = Instantiate(fireFirePrefab, player.transform.position + direction, Quaternion.identity);
            bullet.GetComponent<FireFireAbility>().Launch(direction, fireFireSpeed);
        }
    }
    public void ApplyFireWindAbility()
    {
        GameObject firefog = Instantiate(fireWindPrefab, player.transform.position, Quaternion.identity);
    }
    public void ApplyEarthWaterPrefab()
    {
        Instantiate(earthWaterPrefab, player.transform.position, Quaternion.identity);
    }
    public void ApplyEarthWindAbility()
    {
        Instantiate(earthWindPrefab, player.transform.position, Quaternion.identity);
    }
    public void ApplyFireEarthAbility()
    {
        GameObject fireEarth = Instantiate(fireEarthPrefab, player.transform.position, Quaternion.identity);
        Destroy(fireEarth, 2f);
        Collider[] hits = Physics.OverlapSphere(player.transform.position, fireEarthRadius, LayerMask.GetMask("Enemy"));
        for(int i = 0; i < hits.Length; i++)
        {
            Collider hit = hits[i];
            EnemyFollow enemy = hit.GetComponent<EnemyFollow>();
            float distance = Vector3.Distance(player.transform.position, enemy.transform.position);
            float finalDamage = Mathf.Lerp(fireEarthDamage, 1f, distance/fireEarthRadius);
            enemy.RecibirAtaque(finalDamage);
        }
        for(int i = 0; i < 8; i++)
        {
            float randomX = Random.Range(-1.5f, 1.5f);
            float randomZ = Random.Range(-1.5f, 1.5f);
            Vector3 reference = new Vector3(player.transform.position.x + randomX, player.transform.position.y, player.transform.position.z + randomZ);
            GameObject rock = Instantiate(stonePrefab, reference, Quaternion.identity);
            Rigidbody rockRigidBody = rock.GetComponent<Rigidbody>();
            float dirX = Random.Range(-1f, 1f);
            float dirY = Random.Range(0.5f, 1f);
            float dirZ = Random.Range(-1f, 1f);
            Vector3 randomDirection = new Vector3(dirX, dirY, dirZ).normalized;
            float randomForce = Random.Range(9f, 15f);
            rockRigidBody.AddForce(randomDirection * randomForce, ForceMode.Impulse);
            Destroy(rock, 2f);
        }
    }
}
