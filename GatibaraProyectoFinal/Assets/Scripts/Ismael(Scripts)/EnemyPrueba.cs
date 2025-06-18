//using UnityEngine;

//public class EnemyPrueba : MonoBehaviour
//{
//    public int vidas = 3;
//    public float fuerzaEmpuje = 6f;
//    public float fuerzaVertical = 2f;

//    private Rigidbody _rb;

//    void Awake()
//    {
//        _rb = GetComponent<Rigidbody>();
//    }

//    public void RecibirAtaque(Vector3 direccion)
//    {
//        vidas--;
//        Vector3 fuerzaTotal = direccion.normalized * fuerzaEmpuje + Vector3.up * fuerzaVertical;
//        _rb.AddForce(fuerzaTotal, ForceMode.Impulse);

//        if (vidas <= 0)
//        {
//            Destroy(gameObject);
//        }
//    }
//}
