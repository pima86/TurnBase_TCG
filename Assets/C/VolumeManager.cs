using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    public Slider BGSlider;
    public Slider SFXSlider;
    public AudioMixer mixer;

    const string MIXER_MUSIC = "BGVolume";
    const string MIXER_SFX = "SFXVolume";

    void Start()
    {
        Invoke("Volume", 0.1f);
    }

    void Volume()
    {
        BGSlider.value = Player.Inst.playerdata.BGMSound;
        SFXSlider.value = Player.Inst.playerdata.SFXSound;
    }

    void Awake()
    {
        BGSlider.onValueChanged.AddListener(SetMusicVolume);
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    void SetMusicVolume(float value)
    {
        Player.Inst.playerdata.BGMSound = value;
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
    }
    void SetSFXVolume(float value)
    {
        Player.Inst.playerdata.SFXSound = value;
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
    }
}
