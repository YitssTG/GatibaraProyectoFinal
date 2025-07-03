using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("Pantallas")]
    [SerializeField] private GameObject titleScreenUI;
    [SerializeField] private GameObject mainMenuUI;

    [Header("Texto 'Press to Start'")]
    [SerializeField] private Image pressToStartText;
    [SerializeField] private float fadeDuration = 1.2f;
    [SerializeField] private float scaleDuration = 1.4f;
    [SerializeField] private float scaleFactor = 1.05f;

    [Header("Opciones Popup")]
    [SerializeField] private PopUpOptionsDrop popupOptions;

    [Header("Nombre de Escena")]
    [SerializeField] private string gameSceneName = "GameScene";

    private GameObject currentScreen;
    private bool isTransitioning = false;

    private bool hasStarted = false;
    private Tween fadeTween;
    private Tween scaleTween;

    void Start()
    {
        titleScreenUI.SetActive(false);
        mainMenuUI.SetActive(false);
        popupOptions.popup.gameObject.SetActive(false);
        ShowOnly(titleScreenUI);
        currentScreen = titleScreenUI;

        AnimatePressToStart();
    }

    void Update()
    {
        if (!hasStarted && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            hasStarted = true;
            StopPressToStartAnimation();
            ShowOnly(mainMenuUI);
        }
    }

    public void OnNewGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void OnLoadGame()
    {
        Debug.Log("Load Game aún no implementado.");
    }

    public void OnOptions()
    {
        popupOptions.ShowPopup();
    }

    public void OnClosePopup()
    {
        popupOptions.HidePopup();
    }

    public void OnExitToTitle()
    {
        Application.Quit();
        Debug.Log("Saliste del juego... :(");
    }

    private void ShowOnly(GameObject screenToShow)
    {
        if (isTransitioning || currentScreen == screenToShow) return;
        StartCoroutine(TransitionScreens(currentScreen, screenToShow));
    }
    private IEnumerator TransitionScreens(GameObject from, GameObject to)
    {
        isTransitioning = true;
        float duration = 0.5f;
        float offsetX = 2000f;

        if (from != null)
        {
            RectTransform fromRect = from.GetComponent<RectTransform>();
            if (fromRect != null)
            {
                fromRect.DOLocalMoveX(-offsetX, duration).SetEase(Ease.InBack);
                yield return new WaitForSeconds(duration);
            }

            from.SetActive(false);
        }
        if (to != null)
        {
            to.SetActive(true);
            RectTransform toRect = to.GetComponent<RectTransform>();
            if (toRect != null)
            {
                toRect.anchoredPosition = new Vector2(offsetX, toRect.anchoredPosition.y);
                toRect.DOAnchorPosX(0, duration).SetEase(Ease.OutBack);
            }
        }
        currentScreen = to;
        isTransitioning = false;
        if (popupOptions != null && popupOptions.popup != null)
        {
            popupOptions.popup.gameObject.SetActive(false);
        }
    }
    private void AnimatePressToStart()
    {
        if (pressToStartText == null) return;

        pressToStartText.color = new Color(
            pressToStartText.color.r,
            pressToStartText.color.g,
            pressToStartText.color.b,
            1f
        );

        fadeTween = pressToStartText.DOFade(0.2f, fadeDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);

        scaleTween = pressToStartText.transform.DOScale(Vector3.one * scaleFactor, scaleDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
    private void StopPressToStartAnimation()
    {
        fadeTween?.Kill();
        scaleTween?.Kill();

        pressToStartText.transform.localScale = Vector3.one;
        pressToStartText.color = new Color(
            pressToStartText.color.r,
            pressToStartText.color.g,
            pressToStartText.color.b,
            1f
        );
    }
}