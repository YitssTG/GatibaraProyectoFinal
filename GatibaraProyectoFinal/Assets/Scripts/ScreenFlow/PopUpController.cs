using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class PopUpController : MonoBehaviour
{
    public static PopUpController instance;

    public GameObject popUpPuase;
    public GameObject popUpSelectorPause;
    public GameObject popUpSliderSounds;
    public GameObject popUpLoose;
    public GameObject popUpWin;
    [SerializeField] private SelectorController barController;

    public static event Action OnWin;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        popUpPuase.SetActive(false);
        popUpLoose.SetActive(false);
        popUpWin.SetActive(false);
        popUpSelectorPause.SetActive(false);
        popUpSliderSounds.SetActive(false);

    }
    public void OnPopUpActive()
    {

        popUpPuase.SetActive(true);
        popUpSelectorPause.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        barController.isPaused = true;
    }
    public void OnBackPress()
    {
        popUpPuase.SetActive(false);
        Time.timeScale = 1f;
        barController.isPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
    public void OnAudioPress()
    {
        popUpSliderSounds.SetActive(true);
    }
    public void OnPressClose()
    {
        popUpSliderSounds.SetActive(false);
        popUpSelectorPause.SetActive(true);
    }
    public void ShowWinPopUp()
    {
        OnWin?.Invoke();
        popUpWin.SetActive(true);
        Time.timeScale = 0f;
        barController.isPaused = true;
    }
    public void ShowLosePopUp()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        popUpLoose.SetActive(true);

        Time.timeScale = 0f;
        barController.isPaused = true;
    }
    public void ReiniciarNivel()
    {
        Time.timeScale = 1f;
        barController.isPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResetPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        ReiniciarNivel();
    }
    //public bool IsInventoryActive()
    //{
    //    return inventory;
    //}
}