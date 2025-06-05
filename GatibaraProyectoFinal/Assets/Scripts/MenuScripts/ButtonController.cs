using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public GameObject escena1;
    public GameObject escena2;
    public GameObject escena3;

    private void Start()
    {
        escena1.SetActive(true);
        escena2.SetActive(false);
        escena3.SetActive(false);
    }
    public void Play()
    {
        escena1.SetActive(false);
        escena2.SetActive(false);
        escena3.SetActive(true);
        Debug.Log("EscenaPlay");
    }
    public void Settings()
    {
        escena1.SetActive(false);
        escena2.SetActive(true);
        escena3.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Saliedno del juego... :(");
    }
    public void Back()
    {
        escena1.SetActive(true);
        escena2.SetActive(false);
        escena3.SetActive(false);
    }
    public void NewGame()
    {
        Debug.Log("Iniciando nueva partida");
    }
    public void Continue()
    {
        Debug.Log("Continuando partida");
    }

}
