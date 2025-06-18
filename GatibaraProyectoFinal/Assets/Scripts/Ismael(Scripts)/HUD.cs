using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public TMP_Text points;
    public GameObject[] corazones;

    [Header("Coin Data")]
    private int puntos;
    private void OnEnable()
    {
        Coin.OnCoinsCollection += UpdatePoints;
    }
    private void OnDisable()
    {
        Coin.OnCoinsCollection -= UpdatePoints;
    }
    public void UpdatePoints()
    {
        ++puntos;
        points.text = puntos.ToString();
    }
    public void DesactivarCorazones(int indice)
    {
        corazones[indice].SetActive(false);
    }
    public void ActivarCorazones(int indice)
    {
        corazones[indice].SetActive(true);
    }
}