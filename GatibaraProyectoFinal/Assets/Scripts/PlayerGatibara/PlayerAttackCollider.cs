using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackCollider : MonoBehaviour
{
    [SerializeField] private Move moveScript;
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider attackCollider;
    [SerializeField] private AbilityCaster abilityCaster;
    [SerializeField] private ElementManager elementManager;

    [SerializeField] private float attackDuration = 0.5f;
    [SerializeField] private float hitDelay = 0.2f;
    [SerializeField] private float hitActiveTime = 0.3f;

    [Header("Audio")]
    [SerializeField] private AudioData hitSoundData;

    private bool isAttacking = false;
    public bool IsAttacking => isAttacking;
    private void Start()
    {
        attackCollider.enabled = false;
    }
    public void OnLeftClick(InputAction.CallbackContext context)
    {
        
        if (context.performed && !isAttacking)
        {
            AbilityManager.instance.TryCastOrAttack();
            isAttacking = true; 
            StartCoroutine(PerformAttackCoroutine());
        }
    }
    public IEnumerator PerformAttackCoroutine()
    {
        if (moveScript != null)
        {
            moveScript.canMove = false;
            moveScript.ResetMovementInput();
        }       
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Run");
        animator.SetTrigger("isAttack");
        if (hitSoundData != null && hitSoundData.AudioClip != null)
        {
            AudioManager.TriggerFootstep(hitSoundData.AudioClip);
        }        
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
        if (moveScript != null)
        {
            moveScript.canMove = true;
        }
        isAttacking = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyFollow enemy = other.GetComponent<EnemyFollow>();
            if (enemy != null)
            {
                enemy.RecibirAtaque();

                if (hitSoundData != null && hitSoundData.AudioClip != null)
                {
                    AudioManager.TriggerFootstep(hitSoundData.AudioClip);
                }
            }
        }
    }
}