using UnityEngine;
using System.Collections;

public class ChestInteractable : MonoBehaviour, Interactable
{
    public float openTime = 2f;
    private Coroutine openRoutine;

    public void Interact()
    {
        // Puedes dejarlo vacío si se usa StartOpenProgress en lugar de Interact directamente
    }

    public void StartOpenProgress()
    {
        if (openRoutine == null)
        {
            openRoutine = StartCoroutine(OpenChest());
        }
    }

    public void CancelOpenProgress()
    {
        if (openRoutine != null)
        {
            StopCoroutine(openRoutine);
            openRoutine = null;
            Debug.Log("Apertura cancelada.");
        }
    }

    private IEnumerator OpenChest()
    {
        Debug.Log("Abriendo cofre...");
        yield return new WaitForSeconds(openTime);
        Debug.Log("¡Cofre abierto!");
        // Aquí puedes activar animaciones o recompensas
    }
}