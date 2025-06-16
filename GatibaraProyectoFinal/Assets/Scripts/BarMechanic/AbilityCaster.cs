using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using DG.Tweening;

public class AbilityCaster : MonoBehaviour
{
    [SerializeField] public Image imageUI;
    [SerializeField] private ElementManager manager;
    [SerializeField] private ElementCombination listCombinations;
    [SerializeField] private PlayerShake cameraShake;
    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            List<ElementData.ElementType> activeTypes = manager.GetTypes();
            CombinationData combination = listCombinations.GetCombination(activeTypes);
            if (combination != null)
            {
                Debug.Log("Castear habilidad: " + combination.abilityName);
                imageUI.GetComponent<RectTransform>().DOKill();
                imageUI.GetComponent<RectTransform>().localScale = Vector3.one;
                imageUI.GetComponent<RectTransform>().DOScale(new Vector3(1.5f, 1.5f, 1f), 0.25f).SetLoops(2, LoopType.Yoyo);
            }
            else
            {
                Debug.Log("Combinación no existente");
                imageUI.GetComponent<RectTransform>().DOKill();
                imageUI.GetComponent<RectTransform>().localScale = Vector3.one;
                imageUI.GetComponent<RectTransform>().DOScale(new Vector3(1.5f, 1.5f, 1f), 0.025f).SetLoops(2, LoopType.Yoyo);
                cameraShake.Shake();
            }
        }
    }
}
