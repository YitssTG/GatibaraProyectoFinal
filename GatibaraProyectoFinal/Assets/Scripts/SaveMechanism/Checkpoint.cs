using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.InputSystem;

public class Checkpoint : MonoBehaviour
{
    private bool isInZone;
    [SerializeField] PlayerGatibara player;
    [SerializeField] HUD hud;
    [SerializeField] UnlockedAbilities unlockedAbilities;
    void Start()
    {
        isInZone = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInZone = true;
            Debug.Log("Estás en un checkpoint. Presiona E para guardar.");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInZone = false;
            Debug.Log("Saliste del checkpoint.");
        }
    }
    public void OnSaveInteractable(InputAction.CallbackContext context)
    {
        if(context.performed && isInZone)
        {
            SaveData();
        }
    }
    private void SaveData()
    {
        unlockedAbilities.SaveProgress();
        PlayerPrefs.SetInt("Coins", hud.puntos);
        PlayerPrefs.SetInt("SpellNumber", player.spellnumber);
        Vector3 position = player.transform.position;
        PlayerPrefs.SetFloat("PositionX", position.x);
        PlayerPrefs.SetFloat("PositionY", position.y);
        PlayerPrefs.SetFloat("PositionZ", position.z);
        PlayerPrefs.Save();
    }
}
