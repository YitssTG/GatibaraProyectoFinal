using DG.Tweening;
using UnityEngine;

public class ButtonDinamic : MonoBehaviour
{
    public RectTransform[] botones; 
    public float delayEntreBotones = 0.1f; 

    void Start()
    {
        for (int i = 0; i < botones.Length; i++)
        {
            RectTransform boton = botones[i];

            Vector2 posFinal = boton.anchoredPosition;

            boton.anchoredPosition = new Vector2(posFinal.x, posFinal.y + 1000f);

            float delay = i * delayEntreBotones;

            boton.DOAnchorPosY(posFinal.y, 0.6f)
                 .SetDelay(delay)
                 .SetEase(Ease.OutBounce);
        }
    }
}
