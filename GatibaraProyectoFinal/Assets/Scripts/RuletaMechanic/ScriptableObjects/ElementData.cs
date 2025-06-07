using UnityEngine;

[CreateAssetMenu(fileName = "ElementData", menuName = "ScriptableObjects/ElementData")]
public class ElementData : ScriptableObject
{
    public string element;
    public string description;
    public int id;
}
