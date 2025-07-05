using UnityEngine;

public class FireFireAbility : MonoBehaviour
{
    private Vector3 myDirection;
    private float mySpeed;
    private Rigidbody myRigidBody;
    [SerializeField] private float damage;
    [SerializeField] private float time;
    public void Launch(Vector3 direction, float speed)
    {
        myDirection = direction;
        mySpeed = speed;
        myRigidBody = GetComponent<Rigidbody>();
        myRigidBody.linearVelocity = myDirection * mySpeed;
        Destroy(gameObject, time);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyFollow enemy = other.GetComponent<EnemyFollow>();
            if (enemy != null)
            {
                enemy.RecibirAtaque(5f);//recibe daño, pero puede ser otro tipo de daño
            }
            Destroy(gameObject);
        }
        else if(!other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
