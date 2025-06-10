using UnityEngine;

[CreateAssetMenu(fileName = "ElementData", menuName = "ScriptableObjects/ElementData")]
public class ElementData : ScriptableObject
{
    public string elementName;
    public string elementDescription;
    public int elementID;
    public GameObject elementPrefab;
}
