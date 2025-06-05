using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class OldElementManager : MonoBehaviour
{
    private CustomSimpleLinkedList<Elements> Elements = new CustomSimpleLinkedList<Elements>();
    public Elements wind;
    public Elements tierra;
    public Elements fuego;
    public Elements agua;
    public PlayerGatibara player;
    private void Update()
    {
        Elements.spellnumber = player.spellnumber;
    }
    [Button]
    public void InvokeSpell()
    {

    }
    [Button]
    public void AddWindElement(Elements element)
    {
        Elements.AddElement(wind);
        //Elements.ReadAllElements();
    }
    [Button]
    public void AddEarthElement(Elements element)
    {
        Elements.AddElement(tierra);
        //Elements.ReadAllElements();
    }
    [Button]
    public void AddFireElement(Elements element)
    {
        Elements.AddElement(fuego);
        //Elements.ReadAllElements();
    }
    [Button]
    public void AddWaterElement(Elements element)
    {
        Elements.AddElement(agua);
        //Elements.ReadAllElements();
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
}
