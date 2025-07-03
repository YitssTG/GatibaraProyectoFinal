using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    public static PlayerInteractor Instance;

    [Header("Raycast Properties")]
    [SerializeField] Transform _origin;
    [SerializeField] float _distance;
    [SerializeField] LayerMask _layermask;

    [Header("Draw Properties")]
    [SerializeField] Color debugColorHit = Color.green;
    [SerializeField] Color debugColorNotHit = Color.red;
    [SerializeField] Color highlightColor = Color.yellow;

    private ObjectBreakable lastBreakable;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void Update()
    {
        DoRaycast();
    }
    public void DoRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(_origin.position, _origin.forward, out hit, _distance, _layermask))
        {
            Debug.DrawRay(_origin.position, _origin.forward * hit.distance, debugColorHit);
            //Debug.Log("Obejto detectado");
            ObjectBreakable hiteableObject = hit.collider.GetComponent <ObjectBreakable>();
            // ==== INTERACTUABLES VISUALES ====
            if (hiteableObject != null)
            {
                if (lastBreakable != null)
                    lastBreakable.ResetColor();

                hiteableObject.Highlight(highlightColor);
                lastBreakable = hiteableObject;
            }
        }
        else
        {
            Debug.DrawRay(_origin.position, _origin.forward * _distance, debugColorNotHit);
            //Debug.Log("No hay obejto detectado");
            if (lastBreakable != null)
            {
                lastBreakable.ResetColor();
                lastBreakable = null;
            }
        }
    }
    public void OnBreak(InputAction.CallbackContext context)
    {
        if (!context.performed || lastBreakable == null) return;
            lastBreakable.Interact();
           
    }
}
