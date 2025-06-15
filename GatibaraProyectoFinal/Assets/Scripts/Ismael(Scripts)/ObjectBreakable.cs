using UnityEngine;


public class ObjectBreakable : MonoBehaviour, Interactable
{
    public bool isInteract;
    private Renderer _renderer;
    private Color _originalColor;

    private void Start()
    {
        isInteract = true;
        _renderer = GetComponent<Renderer>();
        _originalColor = _renderer.material.color;
    }

    public void Interact()
    {
        Debug.Log("Objeto destruido");
        Destroy(this.gameObject);
    }
    public void Highlight(Color color)
    {
        if (_renderer != null)
            _renderer.material.color = color;
    }

    public void ResetColor()
    {
        if (_renderer != null)
            _renderer.material.color = _originalColor;
    }
}
