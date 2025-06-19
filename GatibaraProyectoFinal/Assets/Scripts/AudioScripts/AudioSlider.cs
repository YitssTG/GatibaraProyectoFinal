using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class AudioSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text volumeText;
    [SerializeField] private AudioSettings audioSettingsData;


    private void OnEnable()
    {
        audioSettingsData.OnUpdateVolume += UpdateText;

        audioSettingsData.UpdateVolume(audioSettingsData.VolumeScaled);

        slider.onValueChanged.AddListener(audioSettingsData.UpdateVolume);
    }

    private void OnDisable()
    {
        audioSettingsData.OnUpdateVolume -= UpdateText;

        slider.onValueChanged.RemoveListener(audioSettingsData.UpdateVolume);
    }

    private void UpdateText(float value)
    {
        volumeText.text = (value * 100).ToString("000");

        slider.value = value;
    }
}
