using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public Text highScore, currentScore;
    int maxScore;

    void Start()
    {
        if(PlayerPrefs.HasKey("HighScore"))
        {
            maxScore = PlayerPrefs.GetInt("HighScore");
        }
    }

    public void ShowScore()
    {
        if(maxScore <= GameManager.Instance.score)
        {
            maxScore = GameManager.Instance.score;
            PlayerPrefs.SetInt("HighScore", maxScore);
        }
        highScore.text = "" + maxScore;
        currentScore.text = "" + GameManager.Instance.score;
    }

    public void GotoMain()
    {
        SoundManager.Instance.PlaySFX(SoundManager.SFX_SOUND.BUTTON_CLICK);
        SoundManager.Instance.StopSound();
        SoundManager.Instance.PlayMainSonud();
        SceneManager.LoadScene("Main");
    }
}
