using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AbilitySlotUI : MonoBehaviour
{
    [SerializeField] public CombinationData combination;
    public CombinationData GetCombination()
    {
        return combination;
    }
}
