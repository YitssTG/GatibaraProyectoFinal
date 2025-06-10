using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGatibara : MonoBehaviour
{


    [Header("Player Movement")]
    [SerializeField] public float speed;

    int level;
    int health;

    int coins;
    int damage;// va con el speedattack xd
    float speedattack;//pa cuando tenga raycast3d xdxdxdxd
    string type;// tal vez un scriptable con las tres armas
    public int spellnumber;

    private void Start()
    {
        level = 1;
        health = 10;
        damage = 1;
        speedattack = 2f;
        spellnumber = 1;
    }

    private void OnEnable()
    {
        Coin.OnCoinsCollection += CollectCoins;
    }
    private void OnDisable()
    {
        Coin.OnCoinsCollection -= CollectCoins;
    }
    public void Die()
    {
        if(health <= 0)
        {
            //panel perdida(resetear el nivel, base del topo, mapa)
        }
    }

    public void CollectCoins()
    {
        coins = coins + 5;
    }
}
