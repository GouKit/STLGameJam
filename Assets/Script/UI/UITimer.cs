using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{
    [SerializeField]
    Image timeGauge;
    

    public void UpdateTimer(float time, float maxTime) {
        timeGauge.fillAmount = time / maxTime;
    }

}
