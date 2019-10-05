using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    private List<string> QuestList = new List<string>();

    void Awake()
    {
        AddQuest("자연주의적인 꼬치가 먹고싶어요.");
    }

    void AddQuest(string st)
    {
        QuestList.Add(st);
    }

    public string ReturnQuest()
    {
        return QuestList[Random.Range(0, QuestList.Count-1)];
    }

    
}
