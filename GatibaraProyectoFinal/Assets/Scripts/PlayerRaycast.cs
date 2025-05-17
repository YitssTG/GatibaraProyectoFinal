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

    void Update()
    {
        _direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        DoRaycast(_direction);
    }
    public void DoRaycast(Vector3 _direction)
    {
        RaycastHit hit;
        if(Physics.Raycast(_origin.position, _direction, out hit, _distance, _layermask))
        {
            Debug.DrawRay(_origin.position, _direction * hit.distance, colorColliding);
        }
        else
        {
            Debug.DrawRay(_origin.position, _direction * _distance, colorNotColliding);
        }
    }
}
