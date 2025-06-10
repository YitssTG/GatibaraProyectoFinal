using System;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputManagerEntry;

public class SlotObject : MonoBehaviour
{
    public int slotID;
    private ElementData currentElement;
    private GameObject currentPrefab;
    public void SetElement(ElementData data)
    {
        currentElement = data;
        if(currentPrefab != null)
        {
            Destroy(currentPrefab);
        }
        if(data != null && data.elementPrefab != null)
        {
            currentPrefab = Instantiate(data.elementPrefab, transform);
            currentPrefab.transform.localPosition = Vector3.zero;
            currentPrefab.transform.localRotation = Quaternion.identity;
            currentPrefab.transform.localScale = Vector3.one;
        }
    }
}
