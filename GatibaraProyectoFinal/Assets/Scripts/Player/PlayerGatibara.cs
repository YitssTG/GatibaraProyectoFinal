using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGatibara : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] public float speed;
    [SerializeField] HUD hud;
    [SerializeField] UnlockedAbilities unlockedAbilities;

    public float vida;
    private float baseAttackSpeed;
    public float currentAttackSpeed;
    private float bonusSpeed;
    public int spellnumber;
    private void Start()
    {
        baseAttackSpeed = 1f;
        bonusSpeed = 100f;
        spellnumber = 1;
        LoadStats();
    }
    private void OnEnable()
    {
        Health.OnHealthDestroy += CollectHealth;
        EnemyFollow.OnGetPlayerPosition += GetPlayer;

    }
    private void OnDisable()
    {
        Health.OnHealthDestroy -= CollectHealth;
        EnemyFollow.OnGetPlayerPosition -= GetPlayer;
    }
    private Transform GetPlayer()
    {
        return transform;
    }
    public void IncreaseAttackSpeed(int stacks)
    {
        bonusSpeed = 0.2f * stacks;
        UpdateEffect();
    }
    public void ResetEffect()
    {
        bonusSpeed = 0f;
        UpdateEffect();
        Debug.Log("Efectos reseteados");
    }
    public void UpdateEffect()
    {
        currentAttackSpeed = baseAttackSpeed + bonusSpeed;
    }
    public void CollectHealth()
    {
        Debug.Log("Corazon Recogido");
    }
    public void LoadStats()
    {
        if (PlayerPrefs.HasKey("Coins"))
        {
            hud.puntos = PlayerPrefs.GetInt("Coins");
        }
        if (PlayerPrefs.HasKey("SpellNumber"))
        {
            spellnumber = PlayerPrefs.GetInt("SpellNumber");
        }
        if (PlayerPrefs.HasKey("PositionX") && PlayerPrefs.HasKey("PositionY") && PlayerPrefs.HasKey("PositionZ"))
        {
            float x = PlayerPrefs.GetFloat("PositionX");
            float y = PlayerPrefs.GetFloat("PositionY");
            float z = PlayerPrefs.GetFloat("PositionZ");
            transform.position = new Vector3(x, y, z);
        }

        unlockedAbilities.LoadSaved();
    }
}