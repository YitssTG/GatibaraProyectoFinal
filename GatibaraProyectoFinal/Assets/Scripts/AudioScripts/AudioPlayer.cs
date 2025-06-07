using UnityEngine;
using UnityEngine.Audio;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private ChannelPlayer musicPlayer;
    [SerializeField] private ChannelPlayer sfxPlayer;

    private void OnEnable()
    {
        InteractableObjects.OnCollisionMusic += PlayPlayer;
        InteractableObjects.OnCollisionStopMusic += StopPlayer;
        InteractableObjects.OnExitCollision += PlayExit;
    }
    private void OnDisable()
    {
        InteractableObjects.OnCollisionMusic -= PlayPlayer;
        InteractableObjects.OnCollisionStopMusic -= StopPlayer;
        InteractableObjects.OnExitCollision -= PlayExit;

    }
    private void PlayPlayer(AudioMixerGroup currentGroup, AudioClip currentAudioClip)
    {
        if (currentGroup == musicPlayer.PlayerChannel)
        {
            musicPlayer.PlayClip(currentAudioClip, true);
        }
        else
        {
            sfxPlayer.PlayClip(currentAudioClip, false);
        }
    }
    private void StopPlayer(AudioMixerGroup currentGroup)
    {
        if (currentGroup == musicPlayer.PlayerChannel)
        {
            musicPlayer.StopClip();
        }
        else
        {
            sfxPlayer.StopClip();
        }
    }
    private void PlayExit(AudioMixerGroup currentGroup, AudioClip exitClip)
    {
        if (currentGroup == sfxPlayer.PlayerChannel)
        {
            sfxPlayer.ExitClip(exitClip);
        }
    }
}
