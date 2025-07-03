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

//Script q relaciona elSos Combinacion de elementos y slots y lo q esta en la lista de combinaciones desbloqueables
