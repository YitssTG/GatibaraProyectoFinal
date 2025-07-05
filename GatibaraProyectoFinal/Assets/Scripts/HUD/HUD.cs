using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private GameObject monedaUIPrefab;
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private Transform monedaHUDTarget;

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
        Coin.OnCoinsCollection += AnimarMonedaHUD;
    }

    private void OnDisable()
    {
        Coin.OnCoinsCollection -= AnimarMonedaHUD;
    }
    public void AnimarMonedaHUD(Vector3 mundoPos)
    {
        Vector3 pantallaPos = Camera.main.WorldToScreenPoint(mundoPos);
        GameObject monedaUI = Instantiate(monedaUIPrefab, mainCanvas.transform);
        monedaUI.transform.position = pantallaPos;

        monedaUI.transform.DOMove(monedaHUDTarget.position, 0.5f)
            .SetEase(Ease.InOutCubic)
            .OnComplete(() =>
            {
                Destroy(monedaUI);
                UpdatePoints();
            });
    }
    public void UpdatePoints()
    {
        puntos++;
        points.text = puntos.ToString();
    }

    private void Update()
    {
        UpdateAbilityHUD();
    }
    public void SetAbilityCaster(AbilityCaster caster)
    {
        abilityCaster = caster;
    }

    public bool SpendPuntos(int amount)
    {
        if (puntos >= amount)
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