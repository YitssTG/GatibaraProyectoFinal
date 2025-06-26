using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SelectorController : MonoBehaviour
{
    [SerializeField] private ElementVisualUI elementUI;
    [SerializeField] private ElementManager manager;
    public GameObject ElementBarPanel;
    private bool isSpinning;
    public bool isPaused;
    void Start()
    {
        isSpinning = false;
        ElementBarPanel.SetActive(false);
        isPaused = false;
    }
    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed && !isPaused)
        {
            ElementBarPanel.SetActive(true);
            if (!isSpinning)
            {
                elementUI.StartSpinning();
                isSpinning = true;
            }
            else
            {
                elementUI.StopSpinning();
                isSpinning = false;
                ElementBarPanel.SetActive(false);
                ElementType selected = elementUI.GetCurrentElementType();
                manager.ApplyElement(selected);
            }
        }
    }
}