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

        }
        else
        {
            Debug.DrawRay(_origin.position, _direction * _distance, colorNotColliding);
            PlayerInteractor.Instance?.ClearCurrentHit();

        }
    }
    //public void GetMovement(Vector2 movementImput)
    //{
    //    _direction = new Vector3(movementImput.x, 0f, movementImput.y);
    //}
}
