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
    public UnityAction nextNpc;

    Coroutine printCoroutine;

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
        quest.SetQuestRecipe();

        txt.text = "";
        orignText = quest.ReturnQuest();

        if (printCoroutine != null)
            StopCoroutine(printCoroutine);

        printCoroutine = StartCoroutine("PrintCoroutine");
    }

    public void ShowTasting(Quest.TASTING isGood)
    {
        txt.text = "";
        orignText = quest.Tasting(isGood);

        if (printCoroutine != null)
            StopCoroutine(printCoroutine);

        printCoroutine = StartCoroutine("Print");
    }

    IEnumerator Print()
    {
        int len = 0;
        while (len != orignText.Length)
        {
            txt.text += orignText[len++];
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(1f);

        if (nextNpc != null)
            nextNpc.Invoke();
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
