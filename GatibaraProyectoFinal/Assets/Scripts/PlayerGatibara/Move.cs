using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    [SerializeField] private AudioData pasoAudioData;
    [SerializeField] private float intervaloPasos = 0.5f;
    private float contadorPaso;

    [Header("Player Movement Properties")]
    [SerializeField] private PlayerGatibara player;
    [SerializeField] private PlayerAttackCollider attackScript;
    [SerializeField] private Transform reference;
    [SerializeField] private float rotationSpeed = 100f;

    public static event Action OnMoving;

    [SerializeField] private Animator move;

    private Vector2 movementInput;
    private Vector2 inputRaw;
    public bool canMove = true;
    private bool tutorial;
    private string lastTrigger = "";
    private void Start()
    {
        tutorial = true;
    }
    private void Update()
    {
        if (!canMove)
        {
            contadorPaso = intervaloPasos;
            movementInput = Vector2.zero;
            return;
        }
        movementInput = inputRaw;

        if (movementInput.sqrMagnitude > 0.01f)
        {
            if (tutorial)
            {
                OnMoving?.Invoke();
                tutorial = false;
            }
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
            contadorPaso = intervaloPasos;
        }
        UpdateAnimator(); 
    }
    public void ResetMovementInput()
    {
        movementInput = Vector2.zero;

        if (lastTrigger != "Idle")
        {
            move.ResetTrigger("Run");
            move.SetTrigger("Idle");
            lastTrigger = "Idle";
        }
    }
    private void UpdateAnimator()
    {
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
    public void OnMove(InputAction.CallbackContext context)
    {
        inputRaw = context.ReadValue<Vector2>().normalized;

        if (attackScript != null && attackScript.IsAttacking)
            return;
        movementInput = inputRaw;
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