using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using DG.Tweening;

public class AbilityCaster : MonoBehaviour
{
    [SerializeField] private ElementManager manager;
    [SerializeField] private ElementCombination listCombinations;
    [SerializeField] private PlayerShake cameraShake;
    [SerializeField] private UnlockedAbilities unlockedAbilities;
    public static event System.Action<CombinationData> OnAbilityCasted;
    public void OnFCombinationButton(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            List<ElementData.ElementType> activeTypes = manager.GetTypes();
            CombinationData combination = listCombinations.GetCombination(activeTypes);
            if (combination != null)
            {
                Debug.Log("Castear habilidad: " + combination.abilityName);
                unlockedAbilities.UnlockCombination(combination);
                OnAbilityCasted?.Invoke(combination);
            }
            else
            {
                Debug.Log("Combinación no existente");
                OnAbilityCasted?.Invoke(null);
            }
        }
    }
}
