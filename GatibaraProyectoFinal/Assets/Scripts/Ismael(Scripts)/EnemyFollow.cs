using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public int vidas = 3;
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
    public void RecibirAtaque(Vector3 direccion)
    {
        vidas--;
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
}