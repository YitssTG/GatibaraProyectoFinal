using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "ElementCombination", menuName = "ScriptableObjects/ElementCombination")]
public class ElementCombination : ScriptableObject
{
    public List<CombinationData> listCombinations;
    public Dictionary<string, CombinationData> searcher;
    private void OnEnable()
    {
        Initialize();
    }
    //Almacena combinaciones
    public void Initialize()
    {
        searcher = new Dictionary<string, CombinationData>();
        foreach(var combination in listCombinations)
        {
            if (!searcher.ContainsKey(combination.combinationKey))
            {
                searcher[combination.combinationKey] = combination;
            }
            else
            {
                Debug.LogWarning("Clave duplicada: " + combination.combinationKey);
            }
        }
    }
    public CombinationData GetCombination(List<ElementType> elements)
    {
        string key = "";
        for(int i = 0; i< elements.Count; i++)
        {
            key += elements[i].ToString();
            if (i < elements.Count - 1)
            {
                key += "+";
            }
        }
        if(searcher == null ||searcher.Count == 0)
        {
            Initialize();
        }
        if(searcher.TryGetValue(key, out var combination))
        {
            Debug.Log("combinación: " + key);
            return combination;
        }
        return null;
    }
    public CombinationData GetCombinationByKey(string key)
    {
        if(searcher == null || searcher.Count == 0)
        {
            Initialize();
        }
        if(searcher.TryGetValue(key, out var combination))
        {
            return combination;
        }
        return null;
    }
}
//