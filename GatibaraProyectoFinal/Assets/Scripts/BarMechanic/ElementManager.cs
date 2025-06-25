using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class ElementManager : MonoBehaviour
{
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
        Elements.spellnumber = player.spellnumber;
        Elements.ReduceSpellNumber();
    }
    [Button]
    
    public void SetLevel3()
    {
        player.spellnumber = 3;
        UpdateSlots();
        Debug.Log("Player puede usar tres habilidades a la vez");
    }
    public List<ElementData.ElementType> GetTypes()
    {
        List<ElementData> ordered = Elements.GetOrderedElements();
        List<ElementData.ElementType> types = new List<ElementData.ElementType>();
        for (int i = 0; i < player.spellnumber && i < ordered.Count; i++)
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
    public void ApplyElement(ElementData.ElementType type)
    {
        switch (type)
        {
            case ElementData.ElementType.Fire:
                OnFire();
                break;
            case ElementData.ElementType.Water:
                OnWater();
                break;
            case ElementData.ElementType.Wind:
                OnWind();
                break;
            case ElementData.ElementType.Earth:
                OnEarth();
                break;
        }
    }
}