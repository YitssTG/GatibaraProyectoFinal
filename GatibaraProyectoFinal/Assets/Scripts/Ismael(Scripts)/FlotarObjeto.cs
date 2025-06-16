using UnityEngine;

public class FlotarObjeto : MonoBehaviour
{
    public float velocidad = 2f;    
    public float altura = 0.5f;      

    private Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * velocidad) * altura;
        transform.position = new Vector3(posicionInicial.x, posicionInicial.y + y, posicionInicial.z);
    }
}
