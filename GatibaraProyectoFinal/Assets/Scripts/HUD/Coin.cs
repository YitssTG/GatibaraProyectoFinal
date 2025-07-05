using DG.Tweening;
using System;
using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{
    public static event Action<Vector3> OnCoinsCollection;

    [Header("Salto inicial")]
    public float duracionSalto = 0.6f;
    public float alturaSalto = 1.5f;

    [Header("Flotación después del salto")]
    public float velocidadFlotar = 2f;
    public float alturaFlotar = 0.5f;

    private Vector3 posicionFlotacionInicial;
    private bool puedeFlotar = false;

    [Header("Audio")]
    [SerializeField] private AudioData coinSoundData;

    void Start()
    {
        transform.DORotate(new Vector3(0, 360, 0), 1f, RotateMode.FastBeyond360)
                 .SetEase(Ease.Linear)
                 .SetLoops(-1, LoopType.Restart);

        posicionFlotacionInicial = transform.position;
        puedeFlotar = true;
    }

    void Update()
    {
        if (puedeFlotar)
        {
            float y = Mathf.Sin(Time.time * velocidadFlotar) * alturaFlotar;
            transform.position = new Vector3(
                posicionFlotacionInicial.x,
                posicionFlotacionInicial.y + y,
                posicionFlotacionInicial.z
            );
        }
    }

    public void IniciarSalto(Vector3 destino)
    {
        puedeFlotar = false;

        Vector3 inicio = transform.position;
        StartCoroutine(SaltoParabolico(inicio, destino, alturaSalto, duracionSalto));
    }

    IEnumerator SaltoParabolico(Vector3 inicio, Vector3 fin, float altura, float duracion)
    {
        float tiempo = 0f;
        while (tiempo < duracion)
        {
            float t = tiempo / duracion;
            Vector3 pos = Vector3.Lerp(inicio, fin, t);
            pos.y += Mathf.Sin(t * Mathf.PI) * altura;
            transform.position = pos;
            tiempo += Time.deltaTime;
            yield return null;
        }

        transform.position = fin;

        posicionFlotacionInicial = transform.position;
        puedeFlotar = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Moneda recogida por el jugador");

            if (coinSoundData != null && coinSoundData.AudioClip != null)
            {
                AudioManager.TriggerFootstep(coinSoundData.AudioClip);
            }

            OnCoinsCollection?.Invoke(transform.position);

            Destroy(gameObject);
        }
    }
}