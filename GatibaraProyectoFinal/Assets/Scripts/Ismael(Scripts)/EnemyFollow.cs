using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.HID;

public class EnemyFollow : MonoBehaviour
{
    public EnemyBarraVida barraUI;
    private float vidasMaximas;

    [Header("Stats Enemy DeBuffs")]

    public float vidas = 20;
    public int baseDamage = 5;
    [SerializeField] private int damageReduction;
    public int currentDamage;
    private NavMeshAgent agent;
    public static event Func<Transform> OnGetPlayerPosition;
    private static Transform playerTransform;
    private float tiempoUltimoGolpe = -999f;
    private float tiempoEntreGolpes = 1f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        damageReduction = 0;
        vidasMaximas = vidas;
        if (barraUI != null)
        {
            barraUI.SetVida(vidas, vidasMaximas);
        }
        if (playerTransform == null)
        {
            playerTransform = OnGetPlayerPosition?.Invoke();
        }
    }
    void Update()
    {
        Destination(playerTransform.position);
    }
    private void Destination(Vector3 destino)
    {
        agent.destination = destino;
    }
    public void RecibirAtaque()
    {
        vidas = vidas - 3;
        if (barraUI != null)
        {
            barraUI.SetVida(vidas, vidasMaximas);
        }

        if (vidas <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ApplyFireDamage(int cantidad)
    {
        vidas -= (cantidad * 0.1f);
        if (barraUI != null)
        {
            barraUI.SetVida(vidas, vidasMaximas);
        }
        if (vidas <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void ReduceDamage(int stacks)
    {
        damageReduction = stacks;
    }
    public void ResetDebuffs()
    {
        damageReduction = 0;
    }
    private void OnDestroy()
    {
        GameManager.instance.RegisterKill();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            currentDamage = Mathf.Max(0, baseDamage - damageReduction);
            GameManager.instance.PerderCorazones(currentDamage);
        }
    }
}