using DG.Tweening.Core.Easing;
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

    private void Update()
    {
        Vector3 direction = new Vector3(movementInput.x, 0f, movementInput.y);
        transform.Translate(direction * player.speed * Time.deltaTime);


        move.SetFloat("Speed", direction.magnitude);


        Vector3 rota = new Vector3(0f, reference.eulerAngles.y, 0f);
        transform.rotation = Quaternion.Euler(rota);

    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (!move.GetBool("isAttack"))
        {
            movementInput = context.ReadValue<Vector2>();
            OnMoving?.Invoke(movementInput);
        }
        else
        {
            OnMoving?.Invoke(Vector2.zero);
            Debug.Log("ZERO");
        }
    }
}