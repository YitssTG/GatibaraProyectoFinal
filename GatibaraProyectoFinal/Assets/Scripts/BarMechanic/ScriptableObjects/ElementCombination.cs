using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ElementCombination", menuName = "ScriptableObjects/ElementCombination")]
public class ElementCombination : ScriptableObject
{
    public List<CombinationData> listCombinations;
    public Dictionary<string, CombinationData> searcher;
    private void OnEnable()
    {
        Initialize();
    }
    public void Initialize()
    {
        searcher = new Dictionary<string, CombinationData>();
        foreach(var combination in listCombinations)
        {
            if (!searcher.ContainsKey(combination.combinationKey))
            {
                searcher[combination.combinationKey] = combination;
            }
        }
    }
    public CombinationData GetCombination(List<ElementData.ElementType> elements)
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
        if(searcher.TryGetValue(key, out var combination))
        {
            return combination;
        }
        return null;
    }
}
