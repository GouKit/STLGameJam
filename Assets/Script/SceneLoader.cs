using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void StartGame()
    {
        SoundManager.Instance.PlaySFX(SoundManager.SFX_SOUND.BUTTON_CLICK);
        
        SoundManager.Instance.StopSound();
        SoundManager.Instance.PlayIngameSound();
        SceneManager.LoadScene("cooktest");
    }
}
