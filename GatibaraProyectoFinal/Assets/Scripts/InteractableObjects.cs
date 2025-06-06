using System;
using UnityEngine;
using UnityEngine.Audio;

public class InteractableObjects : MonoBehaviour
{
    [SerializeField] private AudioData audioData;
    [SerializeField] private AudioSettings audioSettings;

    public static event Action<AudioMixerGroup, AudioClip> OnCollisionMusic;
    public static event Action<AudioMixerGroup> OnCollisionStopMusic;
    public static event Action<AudioMixerGroup, AudioClip> OnExitCollision;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnCollisionMusic?.Invoke(audioSettings.AudioMixerGroup, audioData.AudioClip);
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnCollisionStopMusic?.Invoke(audioSettings.AudioMixerGroup);

            OnExitCollision?.Invoke(audioSettings.AudioMixerGroup, audioData.AudioClip);
        }
    }
}
