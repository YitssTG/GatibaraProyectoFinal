using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class ElementManager : MonoBehaviour
{
    private CustomSimpleLinkedList<ElementData> elements;
    public event Action<List<ElementData>> OnElementsChanged;
    public static event Action<CustomSimpleLinkedList<ElementData>, ElementData> OnCkeck;

    public ElementData wind;
    public ElementData earth;
    public ElementData fire;
    public ElementData water;

    public PlayerGatibara player;
    private void Awake()
    {
        elements = new CustomSimpleLinkedList<ElementData>();
    }
    public void Start()
    {
        elements.spellnumber = 1;
    }
    void Update()
    {
        elements.spellnumber = player.spellnumber;
        elements.ReduceSpellNumber();
    }
    public void AddElement(ElementData element)
    {
        elements.Add(element);
        OnCkeck?.Invoke(elements, element);
        OnElementsChanged?.Invoke(elements.GetOrderedElements());
    }
    public List<ElementData.ElementType> GetTypes()
    {
        List<ElementData> ordered = elements.GetOrderedElements();
        List<ElementData.ElementType> types = new List<ElementData.ElementType>();
        for (int i = 0; i < player.spellnumber && i < ordered.Count; i++)
        {
            types.Add(ordered[i].type);
        }
        return types;
    }
}
