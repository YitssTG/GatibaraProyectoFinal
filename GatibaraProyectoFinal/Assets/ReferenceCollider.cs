using UnityEngine;

public class ReferenceCollider : MonoBehaviour
{
    [SerializeField] private PlayerGatibara player;
    [SerializeField] private ElementEffectManager manager;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyFollow enemy = other.GetComponent<EnemyFollow>();
            manager.ApplyEffectsToEnemy(enemy, player.GetElementTypes());
        }
    }
}
