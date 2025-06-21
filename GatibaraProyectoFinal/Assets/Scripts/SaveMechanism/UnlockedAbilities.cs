using UnityEngine;
using System.Collections.Generic;

public class UnlockedAbilities : MonoBehaviour
{
    [SerializeField] private ElementCombination allCombinations;
    private List<CombinationData> currentUnlockedList = new List<CombinationData>();
    private List<CombinationData> savedUnlockedList = new List<CombinationData>();
    private SaveManager saveManager;

    private void Awake()
    {
        saveManager = new SaveManager();
    }
    private void Start()
    {
        LoadSaved();
    }
    public void UnlockCombination(CombinationData combination)
    {
        bool Unlocked = false;
        for (int i = 0; i < currentUnlockedList.Count; i++)
        {
            if (currentUnlockedList[i].combinationKey == combination.combinationKey)
            {
                Unlocked = true;
                break;
            }
        }
        if (Unlocked)
        {
            return;
        }
        currentUnlockedList.Add(combination);
        Debug.Log("Combinación desbloqueada: " + combination.abilityName);
    }
    public void SaveProgress()
    {
        for (int i = 0; i < currentUnlockedList.Count; i++)
        {
            bool unlocked = false;
            for (int j = 0; j < savedUnlockedList.Count; j++)
            {
                if (savedUnlockedList[j].combinationKey == currentUnlockedList[i].combinationKey)
                {
                    unlocked = true;
                    break;
                }
                if (!unlocked)
                {
                    savedUnlockedList.Add(currentUnlockedList[i]);
                }
            }
        }
        List<string> keysToSave = new List<string>();
        for (int i = 0; i < savedUnlockedList.Count; i++)
        {
            keysToSave.Add(savedUnlockedList[i].combinationKey);
        }
        saveManager.SaveUnlockedCombinations(keysToSave);
    }
    public void LoadSaved()
    {
        savedUnlockedList.Clear();
        List<string> keys = saveManager.LoadUnlockedCombinations();
        for (int i = 0; i < keys.Count; i++)
        {
            string key = keys[i];
            CombinationData combination = allCombinations.GetCombinationByKey(keys[i]);
            if (combination != null)
            {
                savedUnlockedList.Add(combination);
                currentUnlockedList.Add(combination);
            }
        }
    }
    public List<CombinationData> GetUnlockedList()
    {
        return currentUnlockedList;
    }
}