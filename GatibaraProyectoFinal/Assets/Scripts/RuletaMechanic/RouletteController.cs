using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RouletteController : MonoBehaviour
{
    [SerializeField] private Rotation2D roulette;
    private bool primerClick;
    float result;
    [SerializeField] private ElementManager manager;

    void Start()
    {
        roulette.spinning = false;
        primerClick = true;
    }
    public void LeftClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(primerClick)
            {
                SpinTheRoulette();
                primerClick = false;
            }
            else
            {
                SelectTheResult();
                primerClick = true;
            }
        }
    }
    public void SpinTheRoulette()
    {
        roulette.GetComponent<RectTransform>().rotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0f, 360f));
        roulette.spinning = true;
    }
    public void SelectTheResult()
    {
        roulette.spinning = false;
        Debug.Log(result);
        result = roulette.GetComponent<RectTransform>().rotation.eulerAngles.z;
        if (result >= 0f && result < 90f)
        {
            manager.OnFire();
        }
        else if(result >= 90f && result < 180f)
        {
            manager.OnWater();
        }
        else if(result >= 180f && result < 270f)
        {
            manager.OnWind();
        }
        else
        {
            manager.OnEarth();
        }
    }
}
