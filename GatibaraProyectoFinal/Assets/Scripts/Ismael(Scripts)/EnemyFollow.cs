using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform jugador;
    public float velocidadBase = 3f;
    public float distanciaStop = 2f;
    public float distanciaRay = 2f;

    [SerializeField] private AnimationCurve curvaMovimiento;
    [SerializeField] private float tiempoAceleracion = 1.5f;

    private bool puedeSeguir = false;
    private bool enMovimiento = false;
    private float tiempoActual = 0f;

    private void Start()
    {
        StartCoroutine(EsperarYActivarMovimiento(0.6f));
    }
    //void HabilitarMovimiento()
    //{
    //    puedeSeguir = true;
    //}

    void Update()
    {
        if (!puedeSeguir || jugador == null) return;

        Vector3 direccion = (jugador.position - transform.position).normalized;
        RaycastHit hit;
        bool detectaJugador = Physics.Raycast(transform.position, direccion, out hit, distanciaRay);

        if (detectaJugador && hit.collider.CompareTag("Player"))
        {
            enMovimiento = false; 
            return;
        }
        float distancia = Vector3.Distance(transform.position, jugador.position);
        if (distancia > distanciaStop)
        {
            enMovimiento = true;
        }
        if (enMovimiento)
        {
            tiempoActual += Time.deltaTime / tiempoAceleracion;
            float curvaValor = curvaMovimiento.Evaluate(tiempoActual);
            transform.position += direccion * velocidadBase * curvaValor * Time.deltaTime;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * distanciaRay);
    }
    private System.Collections.IEnumerator EsperarYActivarMovimiento(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        puedeSeguir = true;
    }
}
