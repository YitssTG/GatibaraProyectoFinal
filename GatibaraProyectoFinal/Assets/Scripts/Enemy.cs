using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;
    private Transform target;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime);
        }
    }
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}