using UnityEngine;
using UnityEngine.SceneManagement;

public class PopUpController : MonoBehaviour
{
    public GameObject popUpPuase;
    public GameObject popUpLoose;
    public GameObject popUpWin;

    private void Start()
    {
        popUpPuase.SetActive(false);
        popUpLoose.SetActive(false);
        popUpWin.SetActive(false);
    }
    public void OnPopUpActive()
    {
        popUpPuase.SetActive(true);
        Time.timeScale = 0f;
    }
    public void OnBackPress()
    {
        popUpPuase.SetActive(false);
        Time.timeScale = 1f;

    }
    public void ShowWinPopUp()
    {
        popUpWin.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ShowLosePopUp()
    {
        popUpLoose.SetActive(true);
        Time.timeScale = 0f; 
    }
    public void ReiniciarNivel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
