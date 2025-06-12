using UnityEngine;

public class PopUpController : MonoBehaviour
{
    public GameObject popUpPuase;

    private void Start()
    {
        popUpPuase.SetActive(false);

    }
    public void OnPopUpActive()
    {
        popUpPuase.SetActive(true);

    }
    public void OnBackPress()
    {
        popUpPuase.SetActive(false);
        
    }
}
