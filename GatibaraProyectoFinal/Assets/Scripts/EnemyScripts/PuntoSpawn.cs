using DG.Tweening;
using UnityEngine;

public class PuntoSpawn : MonoBehaviour
{
    public GameObject enemigoPrefab;
    public GameObject particulasTierraPrefab; 
    public Transform jugador;

    public float distanciaActivacion = 10f;
    public float intervaloSpawn = 2f;
    public int cantidadEnemigos = 5;

    private bool activado;
    private int enemigosRestantes;
    private float tiempo;

    void Start()
    {
        enemigosRestantes = cantidadEnemigos;
        activado = false;
    }

    void Update()
    {
        if (!activado && Vector3.Distance(jugador.position, transform.position) < distanciaActivacion)
        {
            activado = true;
            tiempo = 0f;
        }

        if (activado && enemigosRestantes > 0)
        {
            tiempo += Time.deltaTime;
            if (tiempo >= intervaloSpawn)
            {
                GenerarEnemigo();
                tiempo = 0f;
            }
        }
    }

    void GenerarEnemigo()
    {
        Vector2 offset = Random.insideUnitCircle * 3f;
        Vector3 posicionFinal = new Vector3(transform.position.x + offset.x, transform.position.y, transform.position.z + offset.y);
        Vector3 posicionInicial = new Vector3(posicionFinal.x, posicionFinal.y - 3f, posicionFinal.z);

        GameObject enemigo = Instantiate(enemigoPrefab, posicionInicial, Quaternion.identity);
        enemigo.transform.DOMoveY(posicionFinal.y, 0.5f).SetEase(Ease.OutBack);
        if (particulasTierraPrefab != null)
        {
            GameObject particulas = Instantiate(particulasTierraPrefab, posicionFinal, Quaternion.identity);
            ParticleSystem ps = particulas.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Play();
                Destroy(particulas, ps.main.duration + ps.main.startLifetime.constantMax);
            }
        }

        enemigosRestantes--;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, distanciaActivacion);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 3f);
    }
    public bool GetState()
    {
        return activado;
    }
}
