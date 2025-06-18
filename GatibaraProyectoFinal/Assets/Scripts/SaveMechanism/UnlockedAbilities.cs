using UnityEngine;
using System.Collections.Generic;

public class UnlockedAbilities : MonoBehaviour
{
    [SerializeField] private ElementCombination allCombinations;
    private List<CombinationData> unlockedList = new List<CombinationData>();
    private SaveManager saveManager;

    private void Start()
    {
        saveManager = new SaveManager();
        LoadUnlocked();
    }
    public void UnlockCombination(CombinationData combination)
    {
        for(int i = 0; i < unlockedList.Count; i++)
        {
            if(!unlockedList[i] == combination)
            {
                unlockedList.Add(combination);
                SaveUnlocked();
                return;
            }
        }
    }
    public void SaveUnlocked()
    {
        List<string> keys = new List<string>();
        for(int i = 0; i<unlockedList.Count; i++)
        {
            keys.Add(unlockedList[i].combinationKey);
        }
        saveManager.SaveUnlockedCombinations(keys);
    }
    public void LoadUnlocked()
    {
        List<string> keys = saveManager.LoadUnlockedCombinations();
        unlockedList.Clear();
        for (int i = 0;i < keys.Count; i++)
        {
            CombinationData combination = allCombinations.GetCombinationByKey(keys[i]);
            if(combination != null)
            {
                unlockedList.Add(combination);
            }
        }
    }
    public List<CombinationData> GetUnlockedList()
    {
        return unlockedList;
    }
}
