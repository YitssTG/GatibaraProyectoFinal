using DG.Tweening;
using UnityEngine;

public class ObjectBreakable : MonoBehaviour, Interactable
{
    public bool isInteract;

    [Header("Audio")]
    [SerializeField] private AudioData hitSoundData;
    [SerializeField] private AudioData destroySoundData;

    [Header("FX Visuales")]
    [SerializeField] private GameObject hitParticlesPrefab; // Prefab de partícula al ser golpeado
    [SerializeField] private GameObject destroyedPrefab;    // Trozos de la caja al destruirse
    [SerializeField] private Transform visualRoot;

    [Header("Monedas")]
    public GameObject monedaPrefab;
    public int maxMonedas = 5;
    public float radioSpawn = 1f;

    [Header("Durabilidad")]
    public int vida = 3;

    private Renderer _renderer;
    private Color _originalColor;

    private void Start()
    {
        isInteract = true;
        _renderer = GetComponent<Renderer>();

        if (_renderer != null)
        {
            _renderer.material = new Material(_renderer.material); // Clona el material para no afectar a otros
            _originalColor = _renderer.material.color;
        }
    }

    public void Interact()
    {
        if (!isInteract) return;

        vida--;

        if (vida <= 0)
        {
            Debug.Log("Objeto destruido");

            if (destroySoundData != null && destroySoundData.AudioClip != null)
                AudioManager.TriggerFootstep(destroySoundData.AudioClip);

            SoltarMonedas();
            MostrarDestruccion();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("¡Golpe recibido! Vida restante: " + vida);

            transform.DOShakePosition(0.2f, 0.15f, 10, 90, false, true);

            // Feedback visual: parpadeo rojo
            if (_renderer != null)
            {
                _renderer.material.DOColor(Color.red, 0.1f).SetLoops(2, LoopType.Yoyo);
            }

            if (hitSoundData != null && hitSoundData.AudioClip != null)
                AudioManager.TriggerFootstep(hitSoundData.AudioClip);

            if (hitParticlesPrefab != null)
            {
                GameObject fx = Instantiate(hitParticlesPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
                Destroy(fx, 2f); // eliminar después de reproducirse
            }
        }
    }

    private void MostrarDestruccion()
    {
        if (visualRoot != null)
            visualRoot.gameObject.SetActive(false);

        if (destroyedPrefab != null)
        {
            Vector3 spawnPosition = transform.position + Vector3.up * 0.5f;

            GameObject rotos = Instantiate(destroyedPrefab, spawnPosition, transform.rotation);

            foreach (Rigidbody rb in rotos.GetComponentsInChildren<Rigidbody>())
            {
                Transform t = rb.transform;
                t.localScale = Vector3.zero;
                t.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
                Vector3 fuerza = Random.insideUnitSphere * 4f + Vector3.up * 6f;
                rb.AddForce(fuerza, ForceMode.Impulse);
                rb.AddTorque(Random.insideUnitSphere * 8f, ForceMode.Impulse);
            }
            Destroy(rotos, 3f);
        }
    }

    void SoltarMonedas()
    {
        int chance = Random.Range(0, 100);
        int cantidad = 0;

        if (chance < 40) cantidad = 0;
        else if (chance < 70) cantidad = 1;
        else if (chance < 90) cantidad = 2;
        else cantidad = Random.Range(3, maxMonedas + 1);

        for (int i = 0; i < cantidad; i++)
        {
            Vector2 offset = Random.insideUnitCircle * radioSpawn;
            Vector3 posicion = new Vector3(
                transform.position.x + offset.x,
                transform.position.y + 1f,
                transform.position.z + offset.y
            );

            GameObject moneda = Instantiate(monedaPrefab, posicion, Quaternion.identity);

            Coin coinScript = moneda.GetComponent<Coin>();
            if (coinScript != null)
            {
                Vector3 offsetDestino = new Vector3(
                    Random.Range(-1f, 1f),
                    0,
                    Random.Range(-1f, 1f)
                );
                Vector3 destinoFinal = posicion + offsetDestino;
                coinScript.IniciarSalto(destinoFinal);
            }
        }
    }

    public void Highlight(Color color)
    {
        if (_renderer != null)
            _renderer.material.color = color;
    }

    public void ResetColor()
    {
        if (_renderer != null)
            _renderer.material.color = _originalColor;
    }
}