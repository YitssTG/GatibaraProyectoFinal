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
    public int damageReduction = 1;

    public float vidas = 20;
    public int baseDamage = 4;
    public int currentDamage;
    private NavMeshAgent agent;
    public static event Func<Transform> OnGetPlayerPosition;
    private static Transform playerTransform;
    private float tiempoUltimoGolpe = -999f;
    private float tiempoEntreGolpes = 1f;

   
    public ElementType resistances;

    private Coroutine fireCoroutine;
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

    public void TakeDamage(float damage, ElementType element)
    {

    }
    public void RecibirFuego(int cantidad)
    {
        vidas -= (cantidad * 0.01f);
        if (barraUI != null)
        {
            barraUI.SetVida(vidas, vidasMaximas);
        }
        if (vidas <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void ApplyFireDamage(int stacks)
    {
        if(fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
        }
        fireCoroutine = StartCoroutine(FireTick(stacks));
    }
    private IEnumerator FireTick(int stacks)
    {
        while (true)
        {
            RecibirFuego(stacks);
            yield return new WaitForSeconds(1);
        }
    }
    public void StopFireEffect()
    {
        if(fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && Time.time - tiempoUltimoGolpe > tiempoEntreGolpes)
        {
            tiempoUltimoGolpe = Time.time;
            PlayerGatibara player = collision.gameObject.GetComponent<PlayerGatibara>();
            if(player != null)
            {
                currentDamage = Mathf.Max(0, baseDamage - damageReduction);
                GameManager.instance.PerderCorazones(currentDamage);
            }
            Debug.Log("Jugador alcanzado, pierde vida.");
        }
    }
    public void ReduceDamage(int stacks)
    {
        damageReduction = stacks;
    }
    public void ResetDebuffs()
    {
        damageReduction = 0;
        Debug.Log("Debuff Reseted");
    }
}