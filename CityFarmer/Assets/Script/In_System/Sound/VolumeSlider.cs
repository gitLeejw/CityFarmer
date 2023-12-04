using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private TextMeshProUGUI _volumeAmount;
    private Slider _slider;
    private SoundType _soundType;

    private void Awake()
    {
        InitVolumeSliderSetting();
    }

    public void InitVolumeSliderSetting()
    {
        float defaultVolume = 1;

        _slider = GetComponent<Slider>();
        _slider.value = defaultVolume;
        _slider.minValue = 0.0001f;
        _slider.onValueChanged.RemoveAllListeners();
        _slider.onValueChanged.AddListener(ChangeVolume);
        //_soundType = soundType;
    }


    public void ChangeVolume(float value)
    {
        GameManager.SoundManager.SetVolume(_soundType, value);
        Debug.Log("¤±¤¤¤·");
    }
}
