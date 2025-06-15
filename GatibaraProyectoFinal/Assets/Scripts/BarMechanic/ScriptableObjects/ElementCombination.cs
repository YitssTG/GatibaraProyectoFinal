using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ElementCombination", menuName = "ScriptableObjects/ElementCombination")]
public class ElementCombination : ScriptableObject
{
    public List<ElementData.ElementType> combination;
    public string abilityName;
    public string description;
    public Sprite icon;
}
