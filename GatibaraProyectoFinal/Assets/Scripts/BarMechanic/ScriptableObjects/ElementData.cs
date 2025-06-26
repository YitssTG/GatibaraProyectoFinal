using System;
using TMPro;
using UnityEngine;



[System.Flags]
public enum ElementType
{
    None = 0,
    Fire = 1 << 0,  // 1
    Water = 1 << 1,  // 2
    Wind = 1 << 2,  // 4
    Earth = 1 << 3,  // 8
    Empty = 1 << 4   // 16 (opcional si necesitas un valor especial)
}

[CreateAssetMenu(fileName = "ElementData", menuName = "ScriptableObjects/ElementData")]
public class ElementData : ScriptableObject
{
    
    public string elementName;
    public string elementDescription;
    public GameObject elementPrefab;
    public ElementType type;
}
