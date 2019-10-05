using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LoadQuest : MonoBehaviour
{

    [Header("Reference")]
    private Text txt = null;

    [Header("Setting")]
    public float delay = 0.1f;

    private string orignText = "";

    Quest quest;

    public UnityAction talkEnd;

    private void Awake()
    {
        txt = GetComponent<Text>();
        quest = FindObjectOfType<Quest>();
    }

    private void OnDisable()
    {
        StopCoroutine("PrintCoroutine");
    }

    public void CreateQuest()
    {
        txt.text = "";
        orignText = quest.ReturnQuest();
        StartCoroutine("PrintCoroutine");
    }

    IEnumerator PrintCoroutine()
    {
        int len = 0;
        while (len != orignText.Length)
        {
            txt.text += orignText[len++];
            yield return new WaitForSeconds(delay);
        }
        if (talkEnd != null)
            talkEnd.Invoke();

    }

}
