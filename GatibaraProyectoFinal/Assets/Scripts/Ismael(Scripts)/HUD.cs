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

    [Header("Ability Cooldown UI")]
    public Image abilityIcon;
    public TMP_Text cooldownText;

    private AbilityCaster abilityCaster;

    private void OnEnable()
    {
        Coin.OnCoinsCollection += UpdatePoints;
    }
    private void OnDisable()
    {
        Coin.OnCoinsCollection -= UpdatePoints;
    }
    private void Update()
    {
        UpdateAbilityHUD();
    }
    //public int GetPuntos()
    //{
    //    return puntos;
    //}
    public void SetAbilityCaster(AbilityCaster caster)
    {
        abilityCaster = caster;
    }
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
    private void UpdateAbilityHUD()
    {
        if (abilityCaster == null)
        {
            abilityIcon.enabled = false;
            cooldownText.text = "";
            return;
        }
        var nextAbility = abilityCaster.GetAvailableAbility();
        if (nextAbility != null)
        {
            abilityIcon.sprite = nextAbility.data.icon;
            abilityIcon.enabled = true;
            cooldownText.text = nextAbility.cooldownRemaining.ToString("F1") + "s";
        }
        else
        {
            // Mostrar icono vacío o desactivar el actual
            abilityIcon.enabled = false;
            cooldownText.text = "";
        }
    }
}