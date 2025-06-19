using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private UnlockedAbilities unlockedAbilities;
    [SerializeField] private List<GameObject> slot;
    private void Start()
    {
        for (int i = 0; i < slot.Count; i++)
        {
            var ui = slot[i].GetComponent<AbilitySlotUI>();
        }
        for (int i = 0; i < slot.Count; i++)
        {
            slot[i].SetActive(false);
        }
        ShowUnlockedAbilities();
    }
    public void ShowUnlockedAbilities()
    {
        List<CombinationData> unlocked = unlockedAbilities.GetUnlockedList();
        for (int i = 0; i < slot.Count; i++)
        {
            AbilitySlotUI slotUI = slot[i].GetComponent<AbilitySlotUI>();
            if (slotUI != null && slotUI.combination != null)
            {
                string slotKey = slotUI.combination.combinationKey;
                bool found = false;
                foreach (var unlockedCombo in unlocked)
                {
                    if (slotKey == unlockedCombo.combinationKey)
                    {
                        found = true;
                        break;
                    }
                }
                slot[i].SetActive(found);
            }
            else
            {
                slot[i].SetActive(false);
            }
        }
    }
}
