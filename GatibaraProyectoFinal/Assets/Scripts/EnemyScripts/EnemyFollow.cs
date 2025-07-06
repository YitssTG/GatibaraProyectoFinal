using System;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class EnemyFollow : MonoBehaviour
{
    public EnemyBarraVida barraUI;
    private float vidasMaximas;
    private float originalSpeed;
    private bool isSlowed;

    [Header("Stats Enemy DeBuffs")]
    public float vidas = 20;
    public int baseDamage = 5;
    [SerializeField] private int damageReduction;
    public int currentDamage;

    private NavMeshAgent agent;
    public static event Func<Transform> OnGetPlayerPosition;
    private static Transform playerTransform;
    private bool isDying = false;

    // Feedback visual
    private Renderer enemyRenderer;
    private Color originalColor;
    private Coroutine flashRoutine;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        damageReduction = 0;
        vidasMaximas = vidas;

        isSlowed = false;
        originalSpeed = agent.speed;

        enemyRenderer = GetComponentInChildren<Renderer>();
        if (enemyRenderer != null)
        {
            originalColor = enemyRenderer.material.color;
        }

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
        if (!isDying)
        {
            Destination(playerTransform.position);
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

        if (barraUI != null)
        {
            barraUI.SetVida(vidas, vidasMaximas);
        }

        if (vidas <= 0 && !isDying)
        {
            MorirConAnimacion();
        }
    }
    public void RecibirAtaque(float abilitydamage)
    {
        vidas -= abilitydamage;
        FlashRed();

        if (barraUI != null)
        {
            barraUI.SetVida(vidas, vidasMaximas);
        }

        if (vidas <= 0 && !isDying)
        {
            MorirConAnimacion();
        }
    }
    public void ApplyFireDamage(int cantidad)
    {
        vidas -= (cantidad * 0.1f);
        FlashRed();

        if (barraUI != null)
        {
            barraUI.SetVida(vidas, vidasMaximas);
        }

        if (vidas <= 0 && !isDying)
        {
            MorirConAnimacion();
        }
    }
    private void FlashRed()
    {
        if (enemyRenderer == null) return;

        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        flashRoutine = StartCoroutine(FlashRoutine());
    }
    private System.Collections.IEnumerator FlashRoutine()
    {
        enemyRenderer.material.color = Color.red;
        yield return new WaitForSecondsRealtime(0.12f); // Compatible con Time.timeScale = 0
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

        transform.DOLocalRotate(new Vector3(0, 1440f, 0), 0.7f, RotateMode.FastBeyond360)
                 .SetEase(Ease.OutCubic);

        transform.DOScale(Vector3.zero, 0.4f)
                 .SetEase(Ease.InBack)
                 .SetDelay(0.3f)
                 .OnComplete(() =>
                 {
                     Destroy(gameObject);
                 });
    }
    public void ReduceDamage(int stacks)
    {
        damageReduction = stacks;
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            currentDamage = Mathf.Max(0, baseDamage - damageReduction);
            GameManager.instance.PerderCorazones(currentDamage);
        }
    }
}