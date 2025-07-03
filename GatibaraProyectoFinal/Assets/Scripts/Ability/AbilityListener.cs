using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using DG.Tweening;

public class AbilityListener : MonoBehaviour
{
    [SerializeField] private AbilityCaster caster;
    [SerializeField] private ElementManager manager;
    [SerializeField] private PlayerShake cameraShake;
    [SerializeField] private AbilityUIFeedback uiFeedback;

    public void OnCast(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var types = manager.GetTypes();
            var result = caster.CastAbility(types);
            if (result != null)
            {
                uiFeedback.OnSuccess(result);
            }
            else
            {
                uiFeedback.OnFail();
            }
        }
    }
}
//F combina habilidades