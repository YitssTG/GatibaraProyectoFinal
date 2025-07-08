using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using DG.Tweening;

public class HistoriaController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI")]
    [SerializeField] private Image imagenDisplay;
    [SerializeField] private Button botonSiguiente;
    [SerializeField] private RectTransform botonTransform;
    [SerializeField] private Image pantallaNegra;

    [Header("Contenido")]
    [SerializeField] private Sprite[] imagenes;
    [SerializeField] private TextMeshProUGUI[] textos;

    [Header("Configuración")]
    [SerializeField] private string escenaFinal = "NombreDeTuEscena";
    [SerializeField] private float duracionFade = 1f;
    [SerializeField] private float escalaHover = 1.1f;
    [SerializeField] private float duracionEscala = 0.2f;

    private int indiceActual = 0;
    private Vector3 escalaOriginal;
    private bool cargandoEscena = false;

    private void Start()
    {
        if (imagenes.Length != textos.Length)
        {
            Debug.LogError("El número de imágenes y textos debe coincidir.");
            botonSiguiente.interactable = false;
            return;
        }

        escalaOriginal = botonTransform.localScale;

        // Mostrar la primera imagen y texto
        imagenDisplay.sprite = imagenes[0];
        for (int i = 0; i < textos.Length; i++)
            textos[i].gameObject.SetActive(i == 0);

        botonSiguiente.onClick.AddListener(MostrarSiguiente);

        // Iniciar con pantalla negra que desaparece
        pantallaNegra.color = Color.black;
        pantallaNegra.gameObject.SetActive(true);
        pantallaNegra.DOFade(0, duracionFade).SetEase(Ease.OutQuad);
    }

    private void MostrarSiguiente()
    {
        if (cargandoEscena) return;

        indiceActual++;

        if (indiceActual >= imagenes.Length)
        {
            if (!string.IsNullOrEmpty(escenaFinal))
            {
                cargandoEscena = true;

                // Mostrar fade a negro antes de cambiar de escena
                pantallaNegra.gameObject.SetActive(true);
                pantallaNegra.DOFade(1, duracionFade).SetEase(Ease.InQuad).OnComplete(() =>
                {
                    SceneManager.LoadScene(escenaFinal);
                });
            }
            return;
        }

        imagenDisplay.sprite = imagenes[indiceActual];
        for (int i = 0; i < textos.Length; i++)
            textos[i].gameObject.SetActive(i == indiceActual);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter == botonSiguiente.gameObject)
        {
            botonTransform.DOScale(escalaOriginal * escalaHover, duracionEscala).SetEase(Ease.OutQuad);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerEnter == botonSiguiente.gameObject)
        {
            botonTransform.DOScale(escalaOriginal, duracionEscala).SetEase(Ease.OutQuad);
        }
    }
}