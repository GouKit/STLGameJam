using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadQuest : MonoBehaviour
{

    [Header("Reference")]
    private Text txt = null;
 
    [Header("Setting")]
    public float delay = 0.1f;
 
    private string orignText = "";
    private int count = 0;
 
    private void Start()
    {
        txt = GetComponent<Text>();
        Quest quest = GameObject.Find("Quest").GetComponent<Quest>();
        orignText = quest.ReturnQuest();
        StartCoroutine("PrintCoroutine");
    }

    private void OnDisable()
    {
        StopCoroutine("PrintCoroutine");
    }
 
    IEnumerator PrintCoroutine()
    {
        yield return new WaitForSeconds(delay);
 
        if (orignText.Length - count > 0)
        {
            txt.text += orignText[count++];
            StartCoroutine("PrintCoroutine");
        }
    }

}
