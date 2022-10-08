using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] Slider _volumeSlider;
    AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _volumeSlider.value = _audioSource.volume;
    }

    public void DecreaseVolume()
    {
        if (_audioSource == null) { return; }
        if (_volumeSlider == null) { return; }

        _audioSource.volume = _volumeSlider.value;
    }
}
