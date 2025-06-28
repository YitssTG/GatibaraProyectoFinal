using UnityEngine;
using System.Collections.Generic;
using Unity.Mathematics;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int maxSpellNumberUnlocked;
    public int MaxSpellNumberUnlocked
    {
        get
        {
            return maxSpellNumberUnlocked;
        }
        set
        {
            maxSpellNumberUnlocked = value;
        }
    }
    [SerializeField] private int spellUnlockCost;
    private int enemiesKilled;
    public int EnemyKilled
    {
        get
        {
            return enemiesKilled;
        }
        set
        {
            enemiesKilled = value;
        }
    }
    private int requiredKills;
    public int RequiredKills
    {
        get
        {
            return requiredKills;
        }
        set
        {
            requiredKills = value;
        }
    }

    public static event Action OnAllEnemiesKilled;

    [Header("References")]
    [SerializeField] HUD hud;
    [SerializeField] PlayerGatibara playerGatibara;
    [SerializeField] ElementManager manager;

    //public int PuntosTotales { get; private set; }
    [Header("Data Structure")]
    private List<GenerateEnemy> pointGenerateEnemy = new List<GenerateEnemy>();

    [Header("Enemy Generation Data")]
    private Transform player;
    private float minDistance;
    private float currentDistance;
    private int indexListPoint;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        maxSpellNumberUnlocked = 1;
        hud.SetPlayer(playerGatibara);
    }
    private void OnEnable()
    {
        Health.OnHealthDestroy += RecuperarCorazon;
    }

    private void OnDisable()
    {
        Health.OnHealthDestroy -= RecuperarCorazon;
    }

    public void AddpointGenerateEnemy(GenerateEnemy generate)
    {
        pointGenerateEnemy.Add(generate);
    }
    private void Update()
    {
        UpdatePointGereateEnemy();
    }
    private void UpdatePointGereateEnemy()
    {
        if (player == null || pointGenerateEnemy.Count == 0) return;
        minDistance = math.INFINITY;
        for (int i = 0; i < pointGenerateEnemy.Count; ++i)
        {
            currentDistance = Vector3.Distance(player.position, pointGenerateEnemy[i].gameObject.transform.position);
            pointGenerateEnemy[i].enabled = false;
            if (minDistance > currentDistance)
            {
                minDistance = currentDistance;
                indexListPoint = i;
            }
        }
        pointGenerateEnemy[indexListPoint].enabled = true;
    }
    public void SetPlayerTransform(Transform transform_Player)
    {
        player = transform_Player;
    }
    //public void SumarPuntos(int sumaPuntos)
    //{
    //    PuntosTotales += sumaPuntos;
    //}
    public void PerderCorazones(int cantidad)
    {
        playerGatibara.vida -= cantidad;

        hud.UpdateLifeBar();

        if (playerGatibara.vida <= 0)
        {
            Debug.Log("¡Jugador sin corazones! Fin del juego.");
            PopUpController.instance.ShowLosePopUp();
        }
    }
    public void RecuperarCorazon()
    {
        if (playerGatibara.vida < hud.GetVidaMax())
        {
            playerGatibara.vida++;
            hud.UpdateLifeBar();
            Debug.Log("Recuperaste un corazón. Corazones actuales: " + playerGatibara.vida);
        }
        else
        {
            Debug.Log("Vida al máximo. No se agregó corazón.");
        }
    }
    public int GetCurrentSpellNumber()
    {
        return playerGatibara.spellNumber;
    }
    public void SetRequiredKills(int required)
    {
        RequiredKills = required;
        enemiesKilled = 0;
    }
    public void RegisterKill()
    {
        enemiesKilled++;
        Debug.Log("Enemigos eliminados: " + requiredKills);
        if(enemiesKilled >= RequiredKills)
        {
            OnAllEnemiesKilled?.Invoke();
        }
    }
    public bool UnlockNewSpellNumber()
    {
        if(maxSpellNumberUnlocked < 3)
        {
            if (hud.puntos >= spellUnlockCost)
            {
                hud.SpendPuntos(spellUnlockCost);
                maxSpellNumberUnlocked++;
                spellUnlockCost *= 2;
                playerGatibara.SetGatibaraLevel(maxSpellNumberUnlocked);
                Debug.Log("Aumentaste el número de elementos que puedes usar a ");
                return true;
            }
            else
            {
                Debug.Log("No tienes suficientes monedas para desbloquear");
                return false;
            }
        }
        else
        {
            Debug.Log("Ya alcanzaste el máximo de nivel de elementos");
            return false;
        }
    }
    public int GetCost()
    {
        return spellUnlockCost;
    }
}
