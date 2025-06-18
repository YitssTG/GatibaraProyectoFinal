using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public TextMeshPro points;
    public GameObject[] corazones;

    private void Update()
    {
        points.text = GameManager.instance.PuntosTotales.ToString();
    }
    public void UpdatePoints(int totalPoints)
    {
        points.text = totalPoints.ToString();
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
