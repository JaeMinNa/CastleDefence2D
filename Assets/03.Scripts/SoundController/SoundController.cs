using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    public Slider BGMSlider;
    public Slider SFXSlider;

    public void BGMControll()
    {
        float sound = BGMSlider.value;
        GameManager.I.DataManager.GameData.BGMVolume = sound;

        if (sound == -40f)	// -40¿œ ∂ß, ¿Ωæ«¿ª ≤®¡‹
        {
            _audioMixer.SetFloat("BGM", -80f);
        }
        else
        {
            _audioMixer.SetFloat("BGM", sound);
        }

        GameManager.I.DataManager.DataSave();
    }

    public void SFXControll()
    {
        float sound = SFXSlider.value;
        GameManager.I.DataManager.GameData.SFXVolume = sound;

        if (sound == -40f)	// -40¿œ ∂ß, ¿Ωæ«¿ª ≤®¡‹
        {
            _audioMixer.SetFloat("SFX", -80f);
        }
        else
        {
            _audioMixer.SetFloat("SFX", sound);
        }

        GameManager.I.DataManager.DataSave();
    }
}
