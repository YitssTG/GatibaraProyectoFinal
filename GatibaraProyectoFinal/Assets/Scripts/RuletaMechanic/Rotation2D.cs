using UnityEngine;

public class Rotation2D : MonoBehaviour
{
    [SerializeField] private bool antihorario = true;
    [SerializeField] float speed;
    [SerializeField] public bool spinning;

    float direction;

    private void Update()
    {
        if (spinning)
        {
            if (antihorario)
            {
                direction = 1f;
            }
            else
            {
                direction = -1f;
            }
            transform.Rotate(Vector3.forward, direction * speed * Time.deltaTime);
        }
    }
}
