using UnityEngine;
using DG.Tweening;

public class PopUpLooseDrop : MonoBehaviour
{
    public RectTransform popup;           // Asigna el contenedor hijo del popup (no toda la pantalla)
    public float startOffsetY = 1200f;    // Empieza desde arriba
    public float duration = 0.5f;

    private void OnEnable()
    {
        if (popup == null)
        {
            Debug.LogWarning("❌ Falta asignar el popup en el Inspector");
            return;
        }

        // Mueve fuera de pantalla (arriba)
        popup.anchoredPosition = new Vector2(0, startOffsetY);

        // Entra deslizándose a Y=0
        popup.DOAnchorPosY(0, duration)
             .SetEase(Ease.OutBack)
             .SetUpdate(true); // Funciona con Time.timeScale = 0
    }
}