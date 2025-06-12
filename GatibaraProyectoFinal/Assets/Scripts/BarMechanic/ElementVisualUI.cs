using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using static ElementData;

public class ElementVisualUI : MonoBehaviour
{
    [SerializeField] private List<Image> elementImage;
    [SerializeField] private float selectDuration;
    private int currentIndex;
    private Coroutine coroutine;
    //public int CurrentIndex
    //{
    //    get
    //    {
    //        return currentIndex;
    //    }
    //}
    private void Start()
    {
        currentIndex = 0;
        selectDuration = 0.5f;
    }
    public void StartSpinning()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(SpinningElements());
    }
    public void StopSpinning()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }
    IEnumerator SpinningElements()
    {
        while (true)
        {
            foreach(var image in elementImage)
            {
                image.color = new Color(1f, 1f, 1f, 0.3f);
            }
            Image current = elementImage[currentIndex];
            current.DOFade(1f, selectDuration * 0.5f).SetLoops(2,LoopType.Yoyo);
            current.GetComponent<RectTransform>().DOScale(new Vector3(1.5f, 1.5f, 1f), selectDuration * 0.5f).SetLoops(2, LoopType.Yoyo);
            yield return new WaitForSeconds(selectDuration);
            currentIndex = (currentIndex +1) % elementImage.Count;
        }
    }
    public ElementType GetCurrentElementType()
    {
        string name = elementImage[currentIndex].name;
        switch (name)
        {
            case "Fire":
                return ElementData.ElementType.Fire;
            case "Water":
                return ElementData.ElementType.Water;
            case "Wind":
                return ElementData.ElementType.Wind;
            case "Earth":
                return ElementData.ElementType.Earth;
            default:
                return ElementData.ElementType.Empty;
        }
    }
}
