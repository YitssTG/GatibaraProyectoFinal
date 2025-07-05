using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public event Action<int> OnDamageTaken;
    public event Action OnPlayerDied;
    public event Action OnHeartCollected;
    public event Action<Vector3> OnCoinCollected;

    public event Action<int> OnSpeedBuff;
    public event Action OnSpeedReset;
    public event Action<int> OnSpellLevelChanged;

    public event Action OnGameWon;
    public event Action OnGameLost;

    public event Action<int> OnEnemyKilled;
    public event Action OnAllEnemiesKilled;
    public event Action<Vector3> OnSpawnPointActivated;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void TriggerDamage(int amount) => OnDamageTaken?.Invoke(amount);
    public void TriggerPlayerDied() => OnPlayerDied?.Invoke();
    public void TriggerHeartCollected() => OnHeartCollected?.Invoke();
    public void TriggerCoinCollected(Vector3 pos) => OnCoinCollected?.Invoke(pos);
    public void TriggerSpeedBuff(int stacks) => OnSpeedBuff?.Invoke(stacks);
    public void TriggerSpeedReset() => OnSpeedReset?.Invoke();
    public void TriggerSpellLevel(int level) => OnSpellLevelChanged?.Invoke(level);
    public void TriggerGameWon() => OnGameWon?.Invoke();
    public void TriggerGameLost() => OnGameLost?.Invoke();
    public void TriggerEnemyKilled(int total) => OnEnemyKilled?.Invoke(total);
    public void TriggerAllEnemiesKilled() => OnAllEnemiesKilled?.Invoke();
    public void TriggerSpawnActivated(Vector3 pos) => OnSpawnPointActivated?.Invoke(pos);
}