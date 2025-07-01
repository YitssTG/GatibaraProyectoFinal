using UnityEngine;

public class AbilityCooldown
{
    public CombinationData data;
    public float cooldownRemaining;

    public AbilityCooldown(CombinationData data, float cooldownRemaining)
    {
        this.data = data;
        this.cooldownRemaining = cooldownRemaining;
    }
    public void UpdateCooldown(float time)
    {
        cooldownRemaining = Mathf.Max(0, cooldownRemaining -  time);
    }
    public bool IsReady()
    {
        return cooldownRemaining <= 0;
    }
}