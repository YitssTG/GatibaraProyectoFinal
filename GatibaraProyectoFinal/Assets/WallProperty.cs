using UnityEngine;

public class WallProperty : MonoBehaviour
{
    [SerializeField] private GameObject[] walls;
    [SerializeField] private int requiredKills;
    void Start()
    {
        ////SetWallsActive(true); esto debe ser cuando se activa el generateenemy
    }
    private void Update()
    {
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
