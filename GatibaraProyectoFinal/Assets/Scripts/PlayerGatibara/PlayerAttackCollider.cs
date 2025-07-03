using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackCollider : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private BoxCollider attackCollider;
    [SerializeField] private AbilityCaster abilityCaster;
    [SerializeField] private ElementManager elementManager;
    [SerializeField] private float attackDuration = 0.5f;
    [SerializeField] private float hitDelay = 0.5f;
    [SerializeField] private float hitActiveTime = 0.5f;

    private bool isAttacking = false;
    //
    private void Start()
    {
        attackCollider.enabled = false;
    }
    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            AbilityManager.instance.TryCastOrAttack();
        }
    }
    public IEnumerator PerformAttackCoroutine()//parte de coroutine que hace q el golpe sea mas realista
    {
        isAttacking = true;
        animator.SetTrigger("isAttack");
        yield return new WaitForSeconds(hitDelay);
        attackCollider.enabled = true;
        yield return new WaitForSeconds(hitActiveTime);
        attackCollider.enabled = false;
        float remaining = attackDuration - (hitDelay + hitActiveTime);
        if (remaining > 0)
        {
            yield return new WaitForSeconds(remaining);
        }
        animator.SetTrigger("Idle");
        isAttacking = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyFollow enemy = other.GetComponent<EnemyFollow>();
            if(enemy != null)
            {
                enemy.RecibirAtaque();
            }
        }
    }// se relaciona con el enemigo
}