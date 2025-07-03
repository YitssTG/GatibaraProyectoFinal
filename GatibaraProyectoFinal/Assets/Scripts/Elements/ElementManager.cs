using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class ElementManager : MonoBehaviour
{
    //maneja elementos
    public SlotObject[] slots;
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
        Elements.spellnumber = player.spellNumber;
        Elements.ReduceSpellNumber();
    }
    public List<ElementType> GetTypes()
    {
        List<ElementData> ordered = Elements.GetOrderedElements();
        List<ElementType> types = new List<ElementType>();
        for (int i = 0; i < player.spellNumber && i < ordered.Count; i++)
        {
            types.Add(ordered[i].type);
        }
        return types;
    }
    public void OnEarth()
    {
        Elements.AddElement(earth);
        OnCkeck?.Invoke(Elements, earth);
        UpdateSlots();
    }
    public void OnFire()
    {
        Elements.AddElement(fire);
        OnCkeck?.Invoke(Elements, fire);
        UpdateSlots();
    }
    public void OnWater()
    {
        Elements.AddElement(water);
        OnCkeck?.Invoke(Elements, water);
        UpdateSlots();
    }
    public void OnWind()
    {
        Elements.AddElement(wind);
        OnCkeck?.Invoke(Elements, wind);
        UpdateSlots();
    }
    public void UpdateSlots()
    {
        List<ElementData> ordered = Elements.GetOrderedElements();
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < ordered.Count)
            {
                slots[i].SetElement(ordered[i]);
            }
            else
            {
                slots[i].SetElement(null);
            }
        }
    }
    public void ApplyElement(ElementType type)
    {
        switch (type)
        {
            case ElementType.Fire:
                OnFire();
                break;
            case ElementType.Water:
                OnWater();
                break;
            case ElementType.Wind:
                OnWind();
                break;
            case ElementType.Earth:
                OnEarth();
                break;
        }
    }
}