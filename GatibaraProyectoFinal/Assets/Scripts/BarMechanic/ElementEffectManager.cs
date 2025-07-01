using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ElementEffectManager : MonoBehaviour
{
    [SerializeField] private PlayerGatibara player;
    [SerializeField] private EnemyDetector detector;
    private Coroutine healthCoroutine;
    private List<ElementType> types;

    private void Start()
    {
        types = new List<ElementType>();
    }
    private void OnEnable()
    {
        ElementManager.OnCkeck += ApplyEffects;
        healthCoroutine = StartCoroutine(HealthTick());
    }
    private void OnDisable()
    {
        ElementManager.OnCkeck -= ApplyEffects;
        //
    }
    IEnumerator HealthTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            ApplyTickEffect();
        }
    }
    private void ApplyTickEffect()
    {
        for(int i = 0; i < types.Count; i++)
        {
            //if(types[i] == ElementData.ElementType.Fire)
            //{
            //    for(int j = 0; j < enemies.Count; j++)
            //    {
            //        enemies[j].RecibirFuego(1);
            //    }
            //}
            if(types[i] == ElementType.Water)
            {
                player.Heal(1);
            }
        }
    }
    private void ApplyEffects(CustomSimpleLinkedList<ElementData> elements, ElementData elementData)
    {
        List<ElementData> ordered = elements.GetOrderedElements();

        types.Clear();
        int limit = Mathf.Min(player.spellNumber, ordered.Count);
        for(int i = 0;i < limit; i++)
        {
            types.Add(ordered[i].type);
        }
        ApplyEffectsToPlayer(types);
    }
    public void ApplyEffectsToPlayer(List<ElementType> activeElements)
    {
        player.ResetEffect();
        int windCount = 0;
        for (int i = 0; i < activeElements.Count; i++)
        {
            if (activeElements[i] == ElementType.Wind)
            {
                windCount++;
            }
        }
        if (windCount > 0)
        {
            player.IncreaseSpeed(windCount);
        }
    }
    public void ApplyEffectsToEnemy(EnemyFollow enemy, List<ElementType> type)
    {
        enemy.ResetDebuffs();
        enemy.StopFireEffect();
        int fireCount = 0;
        int earthCount = 0;
        for (int i = 0; i < type.Count; i++)
        {
            switch (type[i])
            {
                case ElementType.Fire:
                    fireCount++;
                    break;
                case ElementType.Earth:
                    earthCount++;
                    break;
            }
        }
        if (fireCount > 0)
        {
            enemy.ApplyFireDamage(fireCount);
        }
        if(earthCount > 0)
        {
            enemy.ReduceDamage(earthCount);
        }
    }
    public void ApplyCombinationEffect(CombinationData combination)
    {
        switch (combination.combinationKey)
        {
            case "Fire+Fire":
                ApplyFireFireEffect(detector);
                break;
            //case "Water":
            //    ApplyWaterEffect();
            //    break;
            //case "Earth":
            //    ApplyEarthEffect();
            //    break;
            //case "Wind":
            //    ApplyWindEffect();
            //    break;
            default:
                Debug.Log("No hay efecto.");
                break;
        }
    }
    public void ApplyFireFireEffect(EnemyDetector detector)
    {
        Debug.Log("Habilidad carmesí casteada");
        var enemies = new List<EnemyFollow>(detector.GetEnemies());
        if(enemies.Count > 0)
        {
            return;
        }
        SelectionSortDistance(enemies);
        EnemyFollow closest = enemies[0];
        closest.RecibirAtaque();
    }
    public void SelectionSortDistance(List<EnemyFollow> enemies)
    {
        Vector3 origin = transform.position;
        for (int i = 0; i < enemies.Count - 1; i++)
        {
            int minIndex = i;
            float minDistance = Vector3.Distance(origin, enemies[i].transform.position);

            for (int j = i + 1; j < enemies.Count; j++)
            {
                float distance = Vector3.Distance(origin, enemies[j].transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    minIndex = j;
                }
            }
            var temp = enemies[i];
            enemies[i] = enemies[minIndex];
            enemies[minIndex] = temp;
        }
    }
}
