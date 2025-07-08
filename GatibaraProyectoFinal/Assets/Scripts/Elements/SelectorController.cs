using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor.GettingStarted;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectorController : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private List<ElementType> elements;
    [SerializeField] private ElementVisualUI elementUI;
    [SerializeField] private ElementManager manager;
    [SerializeField] private float spinSpeed = 0.5f;

    [Header("UI Panel")]
    public GameObject ElementBarPanel;

    public static event Action OnSelectElement;

    private int currentIndex = 0;
    private Coroutine spinRoutine;
    private bool isSpinning = false;
    public bool isPaused = false;
    private bool tutorial;
    private void Start()
    {
        tutorial = true;
        ElementBarPanel.SetActive(false);
    }
    public void OnSpace(InputAction.CallbackContext context)
    {
        if (context.performed && !isPaused)
        {
            if (tutorial)
            {
                OnSelectElement?.Invoke();
                tutorial = false;
            }
            if (!isSpinning)
            {
                ElementBarPanel.SetActive(true);
                spinRoutine = StartCoroutine(SpinCycle());
                isSpinning = true;
            }
            else
            {
                StopCoroutine(spinRoutine);
                isSpinning = false;
                ElementBarPanel.SetActive(false);
                ElementType selected = elements[currentIndex];
                manager.ApplyElement(selected);
            }
        }
    }
    private IEnumerator SpinCycle()
    {
        while (true)
        {
            CycleElement();
            yield return new WaitForSeconds(spinSpeed);
        }
    }
    private void CycleElement()
    {
        currentIndex = (currentIndex + 1) % elements.Count;
        elementUI.ShowElement(elements[currentIndex]);
    }
    public void SetSpinSpeedForSpellNumber(int spellNumber)
    {
        switch (spellNumber)
        {
            case 1:
                spinSpeed = 0.5f;
                break;
            case 2:
                spinSpeed = 0.25f;
                break;
            case 3:
                spinSpeed = 0.12f;
                break;
            default:
                spinSpeed = 0.5f;
                break;
        }
    }
}