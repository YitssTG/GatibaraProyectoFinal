using UnityEngine;
using System.Collections.Generic;

public class PlayerGatibara : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] public float baseSpeed;
    [SerializeField] public float currentSpeed;
    [SerializeField] HUD hud;
    [SerializeField] UnlockedAbilities unlockedAbilities;

    [SerializeField] PopUpController onWin;
    [SerializeField] ElementManager manager;

    public float vida;
    public int spellNumber;
    private void Start()
    {
        baseSpeed = 5f;
        currentSpeed = baseSpeed;
        spellNumber = 1;
        LoadStats();
    }
    private void OnEnable()
    {
        Health.OnHealthDestroy += CollectHealth;
        EnemyFollow.OnGetPlayerPosition += GetPlayer;

    }
    private void OnDisable()
    {
        Health.OnHealthDestroy -= CollectHealth;
        EnemyFollow.OnGetPlayerPosition -= GetPlayer;
    }
    public void LoadStats()
    {
        if (PlayerPrefs.HasKey("Coins"))
        {
            hud.puntos = PlayerPrefs.GetInt("Coins");
            hud.UpdatePoints();
        }
        if (PlayerPrefs.HasKey("SpellNumber"))
        {
            spellNumber = PlayerPrefs.GetInt("SpellNumber");
        }
        if (PlayerPrefs.HasKey("PositionX") && PlayerPrefs.HasKey("PositionY") && PlayerPrefs.HasKey("PositionZ"))
        {
            float x = PlayerPrefs.GetFloat("PositionX");
            float y = PlayerPrefs.GetFloat("PositionY");
            float z = PlayerPrefs.GetFloat("PositionZ");
            transform.position = new Vector3(x, y, z);
        }
    }
    private Transform GetPlayer()
    {
        return transform;
    }
    public void CollectHealth()
    {
        Debug.Log("Corazon Recogido");
    }
    public void Heal(int healQuantity)
    {
        vida += healQuantity;
        if(vida > hud.GetVidaMax())
        {
            vida = hud.GetVidaMax();
        }
        hud.UpdateLifeBar();
    }
    //public void RecibirDano(int cantidad)
    //{
    //    vida -= cantidad;
    //    hud.UpdateLifeBar();
    //    if(vida <= 0)
    //    {

    //    }
    //}
    public List<ElementType> GetElementTypes()
    {
        return manager.GetTypes();
    }
    public void IncreaseSpeed(int stacks)
    {
        currentSpeed = baseSpeed + stacks;
    }
    public void ResetEffect()
    {
        currentSpeed = baseSpeed;
        Debug.Log("Efectos reseteados");
    }
    public void SetGatibaraLevel(int newLevel)
    {
        spellNumber = newLevel;
        manager.UpdateSlots();
        Debug.Log("Player puede usar " + newLevel + " habilidades a la vez");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Win"))
        {
            onWin.ShowWinPopUp();
        }
    }
}