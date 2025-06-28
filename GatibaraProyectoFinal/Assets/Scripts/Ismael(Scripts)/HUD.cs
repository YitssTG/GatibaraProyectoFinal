using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public TMP_Text points;
    public Image rellenoVida;
    private PlayerGatibara playerGatibara;
    private float vidaMax;

    [Header("Coin Data")]
    public int puntos;

    private void OnEnable()
    {
        Coin.OnCoinsCollection += UpdatePoints;
    }
    private void OnDisable()
    {
        Coin.OnCoinsCollection -= UpdatePoints;
    }
    //public int GetPuntos()
    //{
    //    return puntos;
    //}
    public void UpdatePoints()
    {
        ++puntos;
        points.text = puntos.ToString();
    }
    public bool SpendPuntos(int amount)
    {
        if(puntos >= amount)
        {
            puntos -= amount;
            points.text = puntos.ToString();
            return true;
        }
        else
        {
            return false;
        }
    }
    public void UpdateLifeBar()
    {
        rellenoVida.fillAmount = playerGatibara.vida / vidaMax;
    }
    public void SetPlayer(PlayerGatibara player)
    {
        playerGatibara = player;
        vidaMax = player.vida;
        UpdateLifeBar();
    }
    public float GetVidaMax()
    {
        return vidaMax;
    }
}