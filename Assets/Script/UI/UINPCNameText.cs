using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINPCNameText : MonoBehaviour
{
    Text textName;

    private void Awake()
    {
        textName = GetComponent<Text>();
    }

    public void UpdateText(string name)
    {
        textName.text = name;
    }

}
