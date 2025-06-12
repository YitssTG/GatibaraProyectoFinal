using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementData", menuName = "ScriptableObjects/ElementData")]
public class ElementData : ScriptableObject
{
    public enum ElementType
    {
        Fire,
        Water,
        Wind,
        Earth,
        Empty
    }
    public string elementName;
    public string elementDescription;
    public GameObject elementPrefab;
    public ElementType type;
}
