using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRaycast : MonoBehaviour
{
    public Animator attack;

    [Header("Raycast Properties")]
    [SerializeField] Transform _origin;
    [SerializeField] Vector3 _direction;
    [SerializeField] float _distance;
    [SerializeField] LayerMask _layermask;

    [Header("Draw Properties")]
    [SerializeField] Color colorColliding = Color.white;
    [SerializeField] Color colorNotColliding = Color.white;

    private GameObject lastHitObject = null;
    private ObjectBreakable lastBreakable = null;

    private GameObject hitObject;

    [SerializeField] private Color highlightColor = Color.red;
    public bool isAttacking = false;

    void Update()
    {
        DoRaycast(transform.forward);
    }
    public void DoRaycast(Vector3 _direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(_origin.position, _direction, out hit, _distance, _layermask))
        {
            Debug.DrawRay(_origin.position, _direction * hit.distance, colorColliding);
            PlayerInteractor.Instance?.SetCurrentHit(hit);
            //Debug.Log("Obejto detectado");
            hitObject = hit.collider.gameObject;

            // ==== INTERACTUABLES VISUALES ====
            if (hitObject != lastHitObject)
            {
                if (lastBreakable != null)
                    lastBreakable.ResetColor();

                lastHitObject = hitObject;
                lastBreakable = hitObject.GetComponent<ObjectBreakable>();

                if (lastBreakable != null)
                    lastBreakable.Highlight(highlightColor);
            }
        }
        else
        {
            Debug.DrawRay(_origin.position, _direction * _distance, colorNotColliding);
            PlayerInteractor.Instance?.ClearCurrentHit();
            //Debug.Log("No hay obejto detectado");
            if (lastBreakable != null)
            {
                lastBreakable.ResetColor();
                lastBreakable = null;
                lastHitObject = null;
            }
        }
    }
    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.performed && !isAttacking)
        {
            attack.SetTrigger("isAttack");
            isAttacking = true;
            EnemyFollow enemigo = hitObject?.GetComponent<EnemyFollow>();
            if (enemigo != null)
            {
                enemigo.RecibirAtaque();
            }
        }
    }
    public void EndAttack()
    {
        attack.SetTrigger("Idle");
        isAttacking = false;
    }
}