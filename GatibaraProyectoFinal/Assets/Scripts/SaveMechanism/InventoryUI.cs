using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private UnlockedAbilities unlockedAbilities;
    [SerializeField] private List<GameObject> slot;
    private void OnEnable()
    {
        unlockedAbilities.OnCombinationUnlocked += OnNewCombinationUnlocked;
    }
    private void OnDisable()
    {
        unlockedAbilities.OnCombinationUnlocked -= OnNewCombinationUnlocked;
    }
    private void Start()
    {
        for (int i = 0; i < slot.Count; i++)
        {
            slot[i].SetActive(false);
        }
        List<CombinationData> unlocked = unlockedAbilities.GetUnlockedList();
        ShowUnlockedAbilities(unlocked);
    }
    private void OnNewCombinationUnlocked(CombinationData newCombination)
    {
        for(int i = 0; i < slot.Count; i++)
        {
            GameObject abilityUI = slot[i];
            AbilitySlotUI slotUI = abilityUI.GetComponent<AbilitySlotUI>();
            if(slotUI != null && slotUI.combination != null && slotUI.combination.combinationKey == newCombination.combinationKey)
            {
                abilityUI.SetActive(true);
                break;
            }
        }
    }
    public void ShowUnlockedAbilities(List<CombinationData> unlocked)
    {
        for (int i = 0; i < slot.Count; i++)
        {
            GameObject abilityUI = slot[i];
            AbilitySlotUI slotUI = slot[i].GetComponent<AbilitySlotUI>();
            if (slotUI != null && slotUI.combination != null)
            {
                string slotKey = slotUI.combination.combinationKey;
                bool found = false;
                for(int j = 0; j < unlocked.Count; j++)
                {
                    CombinationData unlockedCombination = unlocked[j];
                    if (slotKey == unlockedCombination.combinationKey)
                    {
                        found = true;
                        break;
                    }
                }
                abilityUI.SetActive(found);
            }
            else
            {
                abilityUI.SetActive(false);
            }
        }
    }
}
