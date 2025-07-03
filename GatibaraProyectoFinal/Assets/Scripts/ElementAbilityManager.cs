using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ElementAbilityManager : MonoBehaviour
{
    [SerializeField] private PlayerGatibara player;
    public void ApplyAbilityEffect(CombinationData combination)
    {
        switch (combination.combinationKey)
        {
            case "Earth+Earth":
                //this
                break;
            case "Fire+Earth":
                //this
                break;
            case "Fire+Fire":
                ApplyFireFireAbility(5f);
                break;
            case "Fire+Wind":
                //this
                break;
            case "Earth+Water":
                //this
                break;
            case "Water+Water":
                break;
            case "Water+Fire":
                break;
            case "Water+Earth":
                break;
            case "Wind+Fire":
                break;
            case "Earth+Wind":
                //this
                break;
            case "Wind+Water":
                break;
            case "Wind+Wind":
                break;
            default:
                Debug.Log("No existe esa combinación.");
                break;
        }
    }
    public void ApplyFireFireAbility(float radius)//xdxdxddddxdddxdxdxdxdxdxdxdxd
    {
        Collider[] hits = Physics.OverlapSphere(player.transform.position, radius, LayerMask.GetMask("Enemy"));
        var sortedHits = hits.Select(h => new { enemy = h.GetComponent<EnemyFollow>(), dist = Vector3.Distance(player.transform.position, h.transform.position) }).Where(x => x.enemy != null).OrderBy(x => x.dist).ToList();
        //xd
        if (sortedHits.Count > 0)
        {
            sortedHits[0].enemy.RecibirAtaque();
            Debug.Log($"Fire+Fire habilidad golpea a: {sortedHits[0].enemy.name}");
        }
        else
        {
            Debug.Log("No hay enemigos en rango para Fire+Fire.");
        }
    }
}
