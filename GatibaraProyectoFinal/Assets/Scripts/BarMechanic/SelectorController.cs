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
    void Start()
    {
        isSpinning = false;
        ElementBarPanel.SetActive(false);
    }
    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)
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
                ElementData.ElementType selected = elementUI.GetCurrentElementType();
                ApplyElement(selected);
            }
        }
    }
    private void ApplyElement(ElementData.ElementType type)
    {
        switch (type)
        {
            case ElementData.ElementType.Fire:
                manager.OnFire();
                break;
            case ElementData.ElementType.Water:
                manager.OnWater();
                break;
            case ElementData.ElementType.Wind:
                manager.OnWind();
                break;
            case ElementData.ElementType.Earth:
                manager.OnEarth();
                break;
        }
    }
}
