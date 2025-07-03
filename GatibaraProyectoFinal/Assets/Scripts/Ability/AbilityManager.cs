using System.Collections;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager instance;
    [SerializeField] private AbilityCaster abilityCaster;
    [SerializeField] private ElementManager elementManager;
    [SerializeField] private PlayerAttackCollider playerAttackCollider;
    [SerializeField] private ElementAbilityManager elementAbilityManager;

    //ST
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void TryCastOrAttack()
    {
        var combination = abilityCaster.CastAbility(elementManager.GetTypes()); //combinacion de elementos
        if (combination != null && !abilityCaster.IsOnCooldown(combination))
        {
            abilityCaster.StartCooldown(combination);
            StartCoroutine(AbilityAttack(combination));
            Debug.Log("No está en cooldown");
        }
        else
        {
            StartCoroutine(playerAttackCollider.PerformAttackCoroutine());
            Debug.Log("Está en cooldown");
        }
    }
    public IEnumerator AbilityAttack(CombinationData combination) //pide combinacion, da efecto
    {
        yield return playerAttackCollider.PerformAttackCoroutine();
        elementAbilityManager.ApplyAbilityEffect(combination);
    }
}