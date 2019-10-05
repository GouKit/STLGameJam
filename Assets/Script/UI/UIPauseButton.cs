using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPauseButton : MonoBehaviour
{
    Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnButtonDown);
    }


    public void OnButtonDown()
    {
        //TODO :: 일시정지 
        print("Pause");
    }

}
