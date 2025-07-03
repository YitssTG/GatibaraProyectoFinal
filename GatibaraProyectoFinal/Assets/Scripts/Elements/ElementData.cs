using System;
using TMPro;
using UnityEngine;

public enum ElementType
{
    Empty,
    Fire,  
    Water, 
    Wind, 
    Earth, 
}

[CreateAssetMenu(fileName = "ElementData", menuName = "ScriptableObjects/ElementData")]
public class ElementData : ScriptableObject
{
    
    public string elementName;
    public string elementDescription;
    public GameObject elementPrefab;
    public ElementType type;
}
//guarda los tipos de elementos