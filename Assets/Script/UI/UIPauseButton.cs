using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPauseButton : MonoBehaviour
{
    public Sprite[] image;
    Button btn;
    Image mainImg;
    bool isStop = false;

    public Image fadeImage;
    public Text fadeText;
    public float fadeTime, start, end;
    private float time;

    private void Awake()
    {
        btn = GetComponent<Button>();
        mainImg = GetComponent<Image>();
        btn.onClick.AddListener(OnButtonDown);
        time = Time.deltaTime;
    }

    public void OnButtonDown()
    {
        isStop = !isStop;
        if(isStop)
        {
            mainImg.sprite = image[1];
            fadeText.text = "얼음";
            Time.timeScale = 0;
        }
        else
        {
            mainImg.sprite = image[0];
            fadeText.text = "땡";
            Time.timeScale = 1;
        }
        fadeImage.gameObject.SetActive(true);
        StartCoroutine(Fade());
        SoundManager.Instance.PauseSound();
    }

    IEnumerator Fade()
    {
        Color fadecolor = fadeImage.color;

        float timer = 0f;
        fadecolor.a = Mathf.Lerp(start, end, time);

        while (fadecolor .a > 0f)
        {
            timer +=  time/ fadeTime;
            fadecolor .a = Mathf.Lerp(start, end, timer);
            fadeImage.color = fadecolor ;
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
    }

}
