using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource BGMAudio;
    public AudioClip[] BGM;

    public enum SFX_SOUND
    {
        BUTTON_CLICK = 0,
        CATCH,
        GRILL
    }
    public AudioSource SFXAudio;
    public AudioClip[] SFX;

    void Start()
    {
        PlayMainSonud();
    }

    public void PlayMainSonud()
    {
        BGMAudio.clip = BGM[0];
        BGMAudio.loop = true;
        BGMAudio.Play();
    }

    public void PlayIngameSound()
    {
        BGMAudio.clip = BGM[1];
        BGMAudio.loop = true;
        BGMAudio.Play();
    }

    public void PauseSound()
    {
        if(BGMAudio.isPlaying)
            BGMAudio.Pause();
        else
            BGMAudio.UnPause();
    }

    public void StopSound()
    {
        if(BGMAudio.isPlaying)
            BGMAudio.Stop();
    }

    public void PlaySFX(SFX_SOUND type)
    {
        int num = (int)type;
        SFXAudio.PlayOneShot(SFX[num]);
    }
}
