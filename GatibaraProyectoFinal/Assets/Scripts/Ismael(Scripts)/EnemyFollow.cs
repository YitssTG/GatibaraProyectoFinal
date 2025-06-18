using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public int vidas = 3;
    public float fuerzaEmpuje = 6f;
    public float fuerzaVertical = 2f;
    private Rigidbody _rb;

    [SerializeField] public Transform jugador;
    public float velocidadBase = 3f;
    public float distanciaStop = 2f;
    public float distanciaRay = 2f;

    [SerializeField] private AnimationCurve curvaMovimiento;
    [SerializeField] private float tiempoAceleracion = 1.5f;

    private bool puedeSeguir = false;
    private bool enMovimiento = false;
    private float tiempoActual = 0f;

    private NavMeshAgent agent;
    public static event Func<Transform> OnGetPlayerPosition;
    private static Transform playerTransform;
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        //StartCoroutine(EsperarYActivarMovimiento(0.6f));
        ////transform.DOMoveX(transform.position.x + 2f, 1.5f)
        ////         .SetLoops(-1, LoopType.Yoyo)
        ////         .SetEase(Ease.InOutSine);
        ///if
        ///if
        if (playerTransform == null)
        {
            playerTransform = OnGetPlayerPosition?.Invoke();
        }
    }
    void Update()
    {
        //if (!puedeSeguir || jugador == null) return;

        //Vector3 direccion = (jugador.position - transform.position).normalized;
        //RaycastHit hit;
        //bool detectaJugador = Physics.Raycast(transform.position, direccion, out hit, distanciaRay);

        //if (detectaJugador && hit.collider.CompareTag("Player"))
        //{
        //    enMovimiento = false; 
        //    return;
        //}
        //float distancia = Vector3.Distance(transform.position, jugador.position);
        //if (distancia > distanciaStop)
        //{
        //    enMovimiento = true;
        //}
        //if (enMovimiento)
        //{
        //    tiempoActual += Time.deltaTime / tiempoAceleracion;
        //    float curvaValor = curvaMovimiento.Evaluate(tiempoActual);
        //    transform.position += direccion * velocidadBase * curvaValor * Time.deltaTime;
        //}
        Destination(playerTransform.position);
    }
    private void Destination(Vector3 destino)
    {
        agent.destination = destino;
    }
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawRay(transform.position, transform.forward * distanciaRay);
    //}
    //private System.Collections.IEnumerator EsperarYActivarMovimiento(float tiempo)
    //{
    //    yield return new WaitForSeconds(tiempo);
    //    puedeSeguir = true;
    //}
    public void RecibirAtaque(Vector3 direccion)
    {
        vidas--;
        Vector3 fuerzaTotal = direccion.normalized * fuerzaEmpuje + Vector3.up * fuerzaVertical;
        _rb.AddForce(fuerzaTotal, ForceMode.Impulse);

        if (vidas <= 0)
        {
            Destroy(gameObject);
        }
    }
}