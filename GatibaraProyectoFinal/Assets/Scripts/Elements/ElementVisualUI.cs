using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static ElementData;

public class ElementVisualUI : MonoBehaviour
{
    [Header("Elementos visuales")]
    [SerializeField] private List<Image> elementImages;
    [SerializeField] private List<ElementType> elementTypes;
    public void ShowElement(ElementType type)
    {
        for (int i = 0; i < elementImages.Count; i++)
        {
            var img = elementImages[i];
            img.color = new Color(1f, 1f, 1f, 0.3f);
            img.transform.localScale = Vector3.one;

            if (elementTypes[i] == type)
            {
                img.DOFade(1f, 0.25f).SetLoops(2, LoopType.Yoyo);
                img.rectTransform.DOScale(new Vector3(1.5f, 1.5f, 1f), 0.25f).SetLoops(2, LoopType.Yoyo);
            }
        }
    }
}
