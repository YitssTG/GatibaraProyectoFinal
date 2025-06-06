using UnityEngine;

[CreateAssetMenu(fileName = "Audio Data SO", menuName = "Scriptable Objects/Audio/Audio Data")]
public class AudioData : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    public AudioClip AudioClip => audioClip;
}
