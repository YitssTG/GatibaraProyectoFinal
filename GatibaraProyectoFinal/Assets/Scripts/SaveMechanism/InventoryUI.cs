using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using static UnityEngine.Rendering.DebugUI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private PlayerGatibara player;
    [SerializeField] private ElementVisualUI elementVisualUI;

    [SerializeField] private Slider spellNumberSlider;
    [SerializeField] private TMP_Text spellNumberText;
    [SerializeField] private TMP_Text priceCost;

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
        bool success = GameManager.instance.UnlockNewSpellNumber();
        if (success)
        {
            spellNumberSlider.maxValue = GameManager.instance.MaxSpellNumberUnlocked;
            spellNumberSlider.value = GameManager.instance.MaxSpellNumberUnlocked;
            elementVisualUI.SetSelectDurationForSpellNumber(GameManager.instance.MaxSpellNumberUnlocked);
            UpdateText(spellNumberSlider.value);
            player.SetGatibaraLevel(GameManager.instance.MaxSpellNumberUnlocked);
        }
    }
    public void OnSliderChanged(float value)
    {
        int newspeelnumber = Mathf.RoundToInt(value);
        if(newspeelnumber >= 1 && newspeelnumber < (GameManager.instance.MaxSpellNumberUnlocked + 1))
        {
            player.SetGatibaraLevel(newspeelnumber);
            elementVisualUI.SetSelectDurationForSpellNumber(newspeelnumber);
            UpdateText(newspeelnumber);
        }
    }
    private void UpdateText(float newspeelnumber)
    {
        spellNumberText.text = Mathf.RoundToInt(newspeelnumber).ToString();
    }
}
