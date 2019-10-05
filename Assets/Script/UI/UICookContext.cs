using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICookContext : MonoBehaviour
{
    Text textCook;

    private void Awake()
    {
        textCook = GetComponentInChildren<Text>();
    }

    public void UpdateText(string cookName)
    {
        textCook.text = cookName;
    }

}
