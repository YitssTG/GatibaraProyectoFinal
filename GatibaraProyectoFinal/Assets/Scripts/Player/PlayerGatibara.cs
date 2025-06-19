using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGatibara : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] public float speed;

    private float baseAttackSpeed;
    public float currentAttackSpeed;
    private float bonusSpeed;
    //int coins;
    public int spellnumber;

    private void Start()
    {
        baseAttackSpeed = 1f;
        bonusSpeed = 100f;
        spellnumber = 1;
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
}