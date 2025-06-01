using UnityEngine;

public class ObjectBreakable : MonoBehaviour, Interactable
{
    public void Interact()
    {
        Debug.Log("Objeto destruido");
        Destroy(this.gameObject);
    }
}
