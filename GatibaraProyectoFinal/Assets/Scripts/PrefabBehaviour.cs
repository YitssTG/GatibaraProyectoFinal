using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PrefabBehaviour : MonoBehaviour
{
    private void OnEnable()
    {
        SlotObject.OnUpdateTransform += UpdatePosition;
        SlotObject.OnDestroy += Destroy;
    }
    private void OnDisable()
    {
        SlotObject.OnUpdateTransform -= UpdatePosition;
        SlotObject.OnDestroy -= Destroy;
    }
    public void UpdatePosition(Vector3 position)
    {
        this.transform.position = position;
    }
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
