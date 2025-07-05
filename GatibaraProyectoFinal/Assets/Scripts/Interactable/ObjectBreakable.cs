using UnityEngine;

public class ObjectBreakable : MonoBehaviour, Interactable
{
    public bool isInteract;

    [Header("Monedas")]
    public GameObject monedaPrefab;
    public int maxMonedas = 5;
    public float radioSpawn = 1f;

    [Header("Durabilidad")]
    public int vida = 2; // Vida de la caja

    private Renderer _renderer;
    private Color _originalColor;

    private void Start()
    {
        isInteract = true;
        _renderer = GetComponent<Renderer>();
        _originalColor = _renderer.material.color;
    }

    public void Interact()
    {
        if (!isInteract) return;

        vida--;

        if (vida <= 0)
        {
            Debug.Log("Objeto destruido");
            SoltarMonedas();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("¡Golpe recibido! Vida restante: " + vida);
            // Puedes agregar una animación de daño o cambio de color aquí si deseas.
        }
    }

    void SoltarMonedas()
    {
        int chance = Random.Range(0, 100);
        int cantidad = 0;

        if (chance < 40)
        {
            cantidad = 0;
            Debug.Log("Rango 0-39: No se soltó ninguna moneda.");
        }
        else if (chance < 70)
        {
            cantidad = 1;
            Debug.Log("Rango 40-69: Se soltó 1 moneda.");
        }
        else if (chance < 90)
        {
            cantidad = 2;
            Debug.Log("Rango 70-89: Se soltaron 2 monedas.");
        }
        else
        {
            cantidad = Random.Range(3, maxMonedas + 1);
            Debug.Log("Rango 90-99: Se soltaron " + cantidad + " monedas.");
        }

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
                    UnityEngine.Random.Range(-1f, 1f),
                    0,
                    UnityEngine.Random.Range(-1f, 1f)
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