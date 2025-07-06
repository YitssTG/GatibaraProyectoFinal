using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ElementEffectManager : MonoBehaviour
{
    //Maneja efectos de las habilidades
    [SerializeField] private PlayerGatibara player;
    private List<ElementType> types = new List<ElementType>();
    private List<Coroutine> activeCoroutines = new List<Coroutine>();
    private void OnEnable()
    {
        ElementManager.OnCkeck += ApplyEffects;
    }
    private void OnDisable()
    {
        ElementManager.OnCkeck -= ApplyEffects;
        StopAllCoroutines();
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
        ApplyEffectsToPlayer();
        ResetAllEnemiesDebuffs();
        RestartPassiveEffects();
    }
    public void ApplyEffectsToPlayer()
    {
        player.ResetEffect();
        int windCount = 0;
        foreach (var type in types)
        {
            if (type == ElementType.Wind)
                windCount++;
        }

        if (windCount > 0)
        {
            player.IncreaseSpeed(windCount);
        }
    }
    private void RestartPassiveEffects()
    {
        StopAllActiveCoroutines();
        foreach (var type in types)
        {
            switch (type)
            {
                case ElementType.Water:
                    activeCoroutines.Add(StartCoroutine(WaterTick(1f, 0.2f)));
                    break;
                case ElementType.Fire:
                    activeCoroutines.Add(StartCoroutine(FireTick(1f, 1)));
                    break;
                case ElementType.Earth:
                    ApplyEarthEffect(2);
                    break;
            }
        }
    }
    IEnumerator WaterTick(float time, float healAmount)
    {
        while (true)
        {
            player.Heal(healAmount);
            yield return new WaitForSeconds(time);
        }
    }
    IEnumerator FireTick(float time, int damage)
    {
        while (true)
        {
            Collider[] hits = Physics.OverlapSphere(player.transform.position, 5f, LayerMask.GetMask("Enemy"));
            foreach (var hit in hits)
            {
                var enemy = hit.GetComponent<EnemyFollow>();
                if (enemy != null)
                {
                    enemy.ApplyFireDamage(damage);
                }
            }
            yield return new WaitForSeconds(time);
        }
    }
    private void ApplyEarthEffect(int defence)
    {
        Collider[] hits = Physics.OverlapSphere(player.transform.position, 5f, LayerMask.GetMask("Enemy"));
        foreach (var hit in hits)
        {
            var enemy = hit.GetComponent<EnemyFollow>();
            if (enemy != null)
            {
                enemy.ReduceDamage(defence);
            }
        }
    }
    private void StopAllActiveCoroutines()
    {
        foreach (var coroutine in activeCoroutines)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
        }
        activeCoroutines.Clear();
    }
    private void ResetAllEnemiesDebuffs()
    {
        Collider[] hits = Physics.OverlapSphere(player.transform.position, 5f, LayerMask.GetMask("Enemy"));
        foreach (var hit in hits)
        {
            var enemy = hit.GetComponent<EnemyFollow>();
            if (enemy != null)
            {
                enemy.ResetDebuffs();
            }
        }
    }
}
