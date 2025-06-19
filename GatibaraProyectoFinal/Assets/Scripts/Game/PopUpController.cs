using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PopUpController : MonoBehaviour
{
    public GameObject popUpPuase;
    public GameObject popUpLoose;
    public GameObject popUpWin;
    public GameObject popUpInventory;
    bool inventory;
    private void Start()
    {
        inventory = false;
        popUpPuase.SetActive(false);
        popUpLoose.SetActive(false);
        popUpWin.SetActive(false);
        popUpInventory.SetActive(inventory);
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
    public void OnIPressButton(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            popUpInventory.SetActive(!inventory);
            inventory = !inventory;
        }
    }
}
