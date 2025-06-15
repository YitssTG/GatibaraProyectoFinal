using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
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

    [SerializeField] private Color highlightColor = Color.red;

    //private void OnEnable()
    //{
    //    Move.OnMoving += GetMovement;
    //}
    //private void OnDisable()
    //{
    //    Move.OnMoving -= GetMovement;
    //}
    void Update()
    {
        DoRaycast(transform.forward);
    }
    public void DoRaycast(Vector3 _direction)
    {
        RaycastHit hit;
        if(Physics.Raycast(_origin.position, _direction, out hit, _distance, _layermask))
        {
            Debug.DrawRay(_origin.position, _direction * hit.distance, colorColliding);
            PlayerInteractor.Instance?.SetCurrentHit(hit);
            Debug.Log("Obejto detectado");
            GameObject hitObj = hit.collider.gameObject;


            if (hitObj != lastHitObject)
            {
                if (lastBreakable != null)
                    lastBreakable.ResetColor();

                lastHitObject = hitObj;
                lastBreakable = hitObj.GetComponent<ObjectBreakable>();

                if (lastBreakable != null)
                    lastBreakable.Highlight(highlightColor);
            }

        }
        else
        {
            Debug.DrawRay(_origin.position, _direction * _distance, colorNotColliding);
            PlayerInteractor.Instance?.ClearCurrentHit();
            Debug.Log("No hay obejto detectado");
            if (lastBreakable != null)
            {
                lastBreakable.ResetColor();
                lastBreakable = null;
                lastHitObject = null;
            }


        }
    }
    //public void GetMovement(Vector2 movementImput)
    //{
    //    _direction = new Vector3(movementImput.x, 0f, movementImput.y);
    //}
}
