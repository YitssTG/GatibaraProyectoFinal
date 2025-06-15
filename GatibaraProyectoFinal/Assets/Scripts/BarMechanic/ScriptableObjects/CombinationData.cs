using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombinationData", menuName = "ScriptableObjects/CombinationData")]
public class CombinationData : ScriptableObject
{
    public string combinationKey;
    public string abilityName;
    public Sprite icon;
}
