using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private ElementCombination allCombinations;
    [SerializeField] private UnlockedAbilities unlockedAbilities;
    [SerializeField] private List<GameObject> slot;
    private void InitializeSlots()
    {
        for(int i = 0; i < slot.Count; i++)
        {
            slot[i] = unlockedAbilities.GetSlot(i);
        }
    }
}
