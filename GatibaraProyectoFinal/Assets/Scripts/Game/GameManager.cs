using UnityEngine;
using System.Collections.Generic;
using Unity.Mathematics;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public HUD hud;
    public PlayerGatibara playerGatibara;

    [Header("PopUp")]
    public PopUpController popUpController;

    public int PuntosTotales { get; private set; }
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
    public void SumarPuntos(int sumaPuntos)
    {
        PuntosTotales += sumaPuntos;
    }
    public void PerderCorazones()
    {
        playerGatibara.vida = playerGatibara.vida -3;

        hud.UpdateLifeBar();

        if (playerGatibara.vida <= 0)
        {
            Debug.Log("¡Jugador sin corazones! Fin del juego.");
            popUpController.ShowLosePopUp();
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
}
