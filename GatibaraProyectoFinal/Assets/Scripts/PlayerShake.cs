using UnityEngine;

public class PlayerShake : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float magnitude;
    private float shakeTime;

    void Start()
    {
        duration = 0.2f;
        magnitude = 0.001f;
    }

    void Update()
    {
        if (shakeTime > 0)
        {
            float setX = Mathf.Sin(Time.time * 50f) * magnitude;
            float setY = Mathf.Cos(Time.time * 60f) * magnitude;
            transform.localPosition = transform.localPosition + new Vector3(setX, setY, 0f);
            shakeTime -= Time.deltaTime;
        }
        else
        {
            transform.localPosition = transform.localPosition;
        }
    }
    public void Shake()
    {
        shakeTime = duration;
    }
}
