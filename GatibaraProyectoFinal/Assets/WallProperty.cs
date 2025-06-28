using UnityEngine;

public class WallProperty : MonoBehaviour
{
    [SerializeField] private GameObject[] walls;
    [SerializeField] private int requiredKills;
    [SerializeField] private PuntoSpawn spawn;
    private void Start()
    {
        SetWallsActive(false);
    }
    private void Update()
    {
        if(spawn.GetState())
        {
            SetWallsActive(true);
        }
        if(GameManager.instance.EnemyKilled >= requiredKills)
        {
            SetWallsActive(false);
        }
    }
    private void SetWallsActive(bool state)
    {
        for(int i = 0; i < walls.Length; i++)
        {
            walls[i].SetActive(state);
        }
    }

}
