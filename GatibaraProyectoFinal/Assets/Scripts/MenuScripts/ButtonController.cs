using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ButtonController : MonoBehaviour
{
    public GameObject popupInicio;
    public GameObject popupAjustes; 
    public GameObject popupJugar;

    public RectTransform popupOptions; 
    public PopUpOptionsDrop popupAnimator; 

    private void Start()
    {
        popupInicio.SetActive(true);
        popupAjustes.SetActive(false);
        popupJugar.SetActive(false);
        popupOptions.gameObject.SetActive(false);
    }

    public void Play()
    {
        popupInicio.SetActive(false);
        popupAjustes.SetActive(false);
        popupJugar.SetActive(true);
        Debug.Log("Entrando a jugar");
    }

    public void Settings()
    {
        popupInicio.SetActive(false);
        popupAjustes.SetActive(true);
        popupJugar.SetActive(false);

        popupAnimator.ShowPopup(); // lanza el efecto de caída
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego... :(");
    }

    public void Back()
    {
        popupInicio.SetActive(true);
        popupAjustes.SetActive(false);
        popupJugar.SetActive(false);

        popupOptions.DOAnchorPosY(800f, 0.5f)
            .SetEase(Ease.InBack)
            .OnComplete(() => popupOptions.gameObject.SetActive(false));
    }

    public void NewGame(string newScene)
    {
        SceneManager.LoadScene(newScene);
        Debug.Log("Iniciando nueva partida");
    }

    public void Continue()
    {
        Debug.Log("Continuando partida");
    }
}