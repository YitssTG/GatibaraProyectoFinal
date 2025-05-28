using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] public int speed;

    int level;
    int health;
    int damage;// va con el speedattack xd
    float speedattack;//pa cuando tenga raycast3d xdxdxdxd
    string type;// tal vez un scriptable con las tres armas
    public int spellnumber;

    private void Start()
    {
        level = 0;
        health = 10;
        damage = 1;
        speedattack = 2f;
        spellnumber = 0;
    }
    public void Die()
    {
        if(health <= 0)
        {
            //panel perdida(resetear el nivel, base del topo, mapa)
        }
    }
}
