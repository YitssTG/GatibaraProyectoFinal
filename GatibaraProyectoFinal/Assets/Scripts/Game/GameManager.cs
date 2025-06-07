using UnityEngine;
using System.Collections.Generic;
using Unity.Mathematics;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Data Structure")]
    private List<GenerateEnemy> pointGenerateEnemy=new List<GenerateEnemy>();

    [Header("Enemy Generation Data")]
    private Transform player;
    private float minDistance;
    private float currentDistance;
    private int indexListPoint;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AddpointGenerateEnemy(GenerateEnemy generate)
    {
        pointGenerateEnemy.Add(generate);
    }
    private void Update()
    {
        UpdatePointGereateEnemy();
    }
    private void UpdatePointGereateEnemy()
    {
        if (player == null || pointGenerateEnemy.Count == 0) return;
        minDistance = math.INFINITY;
        for (int i = 0; i < pointGenerateEnemy.Count; ++i)
        {
            currentDistance = Vector3.Distance(player.position, pointGenerateEnemy[i].gameObject.transform.position);
            pointGenerateEnemy[i].enabled = false;
            if (minDistance > currentDistance )
            {
                minDistance = currentDistance;
                indexListPoint = i;
            }
        }
        pointGenerateEnemy[indexListPoint].enabled = true;
    }
    public void SetPlayerTransform(Transform transform_Player)
    {
        player = transform_Player;
    }
}
