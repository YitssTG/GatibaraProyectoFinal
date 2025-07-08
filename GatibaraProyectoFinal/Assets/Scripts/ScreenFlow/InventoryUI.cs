using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public static event Action OnPurchase;
    [SerializeField] private PlayerGatibara player;
    [SerializeField] private ElementVisualUI elementVisualUI;
    [SerializeField] private SelectorController selectorController;

    [SerializeField] private Slider spellNumberSlider;
    [SerializeField] private TMP_Text spellNumberText;
    [SerializeField] private TMP_Text priceCost;

    [SerializeField] private UnlockedAbilities unlockedAbilities;
    [SerializeField] private List<GameObject> slot;

    private bool tutorial;

    private void Start()
    {
        tutorial = true;
        for (int i = 0; i < slot.Count; i++)
        {
            var ui = slot[i].GetComponent<AbilitySlotUI>();
        }
        for (int i = 0; i < slot.Count; i++)
        {
            slot[i].SetActive(false);
        }
        ShowUnlockedAbilities();
        spellNumberSlider.minValue = 1;
        spellNumberSlider.maxValue = GameManager.instance.MaxSpellNumberUnlocked;
        spellNumberSlider.value = GameManager.instance.GetCurrentSpellNumber();
        UpdateText(spellNumberSlider.value);
    }
    private void Update()
    {
        priceCost.text = "Costo para la mejora: " + GameManager.instance.GetCost();
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
    public void OnUnlockSpellNumber()
    {
        if (tutorial)
        {
            OnPurchase?.Invoke();
            tutorial = false;
        }
        bool success = GameManager.instance.UnlockNewSpellNumber();
        if (success)
        {
            int newSpellNumber = GameManager.instance.MaxSpellNumberUnlocked;
            spellNumberSlider.maxValue = newSpellNumber;
            spellNumberSlider.value = newSpellNumber;
            selectorController.SetSpinSpeedForSpellNumber(newSpellNumber);
            UpdateText(spellNumberSlider.value);
            player.SetGatibaraLevel(newSpellNumber);
        }
    }
    public void OnSliderChanged(float value)
    {
        int newSpellNumber = Mathf.RoundToInt(value);
        if (newSpellNumber >= 1 && newSpellNumber <= GameManager.instance.MaxSpellNumberUnlocked)
        {
            player.SetGatibaraLevel(newSpellNumber);
            selectorController.SetSpinSpeedForSpellNumber(newSpellNumber);
            UpdateText(newSpellNumber);
        }
    }
    private void UpdateText(float newspeelnumber)
    {
        spellNumberText.text = Mathf.RoundToInt(newspeelnumber).ToString();
    }
}
