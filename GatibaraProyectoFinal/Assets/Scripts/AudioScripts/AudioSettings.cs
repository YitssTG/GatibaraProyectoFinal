using System;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "Audio Settings SO", menuName = "Scriptable Objects/Game Systems/Audio/Mixer Data")]
public class AudioSettings : ScriptableObject
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioMixerGroup audioMixerGroup;
    [SerializeField] private string audioMixerKey;
    [SerializeField] private string audioKeySafe;
    [SerializeField, Range(0, 1)] private float volumeScaled = 1;
    [SerializeField, Range(-80, 20)] private float volumeDBs = 0;
    [SerializeField] private bool isMuted;

    public AudioMixerGroup AudioMixerGroup => audioMixerGroup;
    public float VolumeScaled => volumeScaled;

    public Action<float> OnUpdateVolume;

    private void OnEnable()
    {
        UpdateVolume(PlayerPrefs.GetFloat(audioKeySafe, 0.8f));
    }
    private void OnDisable()
    {
        PlayerPrefs.GetFloat(audioKeySafe, volumeScaled);
    }
    public void SaveDataFile()
    {
        PlayerPrefs.SetFloat(audioKeySafe, volumeScaled);
    }
    public void DeleteSafeData()
    {
        PlayerPrefs.DeleteKey(audioKeySafe);
    }
    public void UpdateVolume(float value)
    {
        volumeScaled = value;

        volumeDBs = ToDecibels(volumeScaled);

        audioMixer.SetFloat(audioMixerKey, volumeDBs);

        OnUpdateVolume?.Invoke(volumeScaled);
    }
    private float ToDecibels(float value)
    {
        return Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 20);
    }
}
