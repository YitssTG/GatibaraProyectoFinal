using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.InputSystem;
public class CharacterController3D : MonoBehaviour
{
    [Header("Player Movement Properties")]
    [SerializeField] PlayerGatibara player;
    [SerializeField] Vector2 movementInput;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {

        Vector3 direction = new Vector3(movementInput.x, 0f, movementInput.y);
        transform.Translate(direction * player.speed * Time.deltaTime);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
}