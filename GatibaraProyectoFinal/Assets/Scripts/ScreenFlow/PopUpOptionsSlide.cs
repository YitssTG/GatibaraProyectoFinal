using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PopUpOptionsSlide : MonoBehaviour
{
    public RectTransform popup;
    public float slideToX = 0f;
    public float startOffsetX = 1000f;
    public float duration = 1.2f;

    public InputActionReference togglePopupAction; // asigna desde el inspector

    public static event Action OnInventoryOpened;

    private bool inventory;

    private void Start()
    {
        inventory = false;
        HidePopup(); // Ocultar desde el comienzo
    }
    private void OnEnable()
    {
        if (togglePopupAction != null)
        {
            togglePopupAction.action.Enable();
            togglePopupAction.action.performed += OnIPressButton;
        }
    }
    private void OnDisable()
    {
        if (togglePopupAction != null)
        {
            togglePopupAction.action.performed -= OnIPressButton;
            togglePopupAction.action.Disable();
        }
    }
    public void ShowPopup()
    {
        popup.gameObject.SetActive(true);
        popup.anchoredPosition = new Vector2(startOffsetX, popup.anchoredPosition.y);
        popup.DOAnchorPosX(slideToX, duration)
             .SetEase(Ease.OutExpo)
             .SetUpdate(true);
    }
    public void HidePopup()
    {
        popup.anchoredPosition = new Vector2(slideToX, popup.anchoredPosition.y);
        popup.DOAnchorPosX(startOffsetX, 0.5f)
             .SetEase(Ease.InSine)
             .SetUpdate(true)
             .OnComplete(() => popup.gameObject.SetActive(false));
    }
    public void OnIPressButton(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnInventoryOpened?.Invoke();
            if (!inventory)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                ShowPopup();
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                HidePopup();
            }
            inventory = !inventory;
        }
    }
}