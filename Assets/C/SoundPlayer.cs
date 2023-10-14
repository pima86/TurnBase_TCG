using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundPlayer : MonoBehaviour
{
    public static SoundPlayer Inst { get; private set; }

    const string MIXER_MUSIC = "BGVolume";
    const string MIXER_SFX = "SFXVolume";

    public AudioMixer mixer;
    public AudioSource bgSound;
    public AudioSource sub_bgSound;
    public AudioClip[] bglist;

    private void Awake()
    {
        if (Inst == null)
        {
            Inst = this;
            DontDestroyOnLoad(Inst);
            DontDestroyOnLoad(sub_bgSound);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool isTest = true;
    void Start()
    {
        Invoke("FirstStart", 0.01f);
    }

    void FirstStart()
    {
        if (!isTest)
            BGM_START("ºø¼Ò¸®");
    }

    public void BGM_START(string BGM_Sel)
    {
        if (BGM_Sel == "ºø¼Ò¸®" && bgSound.clip != bglist[4])
        {
            BgSoundPlay(bglist[4]);
            Sub_BgSoundPlay(bglist[1]);
        }
        else if (BGM_Sel == "ºñ´Ã_±âº»" && bgSound.clip != bglist[3])
            BgSoundPlay(bglist[3]);
        else if (BGM_Sel == "ÁýÂø_±âº»" && bgSound.clip != bglist[2])
            BgSoundPlay(bglist[2]);
        else if (BGM_Sel == "Á¤ºñ" && bgSound.clip != bglist[0])
            BgSoundPlay(bglist[0]);
        else if (BGM_Sel == "N1" && bgSound.clip != bglist[5])
            BgSoundPlay(bglist[5]);
    }

    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        audioSource.clip = clip;
        audioSource.volume = 0.3f;
        audioSource.Play();

        Destroy(go, clip.length);
    }

    public void BGSoundVolume(float val)
    {
        mixer.SetFloat("BGSoundVolume", Mathf.Log10(val) * 20);
    }

    public void BgSoundPlay(AudioClip clip)
    {
        bgSound.outputAudioMixerGroup = mixer.FindMatchingGroups("BGSound")[0];
        bgSound.clip = clip;
        bgSound.loop = true;
        bgSound.volume = 0.6f;
        bgSound.Play();
    }

    public void Sub_BgSoundPlay(AudioClip clip)
    {
        sub_bgSound.outputAudioMixerGroup = mixer.FindMatchingGroups("BGSound")[0];
        sub_bgSound.clip = clip;
        sub_bgSound.loop = true;
        sub_bgSound.volume = 0.6f;
        sub_bgSound.Play();
    }

    public void Clear_BgSound()
    {
        if(bgSound.clip != null)
            bgSound.Stop();
        if (sub_bgSound.clip != null)
            sub_bgSound.Stop();
    }
}
