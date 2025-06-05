using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class EnemyRaycast : MonoBehaviour
{
    float angle;
    [Header("Raycast Properties")]
    [SerializeField] float _rotationspeed;
    [SerializeField] Transform _origin;
    [SerializeField] Vector3 _direction;
    [SerializeField] float _ratio;
    [SerializeField] LayerMask _layermask;

    [Header("Draw Properties")]
    [SerializeField] Color colorColliding = Color.white;
    [SerializeField] Color colorNotColliding = Color.white;

    void Update()
    {
        angle = Time.time * _rotationspeed;
        _direction = new Vector3(Mathf.Sin(angle )* _ratio, 0f, Mathf.Cos(angle) * _ratio);
        DoRaycast(_direction);
    }
    public void DoRaycast(Vector3 _direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(_origin.position, _direction, out hit, _ratio, _layermask))
        {
            Debug.DrawRay(_origin.position, _direction * hit.distance, colorColliding);
        }
        else
        {
            Debug.DrawRay(_origin.position, _direction * _ratio, colorNotColliding);
        }
    }
}
