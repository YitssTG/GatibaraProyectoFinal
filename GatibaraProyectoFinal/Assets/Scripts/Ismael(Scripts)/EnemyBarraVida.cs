using UnityEngine;
using UnityEngine.UI;

public class EnemyBarraVida : MonoBehaviour
{
    public Image fillImage;
    private Transform camara;

    private void Start()
    {
        camara = Camera.main.transform;
    }

    private void LateUpdate()
    {
        if (camara == null) return;
        transform.forward = camara.forward;
    }

    public void SetVida(float actual, float max)
    {
        fillImage.fillAmount = actual / max;
    }
}
