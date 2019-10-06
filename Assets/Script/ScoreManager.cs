using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public Text highScore, currentScore;
    int maxScore;

    public void ShowScore()
    {
        maxScore = PlayerPrefs.GetInt("HighScore", 0);

        if (maxScore < GameManager.Instance.score)
        {
            maxScore = GameManager.Instance.score;
            PlayerPrefs.SetInt("HighScore", maxScore);
        }

        highScore.text = maxScore.ToString();
        currentScore.text = GameManager.Instance.score.ToString();
    }

    public void GotoMain()
    {
        SoundManager.Instance.PlaySFX(SoundManager.SFX_SOUND.BUTTON_CLICK);
        SoundManager.Instance.StopSound();
        SoundManager.Instance.PlayMainSonud();
        SceneManager.LoadScene("Main");
    }
}
