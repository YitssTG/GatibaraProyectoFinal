using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    public static PlayerInteractor Instance;
    private RaycastHit? currentHit;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void SetCurrentHit(RaycastHit hit)
    {
        currentHit = hit;
    }
    public void ClearCurrentHit()
    {
        currentHit = null;
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed || currentHit == null) return;

        var hit = currentHit.Value;
        var interactable = hit.collider.GetComponent<Interactable>();

        if (interactable != null)
        {
            interactable.Interact();
        }
    }
}
