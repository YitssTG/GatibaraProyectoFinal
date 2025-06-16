using UnityEngine;
using DG.Tweening;

public class GeneratorEnemy : MonoBehaviour
{
    public GameObject enemigoPrefab;
    public Transform[] puntosDeSpawn; 
    public float distanciaActivacion = 10f;
    public float intervaloSpawn = 2f;
    public int cantidadEnemigos = 3;

    public Transform jugador;
    private bool activado = false;
    private float tiempo;

    void Update()
    {
        if (!activado && Vector3.Distance(transform.position, jugador.position) < distanciaActivacion)
        {
            activado = true;
            tiempo = 0f;
        }

        if (activado && cantidadEnemigos > 0)
        {
            tiempo += Time.deltaTime;
            if (tiempo >= intervaloSpawn)
            {
                SpawnEnemigo();
                tiempo = 0f;
            }
        }
    }
    void SpawnEnemigo()
    {
        if (puntosDeSpawn.Length == 0) return;

        int indice = Random.Range(0, puntosDeSpawn.Length);
        Vector3 centro = puntosDeSpawn[indice].position;
        Vector2 offset = Random.insideUnitCircle * 3f; 
        Vector3 posicionFinal = new Vector3(centro.x + offset.x, centro.y, centro.z + offset.y);
        Vector3 posicionInicial = new Vector3(posicionFinal.x, posicionFinal.y - 3f, posicionFinal.z);
        GameObject enemigo = Instantiate(enemigoPrefab, posicionInicial, Quaternion.identity);
        enemigo.transform.DOMoveY(posicionFinal.y, 0.5f).SetEase(Ease.OutBack);
        cantidadEnemigos--;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (puntosDeSpawn != null)
        {
            foreach (Transform punto in puntosDeSpawn)
            {
                Gizmos.DrawWireSphere(punto.position, 3f);
            }
        }
    }
    //void SpawnEnemigo()
    //{
    //    if (puntosDeSpawn.Length == 0) return;

    //    int indice = Random.Range(0, puntosDeSpawn.Length);
    //    Vector3 centro = puntosDeSpawn[indice].position;

    //    Vector2 offset = Random.insideUnitCircle * 2f;
    //    Vector3 posicionFinal = new Vector3(centro.x + offset.x, centro.y, centro.z + offset.y);
    //    Vector3 posicionInicial = new Vector3(posicionFinal.x, posicionFinal.y - 2f, posicionFinal.z); 

    //    GameObject enemigo = Instantiate(enemigoPrefab, posicionInicial, Quaternion.identity);

    //    enemigo.transform.DOMoveY(posicionFinal.y, 0.5f).SetEase(Ease.OutBack);
    //}
}
