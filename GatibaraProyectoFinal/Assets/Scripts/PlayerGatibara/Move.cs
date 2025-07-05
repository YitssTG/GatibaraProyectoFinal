using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class Move : MonoBehaviour
{
    [SerializeField] private AudioData pasoAudioData;
    [SerializeField] private float intervaloPasos = 0.5f;
    private float contadorPaso;

    private float tiempoPaso;

    [Header("Player Movement Properties")]
    [SerializeField] PlayerGatibara player;
    [SerializeField] Vector2 movementInput;
    [SerializeField] Transform reference;
    [SerializeField] float rotationSpeed = 100f;

    public Animator move;
    private string lastTrigger = "";
    private void Update()
    {
        if (movementInput.sqrMagnitude > 0.01f)
        {
            // Movimiento
            Vector3 camForward = reference.forward;
            Vector3 camRight = reference.right;
            camForward.y = 0;
            camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDirection = camForward * movementInput.y + camRight * movementInput.x;
            moveDirection.Normalize();

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            transform.position += moveDirection * player.currentSpeed * Time.deltaTime;

            // Pasos
            contadorPaso += Time.deltaTime;
            if (contadorPaso >= intervaloPasos)
            {
                if (pasoAudioData != null && pasoAudioData.AudioClip != null)
                {
                    AudioManager.TriggerFootstep(pasoAudioData.AudioClip);
                }
                contadorPaso = 0f;
            }
        }
        else
        {
            // Si no se mueve, reinicia el contador
            contadorPaso = intervaloPasos;
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        movementInput = movementInput.normalized;

        if (movementInput.magnitude > 0.01f)
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