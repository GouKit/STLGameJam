using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreText : MonoBehaviour
{

    Text textScore;

    private void Awake()
    {
        textScore = GetComponentInChildren<Text>();
    }

    public void UpdateText(int score) {
        textScore.text = score.ToString();
    }

}
