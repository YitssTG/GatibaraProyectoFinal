using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class AbilityCaster : MonoBehaviour
{
    [SerializeField] private ElementManager manager;
    [SerializeField] private ElementCombination listCombinations;
    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            List<ElementData.ElementType> activeTypes = manager.GetTypes();
            CombinationData combination = listCombinations.GetCombination(activeTypes);
            if (combination != null)
            {
                Debug.Log("Castear habilidad: " + combination.abilityName);
            }
            else
            {
                Debug.Log("Combinación no existente");
            }
        }
    }
}
