using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Player player;
    Rigidbody myrgbd;
    float horizontal;
    float transversal;
    private void Awake()
    {
        myrgbd = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        transversal = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        myrgbd.linearVelocity = new Vector3(horizontal * player.speed, 0, transversal * player.speed);
    }
}