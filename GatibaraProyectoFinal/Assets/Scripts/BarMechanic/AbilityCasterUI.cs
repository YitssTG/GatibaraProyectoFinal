using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AbilityCasterUI : MonoBehaviour
{
    [SerializeField] private Image imageUI;
    [SerializeField] private PlayerShake cameraShake;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private UnlockedAbilities unlockedAbilities;
    private void OnEnable()
    {
        AbilityCaster.OnAbilityCasted += HandleAbilityCast;
    }
    private void OnDisable()
    {
        AbilityCaster.OnAbilityCasted -= HandleAbilityCast;
    }
    private void HandleAbilityCast(CombinationData combination)
    {
        RectTransform rect = imageUI.GetComponent<RectTransform>();
        rect.DOKill();
        rect.localScale = Vector3.one;

        if (combination != null)
        {
            rect.DOScale(new Vector3(1.5f, 1.5f, 1f), 0.25f).SetLoops(2, LoopType.Yoyo);
            inventoryUI.ShowUnlockedAbilities(unlockedAbilities.GetUnlockedList());
        }
        else
        {
            rect.DOScale(new Vector3(1.5f, 1.5f, 1f), 0.025f).SetLoops(2, LoopType.Yoyo);
            cameraShake.Shake();
        }
    }
}
