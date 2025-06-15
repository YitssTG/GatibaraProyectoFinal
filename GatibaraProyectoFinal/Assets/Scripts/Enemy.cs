using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;
    private Transform target;
    public float fireDamage = 0f;
    public float attackSpeedPenalty = 0f;
    public float speedMovementPenalty = 0f;
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
    public void ApplyFireDamage(int stacks)
    {
        fireDamage = 5f * stacks;
    }
    public void ReduceAttackSpeed(int stacks)
    {
        attackSpeedPenalty = 0.1f * stacks;
    }
    public void ReduceMovementSpeed(int stacks)
    {
        speedMovementPenalty = 0.1f * stacks;
    }
    public void ResetDebuffs()
    {
        fireDamage = 0;
        attackSpeedPenalty = 0;
        speedMovementPenalty = 0;
        Debug.Log("DebuffRseted");
    }
}