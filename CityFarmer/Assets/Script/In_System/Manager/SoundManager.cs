using System;
using UnityEngine;

public enum SoundType
{
    BGM,
    SFX,
    Voice,
    NumOfSoundType
}

public class SoundManager : MonoBehaviour
{
    private AudioSource[] _audioSources = new AudioSource[(int)SoundType.NumOfSoundType];

    public void Init()
    {
        for (int index = 0; index < _audioSources.Length; ++index)
        {
            GameObject gameObject = new GameObject(Enum.GetName(typeof(SoundType), index));
            gameObject.AddComponent<AudioSource>();
            gameObject.transform.parent = transform;

            _audioSources[index] = transform.GetChild(index).GetComponent<AudioSource>();

        }
        _audioSources[(int)SoundType.BGM].loop = true;
    }

    public void Clear(AudioSource[] audioSources)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
    }

    public void Play(SoundType sound, string fileName)
    {
        // TODO : 소리 설정에 따른 플레이 구현

        if (sound == SoundType.Voice)
        {

        }

    }

    public AudioClip GetAudioClip()
    {
        return null;
    }

    public void SetVolume(SoundType soundType, float value)
    {
        //가장 많이 사용하는 볼륨 변경 식. 원래는 기울기로 50이 아닌 20을 사용하나, 변화가 뚜렷하지 않아 50을 사용함.
        _audioSources[(int)soundType].volume = value;
    }
}
