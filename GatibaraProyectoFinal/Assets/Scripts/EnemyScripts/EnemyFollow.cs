using System;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class EnemyFollow : MonoBehaviour
{
    [Header("Detección y ataque")]
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float requiredStayTime = 3f; // Tiempo necesario en rango
    [SerializeField] private float attackCooldown = 5f;
    private float lastAttackTime;
    private float playerInsideTime = 0f;
    private bool isPlayerInRange = false;

    public EnemyBarraVida barraUI;
    private float vidasMaximas;
    private float originalSpeed;
    private bool isSlowed;

    [Header("Stats Enemy DeBuffs")]
    public float vidas = 20;
    public int baseDamage = 0;
    [SerializeField] private int damageReduction;
    public int currentDamage;

    private NavMeshAgent agent;
    public static event Func<Transform> OnGetPlayerPosition;
    private static Transform playerTransform;
    private bool isDying = false;

    private Renderer enemyRenderer;
    private Color originalColor;
    private Coroutine flashRoutine;
    private Animator animator;

    void Awake()
    {
        animator=GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        damageReduction = 0;
        vidasMaximas = vidas;
        isSlowed = false;
        originalSpeed = agent.speed;

        if (barraUI != null) barraUI.SetVida(vidas, vidasMaximas);

        enemyRenderer = GetComponentInChildren<Renderer>();
        if (enemyRenderer != null) originalColor = enemyRenderer.material.color;

        if (playerTransform == null) playerTransform = OnGetPlayerPosition?.Invoke();

        lastAttackTime = Time.time; // inicia cooldown desde que aparece
    }
    void Update()
    {
        if (isDying || playerTransform == null) return;

        Destination(playerTransform.position);

        float distancia = Vector3.Distance(transform.position, playerTransform.position);

        if (distancia <= attackRange)
        {
            isPlayerInRange = true;
            playerInsideTime += Time.deltaTime;

            if (playerInsideTime >= requiredStayTime && Time.time >= lastAttackTime + attackCooldown)
            {
                AtacarJugador();
                lastAttackTime = Time.time;

                playerInsideTime = 0f; // reinicia el tiempo tras atacar
            }
        }
        else
        {
            isPlayerInRange = false;
            playerInsideTime = 0f;
            animator.SetTrigger("Walk");
        }
    }
    private void Destination(Vector3 destino)
    {
        agent.destination = destino;
    }
    public void RecibirAtaque()
    {
        vidas -= 3;
        FlashRed();
        barraUI?.SetVida(vidas, vidasMaximas);

        if (vidas <= 0 && !isDying)
            MorirConAnimacion();
    }
    public void RecibirAtaque(float abilitydamage)
    {
        vidas -= abilitydamage;
        FlashRed();
        barraUI?.SetVida(vidas, vidasMaximas);

        if (vidas <= 0 && !isDying)
            MorirConAnimacion();
    }
    public void ApplyFireDamage(int cantidad)
    {
        vidas -= (cantidad * 0.1f);
        FlashRed();
        barraUI?.SetVida(vidas, vidasMaximas);

        if (vidas <= 0 && !isDying)
            MorirConAnimacion();
    }
    private void FlashRed()
    {
        if (enemyRenderer == null) return;
        if (flashRoutine != null) StopCoroutine(flashRoutine);
        flashRoutine = StartCoroutine(FlashRoutine());
    }
    private System.Collections.IEnumerator FlashRoutine()
    {
        enemyRenderer.material.color = Color.red;
        yield return new WaitForSecondsRealtime(0.12f);
        enemyRenderer.material.color = originalColor;
        flashRoutine = null;
    }
    private void MorirConAnimacion()
    {
        isDying = true;
        if (agent != null) agent.enabled = false;

        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        this.enabled = false;

        transform.DOLocalRotate(new Vector3(0, 1440f, 0), 0.7f, RotateMode.FastBeyond360).SetEase(Ease.OutCubic);
        transform.DOScale(Vector3.zero, 0.4f).SetEase(Ease.InBack).SetDelay(0.3f).OnComplete(() => Destroy(gameObject));
    }
    public void ReduceDamage(int stacks)
    {
        damageReduction = Mathf.Max(0, stacks);
    }
    public void ResetDebuffs()
    {
        damageReduction = 0;
    }
    public void SpeedModify(float percentage)
    {
        if (!isSlowed)
        {
            agent.speed *= percentage;
            isSlowed = true;
        }
    }
    public void SpeedRestore()
    {
        if (isSlowed)
        {
            agent.speed = originalSpeed;
            isSlowed = false;
        }
    }
    private void OnDestroy()
    {
        GameManager.instance.RegisterKill();
    }
    private void AtacarJugador()
    {
        animator.SetTrigger("Ataque");
        int dañoCalculado = baseDamage - damageReduction;
        Debug.Log($"[DEBUG] {gameObject.name} → base: {baseDamage}, reducción: {damageReduction}, bruto: {dañoCalculado}");

        currentDamage = Mathf.Clamp(Mathf.Max(0, dañoCalculado), 0, 1);
        GameManager.instance.PerderCorazones(currentDamage);
        Debug.Log($"{gameObject.name} atacó al jugador → Daño final: {currentDamage}");

        transform.DOShakePosition(0.2f, 0.2f, 10, 90);
    }
}