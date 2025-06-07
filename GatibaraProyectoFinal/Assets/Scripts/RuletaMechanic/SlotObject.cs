using System;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.InputManagerEntry;

public class SlotObject : MonoBehaviour
{
    public int id;

    public static event Action OnDestroy;
    public static event Action<Vector3> OnUpdateTransform;

    public GameObject FuegoObject;
    public GameObject WaterObject;
    public GameObject EarthObject;
    public GameObject WindObject;

    private void OnEnable()
    {
        ElementManager.OnCkeck += CheckElement;
    }
    private void OnDisable()
    {
        ElementManager.OnCkeck -= CheckElement;
    }
    private void FixedUpdate()
    {
        if(id == 0)
        {
            OnUpdateTransform?.Invoke(this.transform.position);
        }
    }
    public void CheckElement(CustomSimpleLinkedList<ElementData> elements, ElementData elementType)
    {
        if (elements.GetValueAt(id) == elementType)
        {
            if (elementType.element == "FireElement")
            {
                OnDestroy?.Invoke();
                Instantiate(FuegoObject, this.transform.position, this.transform.rotation);
                Debug.Log("Haztefuego");
            }
            else if(elementType.element == "WaterElement")
            {
                OnDestroy?.Invoke();
                Instantiate(WaterObject, this.transform.position, this.transform.rotation);
                Debug.Log("Hazteagua");
            }
            else if (elementType.element == "EarthElement")
            {
                OnDestroy?.Invoke();
                Instantiate(EarthObject, this.transform.position, this.transform.rotation);
                Debug.Log("Haztetierra");
            }
            else if (elementType.element == "WindElement")
            {
                OnDestroy?.Invoke();
                Instantiate(WindObject, this.transform.position, this.transform.rotation);
                Debug.Log("Hazteviento");
            }
        }
        else if(id == 0)
        {
            Debug.Log("Why? 0");
        }
        else if(id == 1)
        {
            Debug.Log("Aún no puedes invocar 2 elementos al mismo tiempo");
        }
        else if (id == 2)
        {
            Debug.Log("Aún no puedes invocar 3 elementos al mismo tiempo");
        }
    }
}
