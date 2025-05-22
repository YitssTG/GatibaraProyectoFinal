using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.InputSystem;
public class CharacterController3D : MonoBehaviour
{
    [Header("Player Movement Properties")]
    [SerializeField] private float speed;
    [SerializeField] Vector2 movementInput;

    [Header("Raycast Properties")]
    [SerializeField] private Transform _origin;
    [SerializeField] private Vector3 _direction = Vector3.down;
    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _layerInteraction;

    [Header("DrawRay Properties")]
    [SerializeField] private Color OnCollisionRay = Color.green;
    [SerializeField] private Color OnNotCollisionRay = Color.white;

    private Rigidbody _rigidbody;
    private bool isMoving;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {

        Vector3 direction = new Vector3(movementInput.x, 0f, movementInput.y);
        transform.Translate(direction * speed * Time.deltaTime);

        isMoving = direction.magnitude > 0.1f;
        RaycastHit hit;
        if (Physics.Raycast(_origin.position, _direction, out hit, _distance, _layerInteraction))
        {
            Debug.DrawRay(_origin.position, _direction * _distance, OnCollisionRay);
        }
        else
        {
            Debug.DrawRay(_origin.position, _direction * _distance, OnNotCollisionRay);
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
}