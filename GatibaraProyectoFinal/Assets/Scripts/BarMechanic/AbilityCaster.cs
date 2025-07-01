using UnityEngine;
using System.Collections.Generic;

public class AbilityCaster : MonoBehaviour
{
    [SerializeField] private ElementCombination Listcombinations;
    [SerializeField] private UnlockedAbilities unlockedAbilities;

    private List<AbilityCooldown> activeCooldowns = new List<AbilityCooldown>();
    private void Update()
    {
        UpdateCooldowns();
    }
    public CombinationData CastAbility(List<ElementType> types)
    {
        var combination = Listcombinations.GetCombination(types);
        if (combination != null)
        {
            unlockedAbilities.UnlockCombination(combination);

            return combination;
        }
        else
        {
            Debug.Log("habilidad en cooldown o no válido.");
        }
            return null;
    }
    public void StartCooldown(CombinationData combination)
    {
        activeCooldowns.Add(new AbilityCooldown(combination, combination.cooldownBase));
    }
    private void UpdateCooldowns()
    {
        for(int i = activeCooldowns.Count - 1;  i >= 0; i--)
        {
            activeCooldowns[i].UpdateCooldown(Time.deltaTime);
            if (activeCooldowns[i].IsReady())
            {
                activeCooldowns.RemoveAt(i);
            }
        }
    }
    public bool IsOnCooldown(CombinationData combination)
    {
        foreach(var cooldown in activeCooldowns)
        {
            if(cooldown.data == combination)
            {
                return true;
            }
        }
        return false;
    }
    public AbilityCooldown GetAvailableAbility()
    {
        AbilityCooldown next = null;
        float minCooldown = float.MaxValue;
        foreach(var cooldown in activeCooldowns)
        {
            if(cooldown.cooldownRemaining >0 && cooldown.cooldownRemaining < minCooldown)
            {
                minCooldown = cooldown.cooldownRemaining;
                next = cooldown;
            }
        }
        return next;
    }
}