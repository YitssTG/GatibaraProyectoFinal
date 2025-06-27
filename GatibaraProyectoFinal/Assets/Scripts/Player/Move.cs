using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class Move : MonoBehaviour
{
    [Header("Player Movement Properties")]
    [SerializeField] PlayerGatibara player;
    [SerializeField] Vector2 movementInput;
    public static event Action<Vector2> OnMoving;
    [SerializeField] Transform reference;

    public Animator move;
    private string lastTrigger = "";
    private PlayerRaycast playerRaycast;
    private void Awake()
    {
        playerRaycast = GetComponent<PlayerRaycast>();
    }
    private void Update()
    {
        Vector3 direction = new Vector3(movementInput.x, 0f, movementInput.y);
        transform.Translate(direction * player.currentSpeed * Time.deltaTime);




        Vector3 rota = new Vector3(0f, reference.eulerAngles.y, 0f);
        transform.rotation = Quaternion.Euler(rota);

    }
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        movementInput = movementInput.normalized;
        OnMoving?.Invoke(movementInput);
        playerRaycast.isAttacking = false;
        if (movementInput != Vector2.zero)
        {
            if (lastTrigger != "Run")
            {
                move.ResetTrigger("Idle");
                move.SetTrigger("Run");
                lastTrigger = "Run";
            }
        }
        else
        {
            if (lastTrigger != "Idle")
            {
                move.ResetTrigger("Run");
                move.SetTrigger("Idle");
                lastTrigger = "Idle";
            }
        }
    }

}