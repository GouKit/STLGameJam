using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICookResult : MonoBehaviour
{
    [SerializeField]
    Text textStickName;

    
    public void UpdateStickName(string stickName)
    {
        textStickName.text = stickName;
    }

}
