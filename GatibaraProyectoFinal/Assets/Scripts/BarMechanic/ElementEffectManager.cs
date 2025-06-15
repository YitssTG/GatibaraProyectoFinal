using UnityEngine;
using System.Collections.Generic;
using System.Threading;

public class ElementEffectManager : MonoBehaviour
{
    [SerializeField] private PlayerGatibara player;
    [SerializeField] private List<Enemy> enemies;

    private void OnEnable()
    {
        ElementManager.OnCkeck += ApplyEffects;
    }
    private void OnDisable()
    {
        ElementManager.OnCkeck -= ApplyEffects;
    }
    private void ApplyEffects(CustomSimpleLinkedList<ElementData> elements, ElementData elementData)
    {
        List<ElementData> ordered = elements.GetOrderedElements();
        List<ElementData.ElementType> types = new List<ElementData.ElementType>();
        int limit = Mathf.Min(player.spellnumber, ordered.Count);
        for(int i = 0; i < limit; i++)
        {
            types.Add(ordered[i].type);
        }
        ApplyActiveElements(types);
        //acá llamo para actualizar
        Debug.Log(enemies[0].fireDamage);
        Debug.Log(enemies[0].attackSpeedPenalty);
        Debug.Log(enemies[0].speedMovementPenalty);
        Debug.Log(player.currentAttackSpeed);
    }
    public void ApplyActiveElements(List <ElementData.ElementType> activeElements)
    {
        player.ResetEffect();
        int fireCount = 0;
        int waterCount = 0;
        int windCount = 0;
        int earthCount = 0;
        for(int i=0; i<activeElements.Count; i++)
        {
            switch (activeElements[i])
            {
                case ElementData.ElementType.Fire:
                    fireCount++;
                    break;
                case ElementData.ElementType.Water:
                    waterCount++;
                    break;
                case ElementData.ElementType.Wind:
                    windCount++;
                    break;
                case ElementData.ElementType.Earth:
                    earthCount++;
                    break;
            }
        }
        for(int i=0; i< enemies.Count; i++)
        {
            enemies[i].ResetDebuffs();
            if(fireCount > 0)
            {
                enemies[i].ApplyFireDamage(fireCount);
            }
            if (waterCount > 0)
            {
                enemies[i].ReduceAttackSpeed(waterCount);
            }
            if (earthCount > 0)
            {
                enemies[i].ReduceMovementSpeed(earthCount);
            }
        }
        if(windCount > 0)
        {
            player.IncreaseAttackSpeed(windCount);
        }
    }
}
