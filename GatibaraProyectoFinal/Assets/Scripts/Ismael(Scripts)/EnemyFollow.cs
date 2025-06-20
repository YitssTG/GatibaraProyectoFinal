using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public EnemyBarraVida barraUI;
    private int vidasMaximas;

    [Header("Stats Enemy DeBuffs")]
    public float fireDamage = 0f;
    public float attackSpeedPenalty = 0f;
    public float speedMovementPenalty = 0f;

    private float fireDamageInterval = 1f;
    private float fireDamageTimer = 0f;

    public int vidas = 20;
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
        if (fireDamage > 0)
        {
            fireDamageTimer += Time.deltaTime;
            if (fireDamageTimer >= fireDamageInterval)
            {
                fireDamageTimer = 0f;
                RecibirFuego();
            }
        }
    }
    private void Destination(Vector3 destino)
    {
        agent.destination = destino;
        //if (speedMovementPenalty > 0)
        //{
        //    agent.speed = Mathf.Max(0, agent.speed * (1f - speedMovementPenalty));
        //}
    }
    public void RecibirAtaque(Vector3 direccion)
    {
        vidas--;
        if (barraUI != null)
        {
            barraUI.SetVida(vidas, vidasMaximas);
        }

        if (vidas <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void RecibirFuego()
    {
        vidas -= (int)fireDamage;
        if (barraUI != null)
        {
            barraUI.SetVida(vidas, vidasMaximas);
        }
        Debug.Log($"🔥 Daño de fuego: {fireDamage}, Vidas restantes: {vidas}");

        if (vidas <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Time.time - tiempoUltimoGolpe > tiempoEntreGolpes)
        {
            tiempoUltimoGolpe = Time.time;
            Debug.Log("Jugador alcanzado, pierde un corazón.");
            GameManager.instance.PerderCorazones();
        }
    }
    public void ApplyFireDamage(int stacks)
    {
        fireDamage = 5f * stacks;
    }
    public void ReduceAttackSpeed(int stacks)
    {
        attackSpeedPenalty = 0.1f * stacks;
    }
    public void ReduceMovementSpeed(int stacks)
    {
        speedMovementPenalty = 0.1f * stacks;
    }
    public void ResetDebuffs()
    {
        fireDamage = 0;
        attackSpeedPenalty = 0;
        speedMovementPenalty = 0;
        fireDamageTimer = 0f; 
        Debug.Log("Debuff Reseted");
    }
}