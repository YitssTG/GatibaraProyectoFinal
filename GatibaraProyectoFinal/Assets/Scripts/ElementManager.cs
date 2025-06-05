using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputManagerEntry;

public class ElementManager : MonoBehaviour
{
    private CustomSimpleLinkedList<ElementData> Elements;
    public static event Action<CustomSimpleLinkedList<ElementData>, ElementData> OnCkeck;

    public ElementData wind;
    public ElementData earth;
    public ElementData fire;
    public ElementData water;

    public PlayerGatibara player;
    private void Awake()
    {
        Elements = new CustomSimpleLinkedList<ElementData>();
    }
    public void Start()
    {
        Elements.spellnumber = 1;
    }
    void Update()
    {
        Elements.spellnumber = player.spellnumber;
    }
    [Button]
    public void SetLevel2()
    {
        player.spellnumber = 2;
        Debug.Log("Player puede usar dos habilidades a la vez");
    }
    [Button]
    public void SetLevel3()
    {
        player.spellnumber = 3;
        Debug.Log("Player puede usar tres habilidades a la vez");
    }
    public void OnNPressedEarth(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Elements.AddElement(earth);
            OnCkeck?.Invoke(Elements, earth);
        }
    }
    public void OnJPressedFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Elements.AddElement(fire);
            OnCkeck?.Invoke(Elements, fire);
        }
    }
    public void OnKPressedWater(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Elements.AddElement(water);
            OnCkeck?.Invoke(Elements, water);
        }
    }
    public void OnLPressedWind(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Elements.AddElement(wind);
            OnCkeck?.Invoke(Elements, wind);
        }
    }
}
