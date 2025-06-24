using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using DG.Tweening;

public class AbilityUIFeedback : MonoBehaviour
{
    [SerializeField] private Image imageUI;
    [SerializeField] private InventoryUI inventoryUI;

    public void OnSuccess(CombinationData combo)
    {
        inventoryUI.ShowUnlockedAbilities();
        var rect = imageUI.GetComponent<RectTransform>();
        rect.DOKill();
        rect.localScale = Vector3.one;
        rect.DOScale(new Vector3(1.5f, 1.5f, 1f), 0.25f).SetLoops(2, LoopType.Yoyo);
    }

    public void OnFail()
    {
        var rect = imageUI.GetComponent<RectTransform>();
        rect.DOKill();
        rect.localScale = Vector3.one;
        rect.DOScale(new Vector3(1.5f, 1.5f, 1f), 0.025f).SetLoops(2, LoopType.Yoyo);
    }
}
