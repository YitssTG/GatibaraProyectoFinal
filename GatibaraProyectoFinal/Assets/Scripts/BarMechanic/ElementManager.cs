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
    public void SetLevel2()
    {
        player.spellnumber = 2;
        UpdateSlots();
        Debug.Log("Player puede usar dos habilidades a la vez");
    }
    [Button]
    public void SetLevel3()
    {
        player.spellnumber = 3;
        UpdateSlots();
        Debug.Log("Player puede usar tres habilidades a la vez");
    }
    //[Button]
    //public void CombineElements()
    //{
    //    List<ElementData> combination = Elements.GetOrderedElements();
    //    if(combination.Count == 3)
    //    {
    //        string comboKey = 
    //    }
    //}
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
    private void UpdateSlots()
    {
        List<ElementData> ordered = Elements.GetOrderedElements();
        for(int i = 0; i< slots.Length; i++)
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
}
