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
    [SerializeField] float rotationSpeed = 100f;

    public Animator move;
    private string lastTrigger = "";
    private void Update()
    {
        Vector3 forwardMovement = transform.forward * movementInput.y * player.currentSpeed * Time.deltaTime;
        transform.position += forwardMovement;

        float rotationAmount = movementInput.x * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotationAmount, 0);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        movementInput = movementInput.normalized;
        OnMoving?.Invoke(movementInput);

        if (movementInput.y != 0)
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