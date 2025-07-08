using System;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialEvents : MonoBehaviour
{
    public static event Action OnMove;
    public static event Action OnAttack;
    public static event Action OnSelectElement;
    public static event Action OnCoinCollected;
    public static event Action OnInentoryOpened;
    public static event Action OnPurchase;
    public static event Action OnLevel2;
    public static event Action OnEarthAirCombined;
    public static event Action OnAbilityUsed;
    public static event Action OnWin;
    
}
