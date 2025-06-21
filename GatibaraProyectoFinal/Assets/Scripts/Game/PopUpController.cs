using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.InputSystem.XR;

public class PopUpController : MonoBehaviour
{
    public GameObject popUpPuase;
    public GameObject popUpSelectorPause;
    public GameObject popUpSliderSounds;
    public GameObject popUpLoose;
    public GameObject popUpWin;
    public GameObject popUpInventory;
    [SerializeField] private SelectorController barController;
    bool inventory;
    private void Start()
    {
        inventory = false;
        popUpPuase.SetActive(false);
        popUpLoose.SetActive(false);
        popUpWin.SetActive(false);
        popUpSelectorPause.SetActive(false);
        popUpSliderSounds.SetActive(false);
        popUpInventory.SetActive(inventory);
    }
    public void OnPopUpActive()
    {
        popUpPuase.SetActive(true);
        popUpSelectorPause.SetActive(true);
        Time.timeScale = 0f;
        barController.isPaused = true;
    }
    public void OnBackPress()
    {
        popUpPuase.SetActive(false);
        Time.timeScale = 1f;
        barController.isPaused = false;
    }
    public void OnAudioPress()
    {
        popUpSliderSounds.SetActive(true);
        popUpSelectorPause.SetActive(false);
    }
    public void OnPressClose()
    {
        popUpSliderSounds.SetActive(false);
        popUpSelectorPause.SetActive(true);
    }
    public void ShowWinPopUp()
    {
        popUpWin.SetActive(true);
        Time.timeScale = 0f;
        barController.isPaused = true;
    }
    public void ShowLosePopUp()
    {
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
    public void OnIPressButton(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            popUpInventory.SetActive(!inventory);
            inventory = !inventory;
        }
    }
    public void ResetPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        ReiniciarNivel();
    }
}
