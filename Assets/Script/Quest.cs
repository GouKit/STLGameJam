using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu
{
    public int id;
    public int count;
    public string foodName;
    public Menu(int id, int count, string name)
    {
        this.id = id;
        this.count = count;
        foodName = name;
    }
}

public class Quest : MonoBehaviour
{
    public List<Menu> QuestMenu = new List<Menu>();
    private List<string> Question = new List<string>();
    private FoodDB db;

    [Header("추가조건 확률")]
    public int second = 2, third = 3;
    [Header("재료 최대 최솟값")]
    public int min = 1, max = 16;
    private int temp;
    public int UseCount
    { get {return useCount;} set{useCount = value; if(useCount <= 0){useCount = 0;}} }
    private int useCount = 6;
    private int usingCount = 0;
    private int questNum = 0, before = 0;
    private string kor = "";
    
    void Awake()
    {
        db = FindObjectOfType<FoodDB>();
        AddQuest();
        AddQuestion();
    }

    void AddQuestion()
    {
        Question.Add("둘이 먹다 하나 죽게 만들어 주세요.");
        Question.Add("적당히 주세요.");
        Question.Add("최선을 다해 주세요.");
        Question.Add("살려 주세요.");
        Question.Add("힘내 주세요.");
        Question.Add("죽여 주세요."); 
    }

    public void AddQuest() //퀘스트 설정
    {
        before = QuestMenu.Count;

        temp = RandomNum(min, max);
        usingCount = RandomNum(0, UseCount);
        UseCount -= usingCount;
        QuestMenu.Add(new Menu(db.FindFoodWithID(temp).id, usingCount, db.FindFoodWithID(temp).name));

        if(RandomNum(0,10)%second == 0)
        {
            temp = RandomNum(min, max);
            usingCount = RandomNum(0, UseCount);
            UseCount -= usingCount;
            QuestMenu.Add(new Menu(db.FindFoodWithID(temp).id, usingCount, db.FindFoodWithID(temp).name));
            
            if(RandomNum(0,10)%third == 0)
            {
                temp = RandomNum(min, max);
                usingCount = RandomNum(0, UseCount);
                UseCount -= usingCount;
                QuestMenu.Add(new Menu(db.FindFoodWithID(temp).id, usingCount, db.FindFoodWithID(temp).name));
            }
        }

        questNum = QuestMenu.Count - before;
        OverlapCheck();
    }

    void OverlapCheck()
    {
        for(int i = 0; i < questNum; ++i)
        {
            int tp = QuestMenu[before+i].id;
            for(int j = i+1; j < questNum; ++j)
            {
                if(tp == QuestMenu[before+j].id)
                {
                    QuestMenu[before+j].foodName = "";
                }
            }
        }
    }

    int RandomNum(int min, int max)
    {
        return Random.Range(min, max);
    }

    string Korean(int index)
    {
        switch (QuestMenu[index].foodName)
        {
            case "레고캐빈":
            case "버섯":
            case "귤":
            case "날치알":
            case "얼음":
            case "태양":
                return kor = "을 " + Question[RandomNum(0, Question.Count-1)];
            case "":
                return "";
            default:
                return kor = "를 " + Question[RandomNum(0, Question.Count-1)];
        }
    }

    public string ReturnQuest()
    {
        string text = "";
        for(int i = 0; i < questNum; ++i)
        {
            text += QuestMenu[before+i].foodName + Korean(before+i);
        }
        return text;
    }
    
}
